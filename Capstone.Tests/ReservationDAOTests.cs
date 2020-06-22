using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Models;
using Capstone.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Capstone.Tests
{
    [TestClass]
    public class ReservationDAOTests : ParkReservationTestInitialize
    {
        [TestMethod]
        public void AddReservationTest()
        {
            //Arrange
            ReservationSqlDAO reservationSqlDAO = new ReservationSqlDAO(connectionString);
            string start ="2020-07-10";
            string end = "2020-07-13";
            Reservations reservation = new Reservations
            {
                SiteId = 1,
                FamilyName = "Jones",
                StartDate = Convert.ToDateTime(start),
                EndDate = Convert.ToDateTime(end),
                CreateDate = DateTime.Now

            };
            //Act
            int confirmationNumber = reservationSqlDAO.AddReservation(reservation);
            //Assert
            Assert.IsTrue(confirmationNumber > 0);
        }

        [TestMethod]
        public void TotalStayCostTest()
        {

            //Arrange
            decimal costAsAnticpiated = 0;
            ParksReservationCLI.campgroundID = 1;
            ParksReservationCLI.startDate = teststartDate;
            ParksReservationCLI.endDate = testEndDate;

            ReservationSqlDAO reservationSqlDAO = new ReservationSqlDAO(connectionString);
            //Act
            costAsAnticpiated = reservationSqlDAO.TotalStayCost(1);
            //Assert
            Assert.AreEqual(245.00M, costAsAnticpiated);

        }

        [TestMethod]

        public void GetReservationByCampgroundTest()
        {
            //Arrange
            ReservationSqlDAO reservationSqlDAO = new ReservationSqlDAO(connectionString);
            //Act
            IList<Reservations> reservations = reservationSqlDAO.GetReservationByCampground(testCampgroundId);
            //Assert
            Assert.IsTrue(reservations.Count > 0);
        }

        [TestMethod]
        public void GetReservationBySitesTest()
        {
            //Arrange
            ReservationSqlDAO reservationSqlDAO = new ReservationSqlDAO(connectionString);
            //Act
            IList<Reservations> reservations = reservationSqlDAO.GetReservationBySites(testCampsiteId);
            //Assert
            Assert.IsTrue(reservations.Count > 0);
        }

    }
}
