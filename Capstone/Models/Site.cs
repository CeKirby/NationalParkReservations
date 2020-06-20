using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Site
    {
        public int SiteId { get; set; }
        public int CampgroundId { get; set; }
        public int SiteNumber { get; set; }
        public int SiteOccupency { get; set; }
        public bool Accessible { get; set; }
        public int RvLength { get; set; }
        public bool Utilities { get; set; }
        public int Popular { get; set; }


        public override string ToString()
        {
            return SiteId.ToString().PadRight(6) + CampgroundId.ToString().PadRight(6) + SiteNumber.ToString().PadRight(6) + SiteOccupency.ToString().PadRight(6) +
               (Accessible ? "Accessible" : "NonAccessbile").PadRight(10) + RvLength.ToString().PadRight(8) + (Utilities ? "Utility Available" : "Utility Not Available").PadRight(20);
        }
    }
}
