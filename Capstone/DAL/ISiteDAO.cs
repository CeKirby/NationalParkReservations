using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public interface ISiteDAO
    {
        /// <summary>
        /// Gets all campsites provided a campgroundid.
        /// </summary>
        /// <param name="campGroundId">The campgroundid to search for.</param>
        /// <returns></returns>
        IList<Site> GetSitesByCampGroundId(string campGroundId);

    }
}
