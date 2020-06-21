using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;

namespace Capstone.Tests
{
    [TestClass]
    public class SiteDAOTests : ParkReservationTestInitialize
    {
        [TestMethod]
        public void GetSiteByCampgroundTest()
        {
            //Arrange
            SiteSqlDAO siteSqlDAO = new SiteSqlDAO(connectionString);
            //Act
            IList<Site> sites = siteSqlDAO.GetSitesByCampGroundId(testCampgroundId);
            //Assert
            Assert.IsTrue(sites.Count > 0);
        }

        //[TestMethod]
        //public void GetSiteByCampgroundInvalidIdTest()
        //{
        //    //Arrange
        //    SiteSqlDAO siteSqlDAO = new SiteSqlDAO(connectionString);
        //    //Act
        //    IList<Site> sites = siteSqlDAO.GetSitesByCampGroundId(-1);
        //    //Assert
        //    Assert.Fail();
        }
    }
}
