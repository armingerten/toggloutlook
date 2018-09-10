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
            foreach (AppointmentItem currentItem in GetItemsBetween(appointment.Start.Date, appointment.Start.Date.AddDays(1)))
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

        public static List<AppointmentItem> GetAppointmentsForDay(DateTime dateTime)
            => GetItemsBetween(dateTime.Date, dateTime.Date.AddDays(1)).ToAppointmentItemList();

        public static List<AppointmentItem> GetAppointmentsForLastSevenDays(DateTime dateTime)
            => GetItemsBetween(dateTime.Date.AddDays(-6), dateTime.Date.AddDays(1)).ToAppointmentItemList();

        public static List<AppointmentItem> ToAppointmentItemList(this Items items)
        {
            List<AppointmentItem> appointmentItems = new List<AppointmentItem>();
            foreach (AppointmentItem appointmentItem in items)
            {
                appointmentItems.Add(appointmentItem);
            }

            return appointmentItems;
        }

        private static Items GetItemsBetween(DateTime startDateTime, DateTime endDateTime)
        {
            Items items = CalendarFolder.Items;
            items.IncludeRecurrences = true;
            items.Sort("[Start]", Type.Missing);
            return items.Restrict($"[Start] >= '{startDateTime.ToString("g")}' AND [End] <= '{endDateTime.ToString("g")}'");
        }
    }
}
