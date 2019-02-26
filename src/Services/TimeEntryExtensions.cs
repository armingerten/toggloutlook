using System;
using System.Collections.Generic;
using System.Linq;
using Toggl.Api.DataObjects;
using TogglOutlookPlugIn.Models;

namespace TogglOutlookPlugIn.Services
{
    public static class TimeEntryExtensions
    {
        private static readonly string TogglResponseDateFormat = "MM/dd/yyyy HH:mm:ss";

        /// <summary>
        /// Indicates whether this time entry is within the specified start- and end time.
        /// </summary>
        /// <param name="entry">The time entry.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <returns>True, if this time entry is within the specified start- and end time.</returns>
        public static bool IsOverlappingWith(this TimeEntry entry, DateTime startTime, DateTime endTime)
        {
            DateTime entryStartTime = entry.GetStartTime();
            DateTime entryEndTime = entry.GetEndTime();

            return (startTime >= entryStartTime && startTime < entryEndTime)
                || (endTime <= entryEndTime && endTime > entryStartTime);
        }

        /// <summary>
        /// Indicates whether this time entry is conflicting with one of the specified appointments or
        /// is no longer relevant within the specified boundaries.
        /// </summary>
        /// <param name="entry">The time entry.</param>
        /// <param name="appointments">The appointments.</param>
        /// <param name="lowerBoundary">The lower boundary.</param>
        /// <param name="upperBoundary">The upper boundary.</param>
        /// <returns>True, if the appointment is conflicting or obsolete.</returns>
        public static bool IsConflictingOrObsolete(this TimeEntry entry,
            IEnumerable<CategorizedAppointment> appointments,
            DateTime lowerBoundary,
            DateTime upperBoundary)
        {
            // Only consider time entry if it is within boundaries, otherwise it is implicitly valid
            if (entry.GetEndTime() <= lowerBoundary || entry.GetStartTime() >= upperBoundary)
                return false;


            return !appointments
                .Where(appointment => entry.IsOverlappingWith(appointment.StartTime, appointment.EndTime))
                .Any(appointment => entry.IsEqualTo(appointment));
        }

        /// <summary>
        /// Indicates whether the provided appointment's properties are equal to this time entry.
        /// </summary>
        /// <param name="entry">The time entry.</param>
        /// <param name="appointment">The appointment.</param>
        /// <returns>True, if the specified appointment's properties are equal to this time entry.</returns>
        public static bool IsEqualTo(this TimeEntry entry, CategorizedAppointment appointment)
            => entry.GetStartTime() == appointment.StartTime
            && entry.GetEndTime() == appointment.EndTime
            && entry.ProjectId == appointment.Category.ProjectId
            && entry.Description == appointment.Description;

        private static DateTime GetStartTime(this TimeEntry entry)
            => DateTime.ParseExact(entry.Start, TogglResponseDateFormat, null);

        private static DateTime GetEndTime(this TimeEntry entry)
            => DateTime.ParseExact(entry.Stop, TogglResponseDateFormat, null);
    }
}
