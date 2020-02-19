using CarPoolApp.UI;
using System;
using System.Collections.Generic;
using CarPoolApp.Enums;


namespace CarPoolApp
{
    class Program
    {
 
        static void ViewBookingMenu()
        {
            Console.Clear();
            int response;
            BookingMenu bookingMenu;
            Console.WriteLine("1.View Bookings For My Rides\n2.View My Bookings\n");
            string Response = (Console.ReadLine());
            Int32.TryParse(Response, out response);
            if (response > Enum.GetValues(typeof(UserMenu)).Length)
                response = 0;
            bookingMenu = (BookingMenu)(response);
            switch (bookingMenu)
            {
                case BookingMenu.ViewBookingForMyRides:
                    BookingUI.ViewBookingsForMyRides();
                    break;

                case BookingMenu.ViewMyBookings:
                    BookingUI.ViewMyBookings();
                    break;

                case BookingMenu.None:
                    Console.WriteLine("Invalid Choice");
                    Console.ReadKey();
                    ViewBookingMenu();
                    break;
            }
        }

        public static void UserChoices()
        {
            Console.Clear();
            int response;
            UserMenu userMenuChoice;
            Console.WriteLine("Hi There, Select any option from menu\n\n");
            Console.WriteLine("1.Create a Ride\n2.Book a Car\n3.View Bookings\n4.View Rides\n5.View Profile\n6.Log Out");
            string Response = (Console.ReadLine());
            Int32.TryParse(Response,out response);
            if (response > Enum.GetValues(typeof(UserMenu)).Length)
                response = 0;
            userMenuChoice = (UserMenu)(response);
            switch (userMenuChoice)
            {
                case UserMenu.CreateRide:
                    RideUI.CreateRide(); 
                    break;

                case UserMenu.BookRide:
                    BookingUI.BookRide();
                    break;

                case UserMenu.ViewBookingsMenu:
                    ViewBookingMenu();
                    break;

                case UserMenu.ViewMyRides:
                    RideUI.ViewMyRides();
                    break;

                case UserMenu.ViewMyProfile:
                    UserUI.ViewProfile(); 
                    break;

                case UserMenu.LogOut:
                    UserUI.HomePage();
                    break;

                case UserMenu.None:
                    Console.WriteLine("Invalid Choice");
                    Console.ReadKey();
                    UserChoices();
                    break;
            }

        }

        static void Main(string[] args)
        {
            UserUI.HomePage();
            Console.ReadKey();
        }
    }
}
