using System;
using TogglOutlookPlugIn.Models;

namespace TogglOutlookPlugIn.Services
{
    public static class TimeEntryExtensions
    {
        private static readonly string TogglResponseDateFormat = "MM/dd/yyyy HH:mm:ss";

        public static bool IsOverlappingWith(this Toggl.Api.DataObjects.TimeEntry entry, DateTime startTime, DateTime endTime)
        {
            DateTime entryStartTime = DateTime.ParseExact(entry.Start, TogglResponseDateFormat, null);
            DateTime entryEndTime = DateTime.ParseExact(entry.Stop, TogglResponseDateFormat, null);

            return (startTime >= entryStartTime && startTime < entryEndTime)
                || (endTime <= entryEndTime && endTime > entryStartTime);
        }

        public static bool IsConflictingWith(this Toggl.Api.DataObjects.TimeEntry entry, CategorizedAppointment appointment)
        {
            if (!entry.IsOverlappingWith(appointment.StartTime, appointment.EndTime))
            {
                return false;
            }
            else
            {
                return !entry.IsEqualTo(appointment);
            }
        }

        public static bool IsEqualTo(this Toggl.Api.DataObjects.TimeEntry entry, CategorizedAppointment appointment)
            => DateTime.ParseExact(entry.Start, TogglResponseDateFormat, null) == appointment.StartTime
                && DateTime.ParseExact(entry.Stop, TogglResponseDateFormat, null) == appointment.EndTime
                && entry.ProjectId == appointment.Category.ProjectId
                && entry.Description == appointment.Description;
    }
}
