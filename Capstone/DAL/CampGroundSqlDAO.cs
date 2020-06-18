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
                        //CampGround campground = ConvertReaderToCampGround(reader);
                        //campgrounds.Add(campground);
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

        /*private CampGround ConvertReaderToCampGround(SqlDataReader reader)
        {
             when we get the variables in
            CampGround campground = new CampGround();
            campground.Id = Convert.ToInt32(reader["campground_id"]);
            campground.ParkId = Convert.ToInt32(reader["park_id"]);
            campground.name = Convert.ToString(reader["name"]);
            campground.OpenFrom = Convert.ToDateTime(reader["open_from_mm"]);
            campground.OpenTil = Convert.ToDateTime(reader["open_to_mm"]);
            campground.Fee = Convert.ToInt32(reader["daily_fee"]);

            return campground;
        }*/
    }

}
