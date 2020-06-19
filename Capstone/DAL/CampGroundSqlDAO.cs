using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq;
using Capstone;
using System.Runtime.CompilerServices;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public class CampGroundSqlDAO : ICampGroundDAO
    {
        public static int month;
        private string connectionString;

        //Single parameter constructor
        public CampGroundSqlDAO(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        public IList<CampGround> GetCampGroundByParkId(int ParkId)
        {
            List<CampGround> campgrounds = new List<CampGround>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // column    // param name  
                    SqlCommand cmd = new SqlCommand("SELECT * FROM campground WHERE park_id = @ParkId;", conn);
                    // param name    // param value
                    cmd.Parameters.AddWithValue("@ParkId", ParkId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        CampGround campground = ConvertReaderToCampGround(reader);
                        campgrounds.Add(campground);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred reading campsites by park.");
                Console.WriteLine(ex.Message);
                throw;
            }
            return campgrounds;
        }

        private CampGround ConvertReaderToCampGround(SqlDataReader reader)
        {

            CampGround campground = new CampGround();
            campground.CampGroundId = Convert.ToInt32(reader["campground_id"]);
            campground.ParkId = Convert.ToInt32(reader["park_id"]);
            campground.CampgroundName = Convert.ToString(reader["name"]);
            campground.OpenMonth = Convert.ToInt32(reader["open_from_mm"]);
            campground.ClosingMonth = Convert.ToInt32(reader["open_to_mm"]);
            campground.DailyFee = Convert.ToInt32(reader["daily_fee"]);

            return campground;
        }
        public int CampGroundMonthToReserve()
        {
            month = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // column    // param name  
                    SqlCommand cmd = new SqlCommand("SELECT MONTH(@startDate);", conn);
                    // param name    // param value
                    cmd.Parameters.AddWithValue("@startDate", ParksReservationCLI.startDate);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        //CampGround campground = ConvertReaderToCampGround(reader);
                        month = GetInt32(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred reading campsites by park.");
                Console.WriteLine(ex.Message);
                throw;
            }

            return month;
        }


        public int BookCampsite()
        {
            int confirmationNumber = -1;
            int campgroundID = CLIHelper.GetInteger("Please enter the desired campground(ID)");
            DateTime startDate = CLIHelper.GetDateTime("Enter desired start date (YYYY-MM-DD)");
            DateTime endDate = CLIHelper.GetDateTime("Enter desired end date (YYYY-MM-DD)");
            


            //display top 5 available on those dates 
            Console.ReadLine();

            Console.WriteLine("Would you like to Reserve a campsite? (Y/N)");
            string reserveInput = Console.ReadLine();

            if (reserveInput.ToLower() == "y")
            {
                int siteID = CLIHelper.GetInteger("Please enter the desired site(ID):");
                string familyName = CLIHelper.GetString("Enter Family Name:");


                Reservations reservation = new Reservations
                {
                    SiteId = siteID,
                    FamilyName = "",
                    StartDate = startDate,
                    EndDate = endDate,
                    CreateDate = DateTime.Now

                };
            }

            return confirmationNumber;
        }

    }

}
