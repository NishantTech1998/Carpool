using CarPoolApp.UI;
using System;
using System.Collections.Generic;
using CarPoolApp.Enums;
using CarPoolApp.Helper;
using CarPoolApp.Repository.DataInterfaces;
using CarPoolApp.Repository;
using CarPoolApp.Services;
using CarPoolApp.Services.IServices;


namespace CarPoolApp
{
    class Program
    {
 
        static void ViewBookingMenu()
        {
            Console.Clear();
            int response;
            BookingUI bookingUI = new BookingUI();
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
                    bookingUI.ViewBookingsForMyRides();
                    break;

                case BookingMenu.ViewMyBookings:
                    bookingUI.ViewMyBookings();
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
            RideUI rideUI = new RideUI();
            BookingUI bookingUI = new BookingUI();
            UserUI userUI = new UserUI();
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
                    rideUI.CreateRide(); 
                    break;

                case UserMenu.BookRide:
                    bookingUI.BookRide();
                    break;

                case UserMenu.ViewBookingsMenu:
                    ViewBookingMenu();
                    break;

                case UserMenu.ViewMyRides:
                    rideUI.ViewMyRides();
                    break;

                case UserMenu.ViewMyProfile:
                    userUI.ViewProfile(); 
                    break;

                case UserMenu.LogOut:
                    userUI.HomePage();
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
            DependencyResolver.Container.Register<IUserRepository, UserRepository>();
            DependencyResolver.Container.Register<IRideService, RideService>();
            DependencyResolver.Container.Register<IBookingService, BookingService>();
            DependencyResolver.Container.Register<IRideRepository, RideRepository>();
            DependencyResolver.Container.Register<IViaPointRepository, ViaPointRepository>();
            DependencyResolver.Container.Register<IBookingRepository, BookingRepository>();
            DependencyResolver.Container.Verify();
            UserUI userUI = new UserUI();
            userUI.HomePage();
            Console.ReadKey();
        }
    }
}
