using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;

namespace Capstone.Tests
{
    [TestClass]
    public class ParkReservationTestInitialize
    {
        protected TransactionScope transactionScope;
        protected string connectionString = @"Data Source=.\SQLEXpress;Initial Catalog=npccampground;Integrated Security=true";
        protected int testParkId = 0;
        protected int testCampgroundId = 0;
        protected int testCampsiteId1 = 0;
        protected int testCampsiteId2 = 0;


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
                    string parkInsert = $"insert into park VALUES ('Jellystone', 'Pennsylvania', 1919-01-21, 41089, 2189849, '' ); select scope_identity();";
                    SqlCommand cmd = new SqlCommand(parkInsert, connection);
                    testParkId = Convert.ToInt32(cmd.ExecuteScalar());

                } catch (Exception e)
                {
                    
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
                    
                }
                try
                {
                    //make sure there are campsites at test campground
                    string campgroundInsert = $"insert into campground VALUES ({testParkId}, 'Pic-a-nic Park', 5, 11, 35.00); select scope_identity();";
                    SqlCommand cmd = new SqlCommand(campgroundInsert, connection);
                    testCampgroundId = Convert.ToInt32(cmd.ExecuteScalar());

                }
                catch (Exception e)
                {

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
