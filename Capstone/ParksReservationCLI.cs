using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class ParksReservationCLI
    {

        const string Command_DisplayParks = "1";
        const string Command_SelectPark = "2";
        const string Command_SelectCampground = "";
        const string Command_ReturnMainMenu = "r";
        const string Command_Quit = "q";


        public ParksReservationCLI()
        {

        }

        public void RunCLI()
        {
            PrintHeader();
            PrintMainMenu();
            while (true)
            {
                string command = Console.ReadLine();

                Console.Clear();

                switch (command.ToLower())
                {
                    case Command_DisplayParks:
                        //DisplayParks();
                        break;
                    case Command_SelectPark:
                        //DisplayParks();
                        SelectParkMenu();
                        break;
                    case Command_Quit:
                        Console.WriteLine("Thank you for using the National Parks Campsite Reservation System!");
                        return;

                    default:
                        Console.WriteLine("The command provided was not a valid command, please try again.");
                        break;
                }
                PrintMainMenu();
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

        private void PrintMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Main-Menu Type in a command");
            Console.WriteLine(" 1 - Get a list of all National Parks");
            Console.WriteLine(" 2 - Select a Park to find a Campground");
            Console.WriteLine(" Q - Quit");
        }

        private void SelectParkMenu()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(" Input Park ID to Display Campgrounds at that Park");
            Console.WriteLine(" R - Return to Main Menu");
        }
    }
}
