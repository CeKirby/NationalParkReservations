using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public interface IParkDAO
    {
        /// <summary>
        /// Gets all parks
        /// </summary>
        /// <returns></returns>
        IList<Parks> GetParks();

        /// <summary>
        /// Gets all parks by location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        //IList<Parks> GetCampsite(string ParkLocation);
    }
}
