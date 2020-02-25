using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApp.Models;
using CarPoolApp.Services;
using CarPoolApp.Services.IServices;
using CarPoolApp.Helper;
using CarPoolApp.Repository.DataInterfaces;
using CarPoolApp.Repository;
using SimpleInjector;

namespace CarPoolApp.UI
{
    class RideUI
    {
        static string activeUser;
        private IUserService userService;
        private IRideService rideService;
 
        public RideUI()
        {
            userService = DependencyResolver.Get<UserService>();
            rideService = DependencyResolver.Get<RideService>();
        }

        public void CreateRide()
        {
            Ride ride = new Ride();
            ViaPoint cities;
            activeUser = UserUI.activeUser;
            User user = userService.GetProfile(activeUser);

            if (user.Car.TotalSeats > 0)
            {
                ride.Id = "R" + activeUser.Substring(0, 3) + DateTime.Now.Hour + DateTime.Now.Minute;
                GeoService geoService = new GeoService();
                string city;
                Console.Clear();
                ride.ViaPoints = new List<ViaPoint>();

                do
                {
                    Console.WriteLine("Enter Available Seat");
                    ride.AvailableSeats = Int32.Parse(Console.ReadLine().DigitValidator());

                    if (ride.AvailableSeats > user.Car.TotalSeats)
                        Console.WriteLine("\nValue exceeds your's car capacity\n");

                } while (ride.AvailableSeats > user.Car.TotalSeats);

                do
                {
                    cities = new ViaPoint();

                    Console.WriteLine("Enter Source city");
                    city = Console.ReadLine().NotEmptyValidator().NameValidator();

                    if (geoService.IsCityAvailable(city))
                    {
                        cities.CityName = city;
                        cities.RideID = ride.Id;
                        cities.SeatAvailable = ride.AvailableSeats;
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
                        cities = new ViaPoint();
                        Console.WriteLine("\nEnter ViaPoint city");
                        city = Console.ReadLine().NotEmptyValidator().NameValidator();
                        if (geoService.IsCityAvailable(city))
                        {
                            cities.CityName = city;
                            cities.RideID = ride.Id;
                            cities.SeatAvailable = ride.AvailableSeats;
                            ride.ViaPoints.Add(cities);
                        }
                    } while (!geoService.IsCityAvailable(city));
                } while (Response == "Y" || Response != "N");

                do
                {
                    cities = new ViaPoint();
                    Console.WriteLine("Enter Destination city");
                    city = Console.ReadLine().NotEmptyValidator().NameValidator();
                    if (geoService.IsCityAvailable(city))
                    {
                        cities.CityName = city;
                        cities.RideID = ride.Id;
                        cities.SeatAvailable = ride.AvailableSeats;
                        ride.ViaPoints.Add(cities);
                    }
                } while (!geoService.IsCityAvailable(city));

                Console.WriteLine("\nEnter Start Date and Time { mm/dd/yyyy hh/mm/ss AM/PM}");
                ride.StartTime = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Enter Price per km");
                ride.PricePerKm = double.Parse(Console.ReadLine().NotEmptyValidator());



                ride.UserId = activeUser;
                if (rideService.CreateRide(ride))
                {
                    Console.WriteLine("Added Successfully");
                    Console.WriteLine("Press any key to go back");
                    Console.ReadKey();
                }
            }
            else { Console.WriteLine("You dont have a car"); Console.ReadKey(); }
            Program.UserChoices();
        }

        public void ViewAvailableRide(string source, string destination,string GenderPreference)
        {
            activeUser = UserUI.activeUser;
            GeoService geoService = new GeoService();
            Console.Clear();
            List<Ride> AvailableRides = rideService.GetRideByRoute(source, destination);
            List<Ride> RidesByPreference = new List<Ride>();
            if(GenderPreference=="Y")
            {
                foreach(Ride ride in AvailableRides)
                {
                    if (userService.GetProfile(ride.UserId).Gender == userService.GetProfile(activeUser).Gender)
                        RidesByPreference.Add(ride);
                }
            }
            else
            {
                RidesByPreference = AvailableRides;
            }
            Console.WriteLine($"From {source} to {destination} we have following rides for you\n");
            foreach (Ride ride in RidesByPreference)
            {
                if (ride.UserId != activeUser)
                {
                    User user = userService.GetProfile(ride.UserId);
                    Console.WriteLine($"Ride ID :{ride.Id}");
                    Console.WriteLine($"Ride Creator :{user.FirstName}");
                    Console.WriteLine($"Car Description :{user.Car.Color} {user.Car.Model} {user.Car.Brand}");
                    Console.WriteLine($"Total Distance :{geoService.Distance(source, destination)} KM");
                    Console.WriteLine($"Price per km :{ride.PricePerKm}");
                    Console.WriteLine($"Total Amount :{(ride.PricePerKm * geoService.Distance(source, destination))}");
                    Console.WriteLine($"Expected Start Time :{ride.StartTime}\n\n");
                }
            }
        }

        public void ViewMyRides()
        {
            activeUser = UserUI.activeUser;
            Console.Clear();
            List<Ride> myRides = rideService.GetMyRides(activeUser);
            foreach (Ride createdRide in myRides)
            {
                Console.WriteLine($"Ride Id :{createdRide.Id}");
                Console.WriteLine($"Ride Start:{createdRide.StartTime}");
                Console.WriteLine($"PricePerKm{createdRide.PricePerKm}\n");
            }
            Console.WriteLine("Enter Ride ID to delete or B to go back");
            string value = Console.ReadLine().NotEmptyValidator();
            if (value != "B")
            {
                Ride ride = rideService.GetRideByRideId(value);
                if (ride == null) { Console.WriteLine("No Such Ride Found");Console.ReadKey();Program.UserChoices(); }
                Console.WriteLine("{D} :Delete {any key} : Go Back");
                ConsoleKey response = Console.ReadKey().Key;
                if (response == ConsoleKey.D)
                {
                    rideService.CancelRide(ride);                  
                    Console.WriteLine("Your ride is cancelled");
                    Console.ReadKey();
                }   
            }
            Program.UserChoices();
        }

    }


    
}
