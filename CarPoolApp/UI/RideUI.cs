using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApp.Models;
using CarPoolApp.Services;

namespace CarPoolApp.UI
{
    class RideUI
    {
        static string activeUser;

        public static void CreateRide()
        {
            Ride ride = new Ride();
            City cities;
            activeUser = UserUI.activeUser;
            UserService userService = new UserService();
            User user = userService.GetProfile(activeUser);
            if (user.Car.TotalSeat != 0)
            {
                ride.Id = "R" + activeUser.Substring(0, 3) + DateTime.Now.Hour + DateTime.Now.Minute;
                GeoService geoService = new GeoService();
                string city;
                Console.Clear();
                ride.ViaPoints = new List<City>();
                do
                {
                    Console.WriteLine("Enter Available Seat");
                    ride.AvailableSeat = Int32.Parse(Console.ReadLine().MenuResponseValidator());
                    if (ride.AvailableSeat > user.Car.TotalSeat)
                        Console.WriteLine("\nValue exceeds your's car capacity\n");
                } while (ride.AvailableSeat > user.Car.TotalSeat);

                do
                {
                    cities = new City();
                    Console.WriteLine("Enter Source city");
                    city = Console.ReadLine().NotEmptyValidator().NameValidator();
                    if (geoService.IsCityAvailable(city))
                    {
                        cities.CityName = city;
                        cities.RideID = ride.Id;
                        cities.SeatAvailable = ride.AvailableSeat;
                        ride.ViaPoints.Add(cities);
                    }
                } while (!geoService.IsCityAvailable(city));

                string Response;
                do
                {

                    Console.WriteLine("Add via points? (Y/N)");
                    Response = Console.ReadLine().NotEmptyValidator().YesNOValidator();
                    if (Response == "N")
                        break;
                    do
                    {
                        cities = new City();
                        Console.WriteLine("\nEnter ViaPoint city");
                        city = Console.ReadLine().NotEmptyValidator().NameValidator();
                        if (geoService.IsCityAvailable(city))
                        {
                            cities.CityName = city;
                            cities.RideID = ride.Id;
                            cities.SeatAvailable = ride.AvailableSeat;
                            ride.ViaPoints.Add(cities);
                        }
                    } while (!geoService.IsCityAvailable(city));
                } while (Response == "Y" || Response != "N");

                do
                {
                    cities = new City();
                    Console.WriteLine("Enter Destination city");
                    city = Console.ReadLine().NotEmptyValidator().NameValidator();
                    if (geoService.IsCityAvailable(city))
                    {
                        cities.CityName = city;
                        cities.RideID = ride.Id;
                        cities.SeatAvailable = ride.AvailableSeat;
                        ride.ViaPoints.Add(cities);
                    }
                } while (!geoService.IsCityAvailable(city));

                Console.WriteLine("\nEnter Start Date and Time { mm/dd/yyyy hh/mm/ss AM/PM}");
                ride.StartTime = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Enter Price per km");
                ride.PricePerKm = double.Parse(Console.ReadLine().NotEmptyValidator());



                ride.UserId = activeUser;
                RideService rideService = new RideService();
                if (rideService.CreateRide(ride))
                {
                    Console.WriteLine("Added Successfully");
                    Console.WriteLine("Press any key to go back");
                    Console.ReadKey();
                }
            }
            else { Console.WriteLine("You dont have a car"); Console.ReadKey(); Program.UserMenu(); }
        }

        public static void ViewAvailableRide(string source, string destination)
        {
            RideService rideService = new RideService();
            UserService userService = new UserService();
            GeoService geoService = new GeoService();
            Console.Clear();
            List<Ride> AvailableRides = rideService.GetRideByRoute(source, destination);
            Console.WriteLine($"From {source} to {destination} we have following rides for you\n");
            foreach (Ride ride in AvailableRides)
            {
                User user = userService.GetProfile(ride.UserId);
                Console.WriteLine($"Ride Creator :{user.FirstName}");
                Console.WriteLine($"Car Description :{user.Car.Color} {user.Car.Model} {user.Car.Brand}");
                Console.WriteLine($"Total Distance :{geoService.Distance(source, destination)} KM");
                Console.WriteLine($"Price per km :{ride.PricePerKm}");
                Console.WriteLine($"Total Amount :{(ride.PricePerKm * geoService.Distance(source, destination))}");
                Console.WriteLine($"Expected Start Time :{ride.StartTime}\n\n");
            }
        }

        public static void ViewMyRides()
        {
            RideService rideService = new RideService();
            activeUser = UserUI.activeUser;
            Console.Clear();
            List<Ride> myRides = rideService.GetMyRides(activeUser);
            foreach (Ride createdRide in myRides)
            {
                Console.WriteLine($"Ride Id :{createdRide.Id}");
                Console.WriteLine($"Ride Start:{createdRide.StartTime}");
                Console.WriteLine($"PricePerKm{createdRide.PricePerKm}\n");
            }
            Console.WriteLine("Enter Ride Serial no to delete or B to go back");
            string value = Console.ReadLine().NotEmptyValidator();
            if (value != "B")
            {
                Console.WriteLine("{D} :Delete {any key} : Go Back");
                ConsoleKey response = Console.ReadKey().Key;
                if (response == ConsoleKey.D)
                {
                    try
                    {
                        rideService.CancelRide(myRides[int.Parse(value) - 1]);
                    }
                    catch(ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("\nRide Not Present");
                        Console.ReadKey();
                        ViewMyRides();
                    }
                    Console.WriteLine("Your ride is cancelled");
                    Console.ReadKey();
                }   
            }
            Program.UserMenu();
        }

    }


    
}
