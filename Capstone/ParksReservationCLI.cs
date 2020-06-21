using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Linq;
using System.Net;
using System.Data.SqlClient;

namespace Capstone
{
    public class ParksReservationCLI
    {

        const string Command_DisplayParks = "1";
        const string Command_SelectPark = "2";
        const string Command_BookCampsite = "s";
        const string Command_ReturnToMainMenu = "r";
        const string Command_Cancel = "0";
        const string Command_Quit = "q";

        public static DateTime startDate;
        public static DateTime endDate;
        public static int campgroundID;

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
                        DisplayParks();
                        break;
                    case Command_SelectPark:
                        Console.Clear();
                        menuSpacer();
                        DisplayParkIds();
                        menuSpacer();
                        DisplayCampgroundsbyParkId();
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
            Console.WriteLine();
            Console.WriteLine(" S - Search for Available Reservation");
            Console.WriteLine(" R - Return to the Main Menu");
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
                    SelectParkMenu();
                    break;
            }

        }

        public void BookCampsite()
        {
            campgroundID = CLIHelper.GetInteger("Please enter the desired campground(ID) or 0 to Cancel:");
            if (campgroundID == 0)
            {
                Console.Clear();
                RunCLI();
            }
            startDate = CLIHelper.GetDateTime("Enter desired start date (MM-DD-YYYY):");
            endDate = CLIHelper.GetDateTime("Enter desired end date (MM-DD-YYY):");
            ////for testing
            //IList<Reservations> reservationsByCampground = reservationDAO.GetReservationByCampground(campgroundID);
            //for (int index = 0; index < reservationsByCampground.Count; index++)
            //{
            //    Console.WriteLine($"SiteBooked: {reservationsByCampground[index].SiteId} From: {reservationsByCampground[index].StartDate.ToString("yyyy/MM/dd")} to {reservationsByCampground[index].EndDate.ToString("yyyy/MM/dd")}");
            //}
            ////
            int startMonth = campGroundDAO.CampGroundMonthToReserve();
            bool betweenOpenMonths = campGroundDAO.BetweenOpenMonths();

            if (betweenOpenMonths == true)
            {
                decimal stayCost = reservationDAO.TotalStayCost(campgroundID);
                IList<Site> availablesites = siteDAO.AvailableSites(campgroundID, startDate, endDate);

                if (availablesites.Count == 0)
                {
                    Console.WriteLine("There are no Available Sites for these dates.");
                    BookCampsite();
                }
                else
                {
                    Console.WriteLine("Results matching your search criteria:");
                    foreach (Site site in availablesites)
                    {
                        Console.Write(site.ToString());
                        Console.WriteLine($"Cost: {stayCost:C2}");
                    }
                    reservationDAO.MakeReservation(startDate, endDate);
                }
            }
            else
            {
                Console.WriteLine("Park is closed at date of reservation");
                Console.WriteLine("Press any key to return to main menu");
                Console.ReadLine();

            }

        }


        private void DisplayParks()
        {
            IList<Parks> parks = parkDAO.GetParks();

            Console.WriteLine();
            Console.WriteLine("Park Information Screen");
            Console.WriteLine();


            for (int index = 0; index < parks.Count; index++)
            {
                Console.WriteLine($"{parks[index].ParkName} National Park");
                Console.WriteLine($"Location:        {parks[index].Location}");
                Console.WriteLine($"Established:     {parks[index].EstablishedDate.ToString("yyyy/MM/dd")}");
                Console.WriteLine($"Area:            {parks[index].Area} sq km");
                Console.WriteLine($"Annual Visitors: {parks[index].Visitors}");
                Console.WriteLine();
                Console.WriteLine($"{parks[index].Description}");
                menuSpacer();

            }
        }

        private void DisplayParkIds()
        {
            IList<Parks> parks = parkDAO.GetParks();

            Console.WriteLine();
            Console.WriteLine("Park IDs");
            Console.WriteLine();


            for (int index = 0; index < parks.Count; index++)
            {
                Console.WriteLine($"{parks[index].ParkName} National Park");
                Console.WriteLine($"Park ID:     {parks[index].ParkId}");

            }
        }

        private void DisplayCampgroundsbyParkId()
        {
            int parkID = CLIHelper.GetInteger("Input the ID of the Park to show Campgrunds:");

            IList<CampGround> campGrounds = campGroundDAO.GetCampGroundByParkId(parkID);

            for (int index = 0; index < campGrounds.Count; index++)
            {
                Console.WriteLine($"{campGrounds[index].CampgroundName}");
                Console.WriteLine($"Campground ID:   {campGrounds[index].CampGroundId}");
                Console.WriteLine($"Open Month:      {campGrounds[index].OpenMonth}");
                Console.WriteLine($"Closing Month:   {campGrounds[index].ClosingMonth}");
                Console.WriteLine($"Daily Fee:       {campGrounds[index].DailyFee}");
                menuSpacer();

            }

        }

        private void DisplaySitesByCampGroundId()
        {
            int campgroundID = CLIHelper.GetInteger("Input the ID of the Campground:");

            IList<Site> sites = siteDAO.GetSitesByCampGroundId(campgroundID);

            Console.WriteLine();
            Console.WriteLine($"Campsites at Campground {campgroundID}");

            for (int index = 0; index < sites.Count; index++)
            {
                Console.WriteLine(index + " - " + sites[index]);
            }
        }
    }
}
