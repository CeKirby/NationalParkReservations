using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class SiteSqlDAO 
    {
        private string connectionString;
        public SiteSqlDAO(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }
        public IList<Site> GetSiteByCampGroundId(int campGroundId)
        {
            List<Site> sites = new List<Site>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // column    // param name  
                    SqlCommand cmd = new SqlCommand("SELECT * FROM site WHERE campground_id = @campground_id;", conn);
                    // param name    // param value
                    cmd.Parameters.AddWithValue("@campground_id", campGroundId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        //Site campSite = ConvertReaderToSites(reader);
                        //sites.Add(campSite);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred reading campsites by Campground.");
                Console.WriteLine(ex.Message);
                throw;
            }
            return sites;
        }
    }
}
