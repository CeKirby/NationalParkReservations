using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Tests
{
    [TestClass]
    public class ParkDAOTests : ParkReservationTestInitialize
    {
        [TestMethod]
        public void GetParksTest()
        {
            //Arrange
            ParkSqlDAO parkSqlDAO = new ParkSqlDAO(connectionString);
            //Act
            IList<Parks> parks = parkSqlDAO.GetParks();
            //Assert
            Assert.IsTrue(parks.Count > 0);
        }
    }
}
