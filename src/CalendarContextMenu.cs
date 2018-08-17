using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Toggl.Api.DataObjects;
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

        private TogglService Toggl => TogglService.Instance;

        public Image GetTogglIcon(Office.IRibbonControl control)
            => Properties.Resources.Toggl;

        public bool GetContextMenuMultipleItemsIsVisible(Office.IRibbonControl control)
            => (control.Context as Selection)?[1] is AppointmentItem;

        public string OnPushAsGetContent(Office.IRibbonControl control)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<menu xmlns=\"http://schemas.microsoft.com/office/2006/01/customui\" >");

            foreach (Project project in this.Toggl.Projects)
            {
                stringBuilder.AppendLine($"<dynamicMenu id=\"p{project.Id}\" label=\"{project.Name}\"  getContent=\"OnPushAsProjectSubMenuGetContent\" />");
            }

            stringBuilder.Append("</menu>");

            return stringBuilder.ToString();
        }

        public string OnPushAsProjectSubMenuGetContent(Office.IRibbonControl control)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<menu xmlns=\"http://schemas.microsoft.com/office/2006/01/customui\" >");

            foreach (Tag tag in this.Toggl.Tags)
            {
                stringBuilder.AppendLine($"<button id=\"{control.Id}t{tag.Id}\" label=\"{tag.Name}\" onAction=\"OnPushAsClick\" />");
            }

            stringBuilder.Append("</menu>");

            return stringBuilder.ToString();
        }

        public void OnPushAsClick(Office.IRibbonControl control)
        {
            var buttonIdComponents = control.Id.TrimStart('p').Split('t');
            int projectId = int.Parse(buttonIdComponents[0]);
            int tagId = int.Parse(buttonIdComponents[1]);

            List<AppointmentItem> appointments = new List<AppointmentItem>();
            foreach (object item in ((Selection)control.Context))
            {
                if (item is AppointmentItem appointmentItem)
                {
                    appointments.Add(appointmentItem);
                }
            }

            this.PushAppointmentsToToggle(appointments, projectId, tagId);
        }

        public void OnQuickPushClick(Office.IRibbonControl control)
        {
            List<AppointmentItem> appointments = new List<AppointmentItem>();
            foreach (object item in ((Selection)control.Context))
            {
                if (item is AppointmentItem appointmentItem)
                {
                    appointments.Add(appointmentItem);
                }
            }

            this.PushAppointmentsWithCategoryToToggle(appointments);
        }

        public void OnConfigureTogglPluginClick(Office.IRibbonControl control)
        {
            new Settings.SettingsDialog().ShowDialog();
            //new Settings.ConfigureTogglForm().ShowDialog();
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

            System.Windows.Forms.MessageBox.Show($"Created {createdAppointments.Count} appointment(s) in toggl");
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
