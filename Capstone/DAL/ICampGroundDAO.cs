using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    public interface ICampGroundDAO
    {

        /// <summary>
        /// Returns all campsites provided by park_id
        /// </summary>
        /// <param name="ParkId"></param>
        /// <returns></returns>
        IList<CampGround> GetCampGroundByParkId(int ParkId);

        int CampGroundMonthToReserve(DateTime startDate);

        bool BetweenOpenMonths(int campgroundId, int month);

        bool IsValidCampground(int parkID, int campgroundID);

    }
}
