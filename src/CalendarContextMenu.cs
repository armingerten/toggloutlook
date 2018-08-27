using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;
using TogglOutlookPlugIn.Categories;
using Office = Microsoft.Office.Core;

namespace TogglOutlookPlugIn
{
    [ComVisible(true)]
    public class CalendarContextMenu : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI ribbon;

        public CalendarContextMenu()
        {
        }

        private TogglService Toggl
            => TogglService.Instance;

        private Folder CalendarFolder
            => Globals.ThisAddIn.Application.Session.GetDefaultFolder(OlDefaultFolders.olFolderCalendar) as Folder;

        public Image GetTogglIcon(Office.IRibbonControl control)
            => Properties.Resources.Toggl;

        public Image GetFitToBoundariesIcon(Office.IRibbonControl control)
            => Properties.Resources.wand48;

        public bool GetContextMenuMultipleItemsIsVisible(Office.IRibbonControl control)
            => (control.Context as Selection)?[1] is AppointmentItem;

        public string OnPushAsGetContent(Office.IRibbonControl control)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<menu xmlns=\"http://schemas.microsoft.com/office/2006/01/customui\" >");

            this.Toggl.Projects.ForEach(project => stringBuilder
                .AppendLine($"<dynamicMenu id=\"p{project.Id}\" label=\"{project.Name}\"  getContent=\"OnPushAsProjectSubMenuGetContent\" />"));

            stringBuilder.Append("</menu>");
            return stringBuilder.ToString();
        }

        public string OnPushAsProjectSubMenuGetContent(Office.IRibbonControl control)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<menu xmlns=\"http://schemas.microsoft.com/office/2006/01/customui\" >");

            this.Toggl.Tags.ForEach(tag => stringBuilder
                .AppendLine($"<button id=\"{control.Id}t{tag.Id}\" label=\"{tag.Name}\" onAction=\"OnButtonPushAsClick\" />"));

            stringBuilder.Append("</menu>");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Pushes the currently selected appointment items to Toggl by applying the chosen Project / Tag.
        /// </summary>
        /// <param name="control">The clicked "PushAs" button containing the project / tag pattern as identifier.</param>
        public void OnButtonPushAsClick(Office.IRibbonControl control)
        {
            var buttonIdComponents = control.Id.TrimStart('p').Split('t');
            int projectId = int.Parse(buttonIdComponents[0]);
            int tagId = int.Parse(buttonIdComponents[1]);

            this.PushAppointmentsToToggle(GetSelectedAppointments(control), projectId, tagId);
        }

        public void OnQuickPushClick(Office.IRibbonControl control)
        {
            this.PushAppointmentsWithCategoryToToggle(GetSelectedAppointments(control));
        }

        private static List<AppointmentItem> GetSelectedAppointments(Office.IRibbonControl control)
        {
            List<AppointmentItem> appointments = new List<AppointmentItem>();
            foreach (object item in ((Selection)control.Context))
            {
                if (item is AppointmentItem appointmentItem)
                {
                    appointments.Add(appointmentItem);
                }
            }

            return appointments;
        }

        public void OnFitToBoundariesClick(Office.IRibbonControl control)
        {
            AppointmentItem selectedAppointment = GetSelectedAppointments(control).FirstOrDefault();

            // Ensure selected appointment is eligible to be fitted
            if (selectedAppointment == null
                || selectedAppointment.Conflicts.Count > 0
                || selectedAppointment.Start.Date != selectedAppointment.End.Date)
            {
                return;
            }

            // Determine predecessor & successor appointments (if any that day)
            AppointmentItem predecessor = null;
            AppointmentItem successor = null;
            foreach (AppointmentItem appointmentItem in this.GetAppointsmentsForDay(selectedAppointment.Start))
            {
                if (appointmentItem.End <= selectedAppointment.Start)
                {
                    predecessor = appointmentItem;
                    continue;
                }

                if (successor == null && appointmentItem.Start >= selectedAppointment.End)
                {
                    successor = appointmentItem;
                }
            }

            // Set new appointment start time (if a predecessor was found that day)
            if (predecessor != null)
            {
                int newDuration = (int)(selectedAppointment.End - predecessor.End).TotalMinutes;
                selectedAppointment.Start = predecessor.End;
                selectedAppointment.Duration = newDuration;
            }

            // Set new appointment end time (if a sucessor was found that day)
            if (successor != null)
            {
                selectedAppointment.End = successor.Start;
            }

            selectedAppointment.Save();
        }

        private Items GetAppointsmentsForDay(DateTime dateTime)
        {
            Items items = this.CalendarFolder.Items;
            items.IncludeRecurrences = true;
            items.Sort("[Start]", Type.Missing);
            return items.Restrict($"[Start] >= '{dateTime.Date.ToString("g")}' AND [End] <= '{dateTime.Date.AddDays(1).ToString("g")}'");
        }


        public void OnConfigureTogglPluginClick(Office.IRibbonControl control)
        {
            new Settings.SettingsDialog().ShowDialog();
        }

        private void PushAppointmentsWithCategoryToToggle(List<AppointmentItem> appointments)
        {
            Dictionary<AppointmentItem, bool> processedAppointments = new Dictionary<AppointmentItem, bool>();

            appointments.ForEach(appointment =>
            {
                if (this.TryParseCategory(appointment, out Categories.Category category))
                {
                    if (this.Toggl.TryCreateTimeEntry(appointment.Subject, appointment.Start, appointment.End, category))
                    {
                        processedAppointments.Add(appointment, true);
                    }
                    else
                    {
                        processedAppointments.Add(appointment, false);
                    }
                }
            });

            if (processedAppointments.Values.Count(v => v) == appointments.Count)
            {
                this.ShowPushingAppointmentsMessageBox($"Created {appointments.Count} appointment(s) in Toggl");
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var item in processedAppointments)
                {
                    stringBuilder.Append(item.Value ? "Created: " : "Not created: ");
                    stringBuilder.AppendLine($"{item.Key.Subject} ({item.Key.Start.ToShortTimeString()} - {item.Key.End.ToShortTimeString()})");
                }

                this.ShowPushingAppointmentsMessageBox(stringBuilder.ToString());
            }

        }

        private void ShowPushingAppointmentsMessageBox(string message)
            => MessageBox.Show(message, "Pushing appointment(s) to Toggl", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void PushAppointmentsToToggle(List<AppointmentItem> appointments, int projectId, int tagId)
        {
            List<AppointmentItem> createdAppointments = new List<AppointmentItem>();

            appointments.ForEach(appointment =>
            {
                if (this.Toggl.TryCreateTimeEntry(appointment.Subject, appointment.Start, appointment.End, projectId, tagId))
                {
                    createdAppointments.Add(appointment);
                }
            });

            MessageBox.Show($"Created {createdAppointments.Count} appointment(s) in toggl");
        }

        private bool TryParseCategory(AppointmentItem appointment, out Categories.Category category)
        {
            category = null;
            if (!string.IsNullOrWhiteSpace(appointment.Categories)
                && !appointment.Categories.Contains(CategoryManager.CategorySeperator))
            {
                category = CategoryManager.Instance.Categories.FirstOrDefault(c => c.Name == appointment.Categories && !c.IsOutlookOnly);
            }

            return category != null;
        }

        #region IRibbonExtensibility Members

        public string GetCustomUI(string ribbonID)
        {
            return GetResourceText("TogglOutlookPlugIn.CalendarContextMenu.xml");
        }

        #endregion

        #region Ribbon Callbacks
        //Create callback methods here. For more information about adding callback methods, visit https://go.microsoft.com/fwlink/?LinkID=271226

        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            this.ribbon = ribbonUI;
        }

        #endregion

        #region Helpers

        private static string GetResourceText(string resourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();
            for (int i = 0; i < resourceNames.Length; ++i)
            {
                if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
                {
                    using (StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
                    {
                        if (resourceReader != null)
                        {
                            return resourceReader.ReadToEnd();
                        }
                    }
                }
            }
            return null;
        }

        #endregion
    }
}
