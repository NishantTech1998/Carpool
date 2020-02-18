using CarPoolApp.UI;
using System;
using System.Collections.Generic;


namespace CarPoolApp
{
    class Program
    {
 
        static void ViewBookingMenu()
        {
            Console.Clear();
            Console.WriteLine("1.View Booking request\n2.View My Bookings\n");
            ConsoleKey ResponseForViewBooking = Console.ReadKey().Key;
            switch(ResponseForViewBooking)
            {
                case ConsoleKey.D1:
                    BookingUI.ViewBookingsForMyRides(UserUI.activeUser);
                    break;
                case ConsoleKey.D2:
                    BookingUI.ViewMyBookings(UserUI.activeUser);
                    break;
            }
        }

        public static void UserMenu()
        {
            Console.Clear();
            Console.WriteLine("Hi There, Select any option from menu\n\n");
            Console.WriteLine("1.Create a Ride\n2.Book a Car\n3.View Bookings\n4.View Rides\n5.View Profile\n6.Log Out");
            ConsoleKey Response = Console.ReadKey().Key;
            switch (Response)
            {
                case ConsoleKey.D1:
                    RideUI.CreateRide(); UserMenu();
                    break;
                case ConsoleKey.D2:
                    BookingUI.BookRide();UserMenu();
                    break;
                case ConsoleKey.D3:
                    ViewBookingMenu();UserMenu();
                    break;
                case ConsoleKey.D4:
                    RideUI.ViewMyRides();UserMenu();
                    break;
                case ConsoleKey.D5:
                    UserUI.ViewProfile(); UserMenu();
                    break;
                case ConsoleKey.D6:
                    UserUI.HomePage();
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
