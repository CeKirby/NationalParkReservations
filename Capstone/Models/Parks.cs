using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Parks
    {
        public int ParkId { get; set; }
        public string ParkName { get; set; }

        public string Location { get; set; }
        public DateTime EstablishedDate { get; set; }
        public int Area { get; set; }
        public int Visitors { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return ParkId.ToString().PadRight(6) + ParkName.PadRight(6) + Location.PadRight(12) + EstablishedDate.ToString().PadRight(12) +
              Area.ToString().PadRight(10) + Visitors.ToString().PadRight(10) + "\n" + Description;
        }
    }
}
