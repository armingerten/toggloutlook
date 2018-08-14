﻿using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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

        public void OnPushToTogglClick(Office.IRibbonControl control)
        {
            List<AppointmentItem> appointments = new List<AppointmentItem>();
            foreach (object item in ((Selection)control.Context))
            {
                if (item is AppointmentItem appointmentItem)
                {
                    appointments.Add(appointmentItem);
                }
            }

            this.PushToToggle(appointments);
        }

        public void OnConfigureTogglPluginClick(Office.IRibbonControl control)
        {
            new ConfigureTogglForm().ShowDialog();
        }

        private void PushToToggle(List<AppointmentItem> appointments)
        {
            List<AppointmentItem> createdAppointments = new List<AppointmentItem>();

            appointments.ForEach(appointment =>
            {
                if (this.TryParseCategory(appointment, out Categories.Category category))
                {
                    if (this.Toggl.TryCreateTimeEntry(appointment.Subject, appointment.Start, appointment.End, category))
                    {
                        createdAppointments.Add(appointment);
                    }
                }
            });

            System.Windows.Forms.MessageBox.Show($"Created {createdAppointments.Count} appointments in toggl");
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
