using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class ReservationSqlDAO : IReservationDAO
    {
        private string connectionString;
        public int totalStay;
        public ReservationSqlDAO(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }
        public int AddReservation(Reservations newReservation)
        {
            int newReservationId = -1;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string cmd = "Insert into reservation Values(@site_id,@name,@from_date, @to_date, @create_date);select scope_identity();";
                    SqlCommand sqlCommand = new SqlCommand(cmd, conn);
                    sqlCommand.Parameters.AddWithValue("@site_id", newReservation.SiteId);
                    sqlCommand.Parameters.AddWithValue("@name", newReservation.FamilyName);
                    sqlCommand.Parameters.AddWithValue("@from_date", newReservation.StartDate);
                    sqlCommand.Parameters.AddWithValue("@to_date", newReservation.EndDate);
                    sqlCommand.Parameters.AddWithValue("@create_date", newReservation.CreateDate);

                    newReservationId = Convert.ToInt32(sqlCommand.ExecuteScalar());
                }
            }
            catch (Exception e)
            {

            }
            return newReservationId;
        }
        public IList<Reservations> GetReservationBySites(int siteId)
        {
            List<Reservations> reservations = new List<Reservations>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // column    // param name  
                    SqlCommand cmd = new SqlCommand("SELECT * FROM reservation WHERE site_id = @site_id;", conn);
                    // param name    // param value
                    cmd.Parameters.AddWithValue("@site_id", siteId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Reservations madeReservations = ConvertReaderToReservations(reader);
                        reservations.Add(madeReservations);
                    }

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred reading reservations by campsites.");
                Console.WriteLine(ex.Message);
                throw;
            }
            return reservations;
        }

        private Reservations ConvertReaderToReservations(SqlDataReader reader)
        {
            Reservations madeReservations = new Reservations();
            madeReservations.ReservationId = Convert.ToInt32(reader["reservation_id"]);
            madeReservations.SiteId = Convert.ToInt32(reader["site_id"]);
            madeReservations.FamilyName = Convert.ToString(reader["name"]);
            madeReservations.StartDate = Convert.ToDateTime(reader["from_date"]);
            madeReservations.EndDate = Convert.ToDateTime(reader["to_date"]);
            madeReservations.CreateDate = Convert.ToDateTime(reader["create_date"]);

            return madeReservations;
        }
        public IList<Reservations> GetReservationByCampground(int campgroundID)
        {
            List<Reservations> reservationsByCampground = new List<Reservations>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // column    // param name  
                    SqlCommand cmd = new SqlCommand("select reservation.site_id, reservation.name, from_date, to_date from reservation join site on site.site_id = reservation.site_id join campground on campground.campground_id =" +
                        " site.campground_id where campground.campground_id = @campground_id  ", conn);

                    // param name    // param value
                    cmd.Parameters.AddWithValue("@campground_id", ParksReservationCLI.campgroundID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Reservations madeReservations = ConvertReaderToReservationsByCampground(reader);
                        reservationsByCampground.Add(madeReservations);
                    }


                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred reading reservations by campgrounds.");
                Console.WriteLine(ex.Message);
                throw;
            }
            return reservationsByCampground;
        }
        private Reservations ConvertReaderToReservationsByCampground(SqlDataReader reader)
        {
            Reservations madeReservations = new Reservations();
            madeReservations.SiteId = Convert.ToInt32(reader["site_id"]);
            madeReservations.FamilyName = Convert.ToString(reader["name"]);
            madeReservations.StartDate = Convert.ToDateTime(reader["from_date"]);
            madeReservations.EndDate = Convert.ToDateTime(reader["to_date"]);

            return madeReservations;
        }

        public int MakeReservation(DateTime startDate, DateTime endDate)
        {
            Console.WriteLine("Would you like to Reserve a campsite? (Y/N)");
            string reserveInput = Console.ReadLine();
            int confirmationNumber = 0;

            if (reserveInput.ToLower() == "y")
            {
                int siteID = CLIHelper.GetInteger("Which site should be reserved?:");
                string familyName = CLIHelper.GetString("What name should the reservation be under?:");
                Reservations reservation = new Reservations
                {
                    SiteId = siteID,
                    FamilyName = familyName,
                    StartDate = startDate,
                    EndDate = endDate,
                    CreateDate = DateTime.Now

                };
                confirmationNumber = AddReservation(reservation);
                Console.WriteLine("The reservation has been made. Your confirmation number is " + confirmationNumber);

            }
            Console.Clear();
            return confirmationNumber;
        }

        public decimal TotalStayCost(int campgroundId)
        {
            decimal totalCostForStay = 0M;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // column    // param name  
                    SqlCommand cmd = new SqlCommand($"SELECT DATEDIFF(day, @startDate, @endDate);", conn);
                    // param name    // param value
                    cmd.Parameters.AddWithValue("@startDate", ParksReservationCLI.startDate);
                    cmd.Parameters.AddWithValue("@endDate", ParksReservationCLI.endDate);
                    int totalStay = (int)cmd.ExecuteScalar();

                    SqlCommand sqlCmd = new SqlCommand($"SELECT daily_fee from campground where campground_id = @campgroundId", conn);
                    // param name    // param value
                    sqlCmd.Parameters.AddWithValue("@campgroundId", campgroundId);
                    decimal cost = (decimal)sqlCmd.ExecuteScalar();

                    totalCostForStay = cost * totalStay;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred reading difference in days to stay.");
                Console.WriteLine(ex.Message);
                throw;
            }
            return totalCostForStay;
        }

    }
}

