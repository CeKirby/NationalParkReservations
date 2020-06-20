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
            return $"Site No.: {SiteId.ToString().PadRight(2)} Max Occup.: {SiteOccupency.ToString().PadRight(2)} Accessible?:{(Accessible ? "Accessible" : "NonAccessbile").PadRight(15)} Max RV Length: {RvLength.ToString().PadRight(4)} Utitilies: {(Utilities ? "Utility Available" : "Not Available").PadRight(20)}";
        }
    }
}
