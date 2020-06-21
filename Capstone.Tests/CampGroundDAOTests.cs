using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;

namespace Capstone.Tests
{
    [TestClass]
    public class CampGroundDAOTests : ParkReservationTestInitialize
    {
        [TestMethod]
        public void CampGroundMonthToReserveTest()
        {

            //Arrange
            CampGroundSqlDAO campgroundDAO = new CampGroundSqlDAO(connectionString);
            //Act
            int month = campgroundDAO.CampGroundMonthToReserve(testStartDate);
            //Assert
            Assert.AreEqual(5, month);

        }

        [TestMethod]
        public void BetweenOpenMonthsTestOpen()
        {

            //Arrange
            CampGroundSqlDAO campgroundDAO = new CampGroundSqlDAO(connectionString);
            //Act
            bool isOpen = campgroundDAO.BetweenOpenMonths(testCampgroundId, 6);
            //Assert
            Assert.AreEqual(true, isOpen);

        }

        [TestMethod]
        public void BetweenOpenMonthsTestClosed()
        {

            //Arrange
            CampGroundSqlDAO campgroundDAO = new CampGroundSqlDAO(connectionString);
            //Act
            bool isOpen = campgroundDAO.BetweenOpenMonths(testCampgroundId, 2);
            //Assert
            Assert.AreEqual(false, isOpen);

        }

        [TestMethod]
        public void GeCampgroundByParkIdTest()
        {
            //Arrange
            CampGroundSqlDAO campgroundDAO = new CampGroundSqlDAO(connectionString);
            //Act
            IList<CampGround> campgrounds = campgroundDAO.GetCampGroundByParkId(testParkId);
            //Assert
            Assert.IsTrue(campgrounds.Count > 0);
        }
    }
}
