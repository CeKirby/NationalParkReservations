using Capstone.DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the connection string from the appsettings.json file
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            string connectionString = configuration.GetConnectionString("Project");

            ICampGroundDAO campGroundDAO = new CampGroundSqlDAO(connectionString);
            IParkDAO parkDAO = new ParkSqlDAO(connectionString);

            //not built yet
            //IReservationDAO reservationDAO = new ReservationSqlDAO(connectionString);
            //ISiteDAO siteDAO = new SiteSqlDAO(connectionString);


            ParksReservationCLI parksReservationCLI = new ParksReservationCLI(parkDAO, campGroundDAO);
            parksReservationCLI.RunCLI();
        }
    }
}
