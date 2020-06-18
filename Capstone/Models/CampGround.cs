using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class CampGround
    {
       public int CampGroundId { get; set; }
        public int ParkId { get; set; }
        public string CampgroundName { get; set; }
        public int OpenMonth { get; set; }
        public int ClosingMonth { get; set; }
        public decimal DailyFee { get; set; }

        public override string ToString()
        {
            return CampGroundId.ToString().PadRight(6) + ParkId.ToString().PadRight(6) + CampgroundName.PadRight(10) + OpenMonth.ToString().PadRight(6) 
               + ClosingMonth.ToString().PadRight(6) + DailyFee.ToString().PadRight(6);
        }

    }
}
