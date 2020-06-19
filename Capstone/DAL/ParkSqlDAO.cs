using System;
using Microsoft.Win32.SafeHandles;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public class ParkSqlDAO : IParkDAO
    {
        private string connectionString;

        public ParkSqlDAO(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        public IList<Parks> GetParks()
        {
            IList<Parks> parks = new List<Parks>();

            try
            {
                // TODO 01 Create the connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // TODO 02 Open Connection
                    connection.Open();
                    // TODO 03
                    SqlCommand sqlCommand = new SqlCommand();
                    // TODO 04 Create command text
                    string sqlStatement = "select * from park";
                    // TODO 05 Set command text to command
                    sqlCommand.CommandText = sqlStatement;
                    // TODO 06 Set Connection
                    sqlCommand.Connection = connection;
                    // TODO 07 Read data
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    // Read each row
                    while (reader.Read())
                    {
                        Parks park = new Parks();

                        park.ParkId = Convert.ToInt32(reader["park_id"]);
                        park.ParkName = Convert.ToString(reader["name"]);
                        park.Location = Convert.ToString(reader["location"]);
                        park.EstablishedDate = Convert.ToDateTime(reader["establish_date"]);
                        park.Area = Convert.ToInt32(reader["area"]);
                        park.Visitors = Convert.ToInt32(reader["visitors"]);
                        park.Description = Convert.ToString(reader["description"]);
                        parks.Add(park);
                    }
                    // TODO 08 Close the connection via the using statement
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred retrieving list of parks.");
                Console.WriteLine(e.Message);
            }
            return parks;
        }

        
    }
}
