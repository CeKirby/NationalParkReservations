using System;
using System.Collections.Generic;
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
    }
}
