using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Capstone
{
    public class ParksReservationCLI
    {

        const string Command_DisplayParks = "1";
        const string Command_SelectPark = "2";
        const string Command_BookCampsite = "b";
        const string Command_ReturnToMainMenu = "r";
        const string Command_Quit = "q";

        private ICampGroundDAO campGroundDAO;
        private IParkDAO parkDAO;
        private ISiteDAO siteDAO;
        private IReservationDAO reservationDAO;


        public ParksReservationCLI(IParkDAO parkDAO, ICampGroundDAO campGroundDAO, ISiteDAO siteDAO, IReservationDAO reservationDAO)
        {
            this.reservationDAO = reservationDAO;
            this.parkDAO = parkDAO;
            this.siteDAO = siteDAO;
            this.campGroundDAO = campGroundDAO;
        }

        public void RunCLI()
        {
            PrintHeader();
            
            bool repeatMenu = true;
            while (repeatMenu)
            {
                
                PrintMainMenu();
                string command = Console.ReadLine();
                switch (command.ToLower())
                {
                    case Command_DisplayParks:
                        Console.Clear();
                        menuSpacer();
                        //GetParks();
                        break;
                    case Command_SelectPark:
                        Console.Clear();
                        menuSpacer();
                        //GetParks();
                        menuSpacer();
                        SelectParkMenu();
                        break;
                    case Command_Quit:
                        Console.Clear();
                        Console.WriteLine("Thank you for using the National Parks Campsite Reservation System!");
                        repeatMenu = false;
                        break;
                    default:
                        Console.WriteLine("The command provided was not a valid command, please try again.");
                        break;
                }

            }
            
        }

        private void PrintHeader()
        {
            Console.WriteLine(@" _   _       _   _                   _   _____           _   ");
            Console.WriteLine(@"| \ | |     | | (_)                 | | |  __ \         | |  ");
            Console.WriteLine(@"|  \| | __ _| |_ _  ___  _ __   __ _| | | |__) |_ _ _ __| | _____");
            Console.WriteLine(@"| . ` |/ _` | __| |/ _ \| '_ \ / _` | | |  ___/ _` | '__| |/ / __|");
            Console.WriteLine(@"| |\  | (_| | |_| | (_) | | | | (_| | | | |  | (_| | |  |   <\__ \");
            Console.WriteLine(@"|_| \_|\__,_|\__|_|\___/|_| |_|\__,_|_| |_|   \__,_|_|  |_|\_\___/");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Welcome to the National Parks Campsite Reservation System!");


        }
        public void menuSpacer()
        {
            Console.WriteLine();
            Console.WriteLine("---~*~---");
            Console.WriteLine();
        }

        private void PrintMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Main-Menu Type in a command");
            Console.WriteLine(" 1 - Get a list of all National Parks");
            Console.WriteLine(" 2 - Select a Campground");
            Console.WriteLine(" Q - Quit");
        }

        private void SelectParkMenu()
        {
            menuSpacer();
            Console.WriteLine(" Select Park ID to Display Campgrounds at that Park");
            string parkID = Console.ReadLine();
            //can convert to int if needed?
            //DisplayCampgrounds(parkID);
            Console.WriteLine();
            Console.WriteLine(" B - Would you like to book a campsite?");
            Console.WriteLine(" R - Or return to the Main Menu?");
            string command = Console.ReadLine();
            switch (command.ToLower())
            {
                case Command_BookCampsite:
                    BookCampsite();
                    break;
                case Command_ReturnToMainMenu:
                    RunCLI();
                    break;
                default: 
                    Console.WriteLine("The command provided was not valid, please try again.");
                    break;
            }

        }

        public int BookCampsite()
        {
            int confirmationNumber = -1;
            int campgroundID = CLIHelper.GetInteger("Please enter the desired campground(ID)");
            DateTime startDate = CLIHelper.GetDateTime("Enter desired start date (YYYY-MM-DD)");
            DateTime endDate = CLIHelper.GetDateTime("Enter desired end date (YYYY-MM-DD)");

            //display top 5 available on those dates 
            Console.ReadLine();


            Reservations reservation = new Reservations
            {
                SiteId = 0,
                FamilyName = "",
                StartDate = startDate,
                EndDate = endDate,
                CreateDate = DateTime.Now

            };
            
            return confirmationNumber;
        }

        public void DisplaySitesByCampGroundId()
        {
            int campgroundID = CLIHelper.GetInteger("Input the ID of the Campground:");

            IList<Site> sites = siteDAO.GetSitesByCampGroundId(campgroundID);

            Console.WriteLine();
            Console.WriteLine($"Campsites at Campground {campgroundID}");

          //  foreach (var sites in northAmericanCountries)
           // {
           //     Console.WriteLine(country);
          //  }
        }

    }
}
