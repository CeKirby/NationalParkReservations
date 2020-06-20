﻿using Capstone.Models;
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

        public IList<Site> AvailableSites(int campgroundId, DateTime startDate, DateTime endDate)
        {

            List<Site> AvailableSites = new List<Site>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
<<<<<<< HEAD

                    string cmd = $"select  top 5 * from (Select Distinct reservation.Site_id), site join reservation on reservation.site_id = site.site_id where site.campground_id = @campgroundId and(reservation.from_date not between @startDate and @endDate) and(reservation.to_date not between @startDate and @endDate) ; ";
=======
                    string cmd = $"select * from site join campground on site.campground_id = campground.campground_id where site.campground_id = @campgroundId and site.site_id not in (select site_id from reservation where (reservation.to_date > @startDate and @endDate > reservation.from_date)); ";
                    //string cmd = $"select top 5 * from site join reservation on reservation.site_id = site.site_id where site.campground_id = @campgroundId and(reservation.from_date not between @startDate and @endDate) and(reservation.to_date not between @startDate and @endDate); ";
>>>>>>> dc6d0dc7d602b89b1f6eb3e12d4aead9e19a122e
                    SqlCommand sqlCommand = new SqlCommand(cmd, conn);
                    sqlCommand.Parameters.AddWithValue("@campgroundId", campgroundId);
                    sqlCommand.Parameters.AddWithValue("@startDate", startDate);
                    sqlCommand.Parameters.AddWithValue("@endDate", endDate);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        Site campSite = ConvertReaderToSites(reader);
                        AvailableSites.Add(campSite);
                    }

                }
            }
            catch (Exception e)
            {

            }

            return AvailableSites;
        }

        private Site ConvertReaderToSites(SqlDataReader reader)
        {
            Site campSite = new Site();
            campSite.SiteId = Convert.ToInt32(reader["site_id"]);
            campSite.CampgroundId = Convert.ToInt32(reader["campground_id"]);
            campSite.SiteNumber = Convert.ToInt32(reader["site_number"]);
            campSite.SiteOccupency = Convert.ToInt32(reader["max_occupancy"]);
            campSite.Accessible = Convert.ToBoolean(reader["accessible"]);
            campSite.RvLength = Convert.ToInt32(reader["max_rv_length"]);
            campSite.Utilities = Convert.ToBoolean(reader["utilities"]);
            return campSite;
        }


    }


}

