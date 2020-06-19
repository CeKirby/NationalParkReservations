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
        public void MakeReservationTest()
        {
            //Arrange
            ReservationSqlDAO reservationSqlDAO = new ReservationSqlDAO(connectionString);
            //Act
            string start ="2020-07-10";
            string end = "2020-07-13";
            int confirmationNumber = reservationSqlDAO.MakeReservation( Convert.ToDateTime(start), Convert.ToDateTime(end));
            //Assert
            Assert.IsTrue(confirmationNumber > 0);
        }
    }
}
