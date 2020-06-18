using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Reservations
    {
        public int ReservationId { get; set; }
        public int SiteId { get; set; }
        public string FamilyName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateDate { get; set; }

        public override string ToString()
        {
            return ReservationId.ToString().PadRight(6) + SiteId.ToString().PadRight(6) + FamilyName.PadRight(16) + StartDate.ToString().PadRight(6) +
              EndDate.ToString().PadRight(10) + CreateDate.ToString().PadRight(8);
        }
    }
}
