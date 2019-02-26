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
using TogglOutlookPlugIn.Models;
using TogglOutlookPlugIn.Services;
using TogglOutlookPlugIn.Synchronization;
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

        public Image GetTogglIcon(Office.IRibbonControl control)
            => Properties.Resources.Toggl;

        public Image GetFitToBoundariesIcon(Office.IRibbonControl control)
            => Properties.Resources.wand48;

        public bool IsContextMenuMultipleItemsVisible(Office.IRibbonControl control)
            => (control.Context as Selection)?[1] is AppointmentItem;

        public bool IsSyncNowVisible(Office.IRibbonControl control)
            => SynchronizationService.Instance.SynchronizationOption != SyncOption.NoSync;

        public bool IsPushAsVisible(Office.IRibbonControl control)
            => SynchronizationService.Instance.SynchronizationOption == SyncOption.NoSync;

        public string OnPushAsGetContent(Office.IRibbonControl control)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<menu xmlns=\"http://schemas.microsoft.com/office/2006/01/customui\" >");

            this.Toggl.Projects.ForEach(project => stringBuilder
                .AppendLine($"<button id=\"{project.Id}\" label=\"{project.Name}\"  onAction=\"OnButtonPushAsClick\" />"));

            stringBuilder.Append("</menu>");
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Pushes the currently selected appointment items to Toggl by applying the chosen Project / Tag.
        /// </summary>
        /// <param name="control">The clicked "PushAs" button containing the project / tag pattern as identifier.</param>
        public void OnButtonPushAsClick(Office.IRibbonControl control)
            => this.PushAppointmentsToToggle(GetSelectedAppointments(control), int.Parse(control.Id));

        public void OnQuickPushClick(Office.IRibbonControl control)
            => this.PushAppointmentsWithCategoryToToggle(GetSelectedAppointments(control));

        private static List<AppointmentItem> GetSelectedAppointments(Office.IRibbonControl control)
        {
            List<AppointmentItem> appointments = new List<AppointmentItem>();
            foreach (object item in (Selection)control.Context)
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
            var (predecessor, successor) = Calendar.GetSurroundingAppointmentsTheSameDay(selectedAppointment);

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

        public void OnSyncNowClick(Office.IRibbonControl control) => SynchronizationService.Instance.SynchronizeWithToggl();

        public void OnConfigureTogglPluginClick(Office.IRibbonControl control) => new Settings.SettingsDialog().ShowDialog();

        private void PushAppointmentsWithCategoryToToggle(List<AppointmentItem> appointments)
        {
            Dictionary<AppointmentItem, bool> processedAppointments = new Dictionary<AppointmentItem, bool>();

            appointments.ForEach(appointment =>
            {
                if (Calendar.TryParseCategorizedAppointment(appointment, out CategorizedAppointment categorizedAppointment))
                {
                    if (this.Toggl.TryCreateAppointment(categorizedAppointment))
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

        private void PushAppointmentsToToggle(List<AppointmentItem> appointments, int projectId)
        {
            List<AppointmentItem> createdAppointments = new List<AppointmentItem>();

            appointments.ForEach(appointment =>
            {
                if (this.Toggl.TryCreateAppointment(appointment.Subject, appointment.Start, appointment.End, projectId))
                {
                    createdAppointments.Add(appointment);
                }
            });

            MessageBox.Show($"Created {createdAppointments.Count} appointment(s) in toggl");
        }

        #region IRibbonExtensibility Members

        public string GetCustomUI(string ribbonID) => GetResourceText("TogglOutlookPlugIn.CalendarContextMenu.xml");

        #endregion

        #region Ribbon Callbacks
        //Create callback methods here. For more information about adding callback methods, visit https://go.microsoft.com/fwlink/?LinkID=271226

        public void Ribbon_Load(Office.IRibbonUI ribbonUI) => this.ribbon = ribbonUI;

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
