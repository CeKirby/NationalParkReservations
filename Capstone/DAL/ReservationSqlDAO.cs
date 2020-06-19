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
        public ReservationSqlDAO(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }
        public void AddReservation(Reservations newReservation)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string cmd = "Insert into reservation Values(@reservation_id,@site_id,@name,@from_date, @to_date, @create_date);select scope_identity();";
                    SqlCommand sqlCommand = new SqlCommand(cmd, conn);
                    sqlCommand.Parameters.AddWithValue("@reservation_id", newReservation.ReservationId);
                    sqlCommand.Parameters.AddWithValue("@site_id", newReservation.SiteId);
                    sqlCommand.Parameters.AddWithValue("@name", newReservation.FamilyName);
                    sqlCommand.Parameters.AddWithValue("@from_date", newReservation.StartDate);
                    sqlCommand.Parameters.AddWithValue("@to_date", newReservation.EndDate);
                    sqlCommand.Parameters.AddWithValue("@create_date", newReservation.CreateDate);


                    int newReservationId = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    Console.WriteLine("The new reservation number is " + newReservationId);
                }
            }
            catch (Exception e)
            {

            }
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
        public IList<Reservations> GetReservationByCampground(int campgroundId)
        {
            List<Reservations> reservationsByCampground = new List<Reservations>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // column    // param name  
                    SqlCommand cmd = new SqlCommand( "select reservation.site_id, reservation.name, from_date, to_date from reservation join site on site.site_id = reservation.site_id join campground on campground.campground_id = site.campground_id where campground.campground_id = @campground_id", conn);
                   
                    // param name    // param value
                    cmd.Parameters.AddWithValue("@campground_id",ParksReservationCLI.campgroundID);

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
        
    }
}

