using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Outlook;
using TogglOutlookPlugIn.Models;
using TogglOutlookPlugIn.Services;

namespace TogglOutlookPlugIn
{
    public static class Calendar
    {
        private static Folder CalendarFolder
            => Globals.ThisAddIn.Application.Session.GetDefaultFolder(OlDefaultFolders.olFolderCalendar) as Folder;

        public static bool TryParseCategorizedAppointment(AppointmentItem appointmentItem, out CategorizedAppointment categorizedAppointment)
        {
            categorizedAppointment = null;
            if (!string.IsNullOrWhiteSpace(appointmentItem.Categories)
                && !appointmentItem.Categories.Contains(CategoryService.CategorySeperator)
                && TryParseCategory(appointmentItem, out Models.Category category))
            {
                categorizedAppointment = new CategorizedAppointment
                {
                    Description = appointmentItem.Subject,
                    StartTime = appointmentItem.Start,
                    EndTime = appointmentItem.End,
                    Category = category
                };
            }
            return categorizedAppointment != null;
        }

        private static bool TryParseCategory(AppointmentItem appointment, out Models.Category category)
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

        public static List<CategorizedAppointment> GetAppointmentsBetween(DateTime startTime, DateTime endTime)
            => ParseCategorizedAppointsments(GetItemsBetween(startTime, endTime).ToAppointmentItemList());

        public static List<AppointmentItem> ToAppointmentItemList(this Items items)
        {
            List<AppointmentItem> appointmentItems = new List<AppointmentItem>();
            foreach (AppointmentItem appointmentItem in items)
            {
                appointmentItems.Add(appointmentItem);
            }

            return appointmentItems;
        }

        private static Items GetItemsBetween(DateTime startTime, DateTime endTime)
        {
            Items items = CalendarFolder.Items;
            items.IncludeRecurrences = true;
            items.Sort("[Start]", Type.Missing);
            return items.Restrict($"[Start] >= '{startTime.ToString("g")}' AND [End] <= '{endTime.ToString("g")}'");
        }


        private static List<CategorizedAppointment> ParseCategorizedAppointsments(List<AppointmentItem> appointmentItems)
        {
            var categorizedAppointments = new List<CategorizedAppointment>();
            appointmentItems.ForEach(appointmentItem =>
            {
                if (TryParseCategorizedAppointment(appointmentItem, out CategorizedAppointment categorizedAppointment))
                {
                    categorizedAppointments.Add(categorizedAppointment);
                }
            });

            return categorizedAppointments;
        }
    }
}
