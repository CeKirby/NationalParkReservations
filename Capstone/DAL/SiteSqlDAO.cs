using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class SiteSqlDAO : ISiteDAO
    {
        private string connectionString;
        public SiteSqlDAO(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        public IList<Site> GetSitesByCampGroundId(int campGroundId)
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
                        Site campSite = ConvertReaderToSites(reader);
                        sites.Add(campSite);
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



        private Site ConvertReaderToSites(SqlDataReader reader)
        {
            Site campSite = new Site();
            campSite.SiteId = Convert.ToInt32(reader["site_id"]);
            campSite.CampgroundId = Convert.ToInt32(reader["campground_id_id"]);
            campSite.SiteNumber = Convert.ToInt32(reader["site_number"]);
            campSite.SiteOccupency = Convert.ToInt32(reader["max_occupency"]);
            campSite.Accessible = Convert.ToBoolean(reader["accessible"]);
            campSite.RvLength = Convert.ToInt32(reader["max_rv_length"]);
            campSite.Utilities = Convert.ToBoolean(reader["utilities"]);
            return campSite;
        }
    }


}

