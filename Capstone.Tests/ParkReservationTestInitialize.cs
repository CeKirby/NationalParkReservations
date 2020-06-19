using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Capstone.Tests
{
    [TestClass]
    public class ParkReservationTestInitialize
    {
        protected TransactionScope transactionScope;
        protected string connectionString = @"Data Source=.\SQLExpress;Database=npcampground;Trusted_Connection=True;";
        protected int testParkId = 0;
        protected int testCampgroundId = 0;
        protected int testCampsiteId = 1;
        protected int testReservationId1 = 0;
        protected int testReservationId2 = 0;

        [TestInitialize]
        public void Initialize()
        {
            transactionScope = new TransactionScope();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    //make sure there is at least 1 park, saves ID
                    string parkInsert = $"insert into park VALUES ('Jellystone', 'Pennsylvania', '1919-01-21', 41089, 2189849, 'A haven for local wildlife. Please feed the bears.' ); select scope_identity();";
                    SqlCommand cmd = new SqlCommand(parkInsert, connection);
                    testParkId = Convert.ToInt32(cmd.ExecuteScalar());

                } catch (Exception e)
                {
                    Console.WriteLine("An error occurred inserting new park.");
                    Console.WriteLine(e.Message);
                }
                try
                {
                    //make sure there is a campground at test park
                    string campgroundInsert = $"insert into campground VALUES ({testParkId}, 'Pic-a-nic Park', 5, 11, 35.00); select scope_identity();";
                    SqlCommand cmd = new SqlCommand(campgroundInsert, connection);
                    testCampgroundId = Convert.ToInt32(cmd.ExecuteScalar());

                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred inserting new campground.");
                    Console.WriteLine(e.Message);
                }
                try
                {
                    //make sure there is a campsite at test campground
                    string campsiteInsert = $"insert into site VALUES ({testCampgroundId},1,4,1,0,1); select scope_identity();";
                    SqlCommand cmd = new SqlCommand(campsiteInsert, connection);
                    testCampsiteId = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred inserting new campsite.");
                    Console.WriteLine(e.Message);
                }
                try
                {
                    //make sure there is a campsite at test campground
                    string reservationInsert = $"insert into reservation VALUES ({testCampsiteId}, 'Martha Grant', '2020-09-01', '2020-09-04', '{DateTime.Now}'); select scope_identity();";
                    SqlCommand cmd = new SqlCommand(reservationInsert, connection);
                    testReservationId1 = Convert.ToInt32(cmd.ExecuteScalar());

                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred inserting new reservation.");
                    Console.WriteLine(e.Message);
                }
                try
                {
                    //make sure there is a campsite at test campground
                    string reservationInsert = $"insert into reservation VALUES ({testCampsiteId}, 'Lovelace Family Reservation', '2020-06-10', '2020-06-16', '{DateTime.Now}'); select scope_identity();";
                    SqlCommand cmd = new SqlCommand(reservationInsert, connection);
                    testReservationId2 = Convert.ToInt32(cmd.ExecuteScalar());

                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred inserting new reservation.");
                    Console.WriteLine(e.Message);
                }
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            transactionScope.Dispose();
        }

    }
}
