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
        public static bool betweenOpenMonths;

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
                    SqlCommand cmd = new SqlCommand("SELECT MONTH(@startDate) as MonthInt;", conn);
                    // param name    // param value
                    cmd.Parameters.AddWithValue("@startDate", ParksReservationCLI.startDate);

                    month = (int)cmd.ExecuteScalar();


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

        
        public bool BetweenOpenMonths()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // column    // param name  
                    SqlCommand cmd = new SqlCommand("SELECT open_from_mm, open_to_mm from campground where @month between open_from_mm and open_to_mm and campground_id = @campground_id", conn);
                    // param name    // param value
                    cmd.Parameters.AddWithValue("@month", month);
                    cmd.Parameters.AddWithValue("@campground_id", ParksReservationCLI.campgroundID);

                    var certainty = cmd.ExecuteScalar();

                    if (certainty != null)
                    {
                        betweenOpenMonths = true;
                    }
                    else
                    {
                        betweenOpenMonths = false;
                    }


                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred determining if date is during Open Season.");
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (System.NullReferenceException e)
            {
                betweenOpenMonths = false;
                Console.WriteLine(e.Message);
            }
            return betweenOpenMonths;

        }

    }

}
