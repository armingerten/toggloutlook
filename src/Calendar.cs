using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Outlook;
using TogglOutlookPlugIn.Categories;

namespace TogglOutlookPlugIn
{
    public static class Calendar
    {
        private static Folder CalendarFolder
            => Globals.ThisAddIn.Application.Session.GetDefaultFolder(OlDefaultFolders.olFolderCalendar) as Folder;

        public static bool TryParseCategory(AppointmentItem appointment, out Categories.Category category)
        {
            category = null;
            if (!string.IsNullOrWhiteSpace(appointment.Categories)
                && !appointment.Categories.Contains(CategoryService.CategorySeperator))
            {
                category = CategoryService.Instance.Categories.FirstOrDefault(c => c.Name == appointment.Categories && !c.IsOutlookOnly);
            }

            return category != null;
        }

        public static (AppointmentItem predecessor, AppointmentItem successor) GetSurroundingAppointmentsTheSameDay(AppointmentItem appointment)
        {
            AppointmentItem predecessor = null;
            AppointmentItem successor = null;
            foreach (AppointmentItem currentItem in GetItemsForDay(appointment.Start))
            {
                if (currentItem.End <= appointment.Start)
                {
                    predecessor = currentItem;
                    continue;
                }

                if (successor == null && currentItem.Start >= appointment.End)
                {
                    successor = currentItem;
                }
            }

            return (predecessor, successor);
        }

        public static List<AppointmentItem> GetAppointsmentsForDay(DateTime dateTime)
        {
            List<AppointmentItem> appointmentItems = new List<AppointmentItem>();
            foreach (AppointmentItem appointmentItem in GetItemsForDay(dateTime))
            {
                appointmentItems.Add(appointmentItem);
            }

            return appointmentItems;
        }

        private static Items GetItemsForDay(DateTime dateTime)
        {
            Items items = CalendarFolder.Items;
            items.IncludeRecurrences = true;
            items.Sort("[Start]", Type.Missing);
            return items.Restrict($"[Start] >= '{dateTime.Date.ToString("g")}' AND [End] <= '{dateTime.Date.AddDays(1).ToString("g")}'");
        }
    }
}
