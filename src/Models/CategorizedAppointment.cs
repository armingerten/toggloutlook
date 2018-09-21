using System;

namespace TogglOutlookPlugIn.Models
{
    public class CategorizedAppointment
    {
        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public Category Category { get; set; }
    }
}
