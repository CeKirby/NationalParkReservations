using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Models;
using Capstone.DAL;
using System;
using System.Collections.Generic;
using System.Text;

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

    }
}
