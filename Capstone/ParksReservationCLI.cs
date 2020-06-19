﻿using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Linq;
using System.Net;

namespace Capstone
{
    public class ParksReservationCLI
    {

        const string Command_DisplayParks = "1";
        const string Command_SelectPark = "2";
        const string Command_BookCampsite = "b";
        const string Command_ReturnToMainMenu = "r";
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
                        GetParks();
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

        private bool SelectParkMenu()
        {
            bool success = false;

            Console.WriteLine();
            Console.WriteLine(" B - Would you like to book a campsite?");
            Console.WriteLine(" R - Or return to the Main Menu?");
            string command = Console.ReadLine();
            switch (command.ToLower())
            {
                case Command_BookCampsite:
                    success = true;
                    BookCampsite();
                    break;
                case Command_ReturnToMainMenu:
                    success = true;
                    RunCLI();
                    break;
                default: 
                    Console.WriteLine("The command provided was not valid, please try again.");
                    break;
            }
            return success;

        }

        public int BookCampsite()
        {
            int confirmationNumber = -1;
            campgroundID = CLIHelper.GetInteger("Please enter the desired campground(ID)");
            startDate = CLIHelper.GetDateTime("Enter desired start date (YYYY-MM-DD)");
            endDate = CLIHelper.GetDateTime("Enter desired end date (YYYY-MM-DD)");
            int startMonth = campGroundDAO.CampGroundMonthToReserve();
            bool betweenOpenMonths = campGroundDAO.BetweenOpenMonths();

            //if (betweenOpenMonths == true)
            //{

            //}
            //else
            //{
            //    Console.WriteLine("Park is closed at date of reservation");
            //}
            Console.WriteLine(betweenOpenMonths);
            //display top 5 available on those dates
            //pulls correct value out of datetime to compare campgroundId open month next step


            //Console.WriteLine($"{CampGroundSqlDAO.month}");

        //    //display top 5 available on those dates 
        //    Console.ReadLine();

        //    Console.WriteLine("Would you like to Reserve a campsite? (Y/N)");
        //    string reserveInput = Console.ReadLine();

        //    if (reserveInput.ToLower() == "y")
        //    {
        //        int siteID = CLIHelper.GetInteger("Please enter the desired site(ID):");
        //        string familyName = CLIHelper.GetString("Enter Family Name:");


        //        Reservations reservation = new Reservations
        //        {
        //            SiteId = siteID,
        //            FamilyName = "",
        //            StartDate = startDate,
        //            EndDate = endDate,
        //            CreateDate = DateTime.Now

        //        };
        //    }
            
        //    return confirmationNumber;
        }

        private bool GetParks()
        {
            bool success = false;
            try
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
                success = true;
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return success;

        }

        private bool DisplayParkIds()
        {
            bool success = false;
            try
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
                success = true;
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return success;
        }

        private bool DisplayCampgroundsbyParkId()
        {
            bool success = false;
            try
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
                success = true;
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return success;

        }

        private bool DisplaySitesByCampGroundId()
        {
            bool success = false;
            try
            {
                int campgroundID = CLIHelper.GetInteger("Input the ID of the Campground:");

                IList<Site> sites = siteDAO.GetSitesByCampGroundId(campgroundID);

                Console.WriteLine();
                Console.WriteLine($"Campsites at Campground {campgroundID}");

                for (int index = 0; index < sites.Count; index++)
                {
                    Console.WriteLine(index + " - " + sites[index]);
                }
                success = true;
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return success;
        }
    }
}
