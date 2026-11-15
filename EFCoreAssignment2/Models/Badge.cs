using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreAssignment2.Models
{
    internal class Badge
    {
        public int Id { get; set; }
        public string BadgeNumber { get; set; } = string.Empty;
        public DateTime IssuedDate { get; set; }
        public BadgeTier Tier { get; set; }

        public int AttendeeId { get; set; }
        public Attendee Attendee { get; set; } = null!;
    }
}
