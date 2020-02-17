using System;
using System.Collections.Generic;
using CarPoolApp.Models;
using CarPoolApp.Services;

namespace CarPoolApp
{
    class Program
    {
        static string activeUser;
        static void HomePage()
        {
            ConsoleKey Response;
            do
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t\tCAR POOl APPLICATION");
                Console.WriteLine("1.Log In\n2.Sign Up");
                Response = Console.ReadKey().Key;
                if (Response == ConsoleKey.D1) { LogIn(); }
                else if (Response == ConsoleKey.D2) { SignUp(); }
                else { }
            } while (Response != ConsoleKey.D1 && Response != ConsoleKey.D2);
        }
        static void SignUp()
        {
            User user = new User();
            Car car = new Car();
            Console.Clear();
            Console.WriteLine("First Name");
            user.FirstName = Console.ReadLine();
            Console.WriteLine("Last Name");
            user.LastName = Console.ReadLine();
            Console.WriteLine("Date Of Birth");
            user.Dob = DateTime.Parse(Console.ReadLine()).Date;
            Console.WriteLine("Aadhar Number");
            user.AadharNumber = Console.ReadLine();
            Console.WriteLine("Contact Number");
            user.ContactNumber = Console.ReadLine();
            Console.WriteLine("Email ID");
            user.Email = Console.ReadLine();
            Console.WriteLine("Address Line 1");
            user.CurrentAddress.FirstLine = Console.ReadLine();
            Console.WriteLine("Address Line 2");
            user.CurrentAddress.SecondLine = Console.ReadLine();
            Console.WriteLine("City");
            user.CurrentAddress.City = Console.ReadLine();
            Console.WriteLine("State");
            user.CurrentAddress.State = Console.ReadLine();
            Console.WriteLine("Pincode");
            user.CurrentAddress.Pincode = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Car Details\n\n");
            Console.WriteLine("Car Brand");
            car.Brand = Console.ReadLine();
            Console.WriteLine("Car Model");
            car.Model = Console.ReadLine();
            Console.WriteLine("Car Color");
            car.Color = Console.ReadLine();
            Console.WriteLine("Total Seats excluding driver");
            car.TotalSeat = Int32.Parse(Console.ReadLine());
            Console.WriteLine("License Number");
            car.LicenseNumber = Console.ReadLine();
            Console.WriteLine("User ID");
            user.UserId = Console.ReadLine();
            Console.WriteLine("PassWord");
            user.Password = Console.ReadLine();
            user.CurrentAddress.UserId = user.UserId;
            car.UserId= user.UserId;
            user.Car = car;
            UserService userService = new UserService();
            if (userService.SignUp(user))
            { Console.WriteLine("Added Successfully");HomePage(); }
            else
            { SignUp(); }
        }

        static void LogIn()
        {
            Console.Clear();
            Console.WriteLine("User ID");
            string UserID = Console.ReadLine();
            Console.WriteLine("PassWord");
            string Password = Console.ReadLine();
            UserService userService = new UserService();
            if(userService.Login(UserID,Password))
            {
                activeUser = UserID;
                UserMenu();
            }
            else
            {
                HomePage();
            }
        }

        static void ViewProfile()
        {
            UserService userService = new UserService();
            Console.Clear();
            User user = userService.GetProfile(activeUser);
            Console.WriteLine($"Name :{user.FirstName + " " + user.LastName}");
            Console.WriteLine($"User ID :{user.UserId}");
            Console.WriteLine($"BirthDate :{user.Dob}");
            Console.WriteLine($"Email ID :{user.Email}");
            Console.WriteLine($"Contact Number :{user.ContactNumber}");
            Console.WriteLine("\n\nCar Details");
            Console.WriteLine($"Car Brand :{user.Car.Brand}");
            Console.WriteLine($"Car Model :{user.Car.Model}");
            Console.WriteLine($"Car Color :{user.Car.Color}");
            Console.WriteLine($"Car License :{user.Car.LicenseNumber}");
            Console.ReadKey();
        }

        static void CreateRide()
        {
            Ride ride = new Ride();
            City cities;
            UserService userService=new UserService();
            User user = userService.GetProfile(activeUser);
            ride.RideId = "R" + activeUser.Substring(0, 3) + DateTime.Now.Hour + DateTime.Now.Minute;
            GeoService geoService = new GeoService();
            string city;
            Console.Clear();
            ride.Route = new List<City>();
            Console.WriteLine("Enter Available Seat");
            ride.AvailableSeat = Int32.Parse(Console.ReadLine());

            do
            {
                cities = new City();
                Console.WriteLine("Enter Source city");
                city = Console.ReadLine();
                if (geoService.IsCityAvailable(city))
                {
                    cities.CityName = city;
                    cities.RideID = ride.RideId;
                    cities.SeatAvaible = ride.AvailableSeat;
                    ride.Route.Add(cities);
                }
            } while (!geoService.IsCityAvailable(city));

            ConsoleKey Response;
            do
            {
                
                Console.WriteLine("Add via points? (Y/N)");
                Response = Console.ReadKey().Key;
                if (Response == ConsoleKey.N)
                    break;
                do
                {
                    cities = new City();
                    Console.WriteLine("\nEnter ViaPoint city");
                    city = Console.ReadLine();
                    if (geoService.IsCityAvailable(city))
                    {
                        cities.CityName = city;
                        cities.RideID = ride.RideId;
                        cities.SeatAvaible = ride.AvailableSeat;
                        ride.Route.Add(cities);
                    }
                } while (!geoService.IsCityAvailable(city));
            } while (Response == ConsoleKey.Y || Response != ConsoleKey.N);

            do
            {
                cities = new City();
                Console.WriteLine("Enter Destination city");
                city = Console.ReadLine();
                if (geoService.IsCityAvailable(city))
                {
                    cities.CityName = city;
                    cities.RideID = ride.RideId;
                    cities.SeatAvaible = ride.AvailableSeat;
                    ride.Route.Add(cities);
                }
            } while (!geoService.IsCityAvailable(city));

            Console.WriteLine("\nEnter Start Date and Time");
            ride.StartTime = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Enter Price per km");
            ride.PricePerKm = double.Parse(Console.ReadLine());

            
    
            ride.UserId = activeUser;
            RideService rideService = new RideService();
            if (rideService.CreateRide(ride))
            { Console.WriteLine("Added Successfully");
                Console.WriteLine("Press any key to go back");
                Console.ReadKey();
            }
            
        }

        static void BookRide()
        {
            GeoService geoService = new GeoService();
            Console.Clear();
            string Source;
            string Destination;
            Booking booking = new Booking();
            booking.BookingId="Bid"+activeUser.Substring(0, 3) + DateTime.Now.Hour + DateTime.Now.Minute+DateTime.Now.Second;
            do
            {
                Console.WriteLine("Enter Your Source Location");
                Source = Console.ReadLine();
                if (geoService.IsCityAvailable(Source))
                {
                    booking.Source = Source;
                }
            } while (!geoService.IsCityAvailable(Source));

            do
            {
                Console.WriteLine("Enter Your Destination Location");
                Destination = Console.ReadLine();
                if (geoService.IsCityAvailable(Destination))
                {
                    booking.Destination = Destination;
                }
            } while (!geoService.IsCityAvailable(Destination));

            ViewAvailableRide(booking.Source, booking.Destination);
            Console.WriteLine("\nEnter the ride no to Book or B to go back");
            string Response = Console.ReadLine();
            if (Response == "B")
                UserMenu();
            else
            {
                RideService rideService = new RideService();
                BookingService bookingServices = new BookingService();
                booking.RideId = rideService.SearchRide(booking.Source, booking.Destination)[int.Parse(Response) - 1].RideId;
                booking.UserId = activeUser;
                booking.Status = "Waiting";
                if(bookingServices.CreateBooking(booking))
                {
                    Console.WriteLine("Booking Created Successfully");
                }
                else
                {
                    Console.WriteLine("Error Encountered During Booking,You have booked this already");
                }
                Console.WriteLine("Press any key to go back");
                Console.ReadKey();
            }
        }

        public static void ViewAvailableRide(string source, string destination)
        {
            RideService rideService = new RideService();
            GeoService geoService = new GeoService();
            Console.Clear();
            List<Ride> AvailableRides = rideService.SearchRide(source, destination);
            Console.WriteLine($"From {source} to {destination} we have following rides for you\n");
            foreach (Ride ride in AvailableRides)
            {
                Console.WriteLine($"Ride Creator :{ride.User.FirstName}");
                Console.WriteLine($"Car Description :{ride.User.Car.Color} {ride.User.Car.Model} {ride.User.Car.Brand}");
                Console.WriteLine($"Total Distance :{geoService.Distance(source,destination)} KM");
                Console.WriteLine($"Price per km :{ride.PricePerKm}");
                Console.WriteLine($"Total Amount :{ride.PricePerKm*geoService.Distance(source,destination)}");
                Console.WriteLine($"Expected Start Time :{ride.StartTime}\n\n");
            }
        }

        static void ViewBookingMenu()
        {
            Console.Clear();
            Console.WriteLine("1.View Booking request\n2.View My Bookings\n");
            ConsoleKey ResponseForViewBooking = Console.ReadKey().Key;
            switch(ResponseForViewBooking)
            {
                case ConsoleKey.D1:
                    BookingRequest(activeUser);
                    break;
                case ConsoleKey.D2:
                    ViewMyBookings(activeUser);
                    break;
            }
        }

        static void BookingRequest(string userId)
        {
            BookingService bookingService = new BookingService();
            Console.Clear();
            List<Booking> requests = bookingService.GetBookingRequest(userId);
            foreach(Booking request in requests)
            {
                Console.WriteLine($"Booking Id :{request.BookingId}");
                Console.WriteLine($"Ride Id :{request.RideId}");
                Console.WriteLine($"Source :{request.Source}");
                Console.WriteLine($"Destination :{request.Destination}");
                Console.WriteLine($"Name :{request.User.FirstName}");
                Console.WriteLine($"Contact :{request.User.ContactNumber}");
                Console.WriteLine($"Status :{request.Status}\n");

            }

            Console.WriteLine("\nEnter the request no to accept or reject");
            string Response = Console.ReadLine();
            Console.WriteLine("\nEnter A to accept or R to Reject");
            ConsoleKey value = Console.ReadKey().Key;
            if (value == ConsoleKey.A)
            {
                bookingService.AcceptBooking(requests[int.Parse(Response) - 1], "accept");
            }
            else if (value == ConsoleKey.B)
            {
                bookingService.AcceptBooking(requests[int.Parse(Response) - 1], "reject");
            }
            else
                UserMenu();

        }

        static void ViewCreatedRides(string userId)
        {
            RideService rideService = new RideService();
            Console.Clear();
            List<Ride> myRides = rideService.GetCreatedRides(userId);
            foreach(Ride createdRide in myRides)
            {
                Console.WriteLine($"Ride Id :{createdRide.RideId}");
                Console.WriteLine($"Ride Start:{createdRide.StartTime}");
                Console.WriteLine($"PricePerKm{createdRide.PricePerKm}\n");
            }
            Console.WriteLine("Enter Ride no to delete or update or B to go back");
            string value = Console.ReadLine();
            Console.WriteLine("Enter D to delete or U to update");
            ConsoleKey response = Console.ReadKey().Key;
            if (response == ConsoleKey.D)
            {
                rideService.DeleteRide(myRides[int.Parse(value) - 1]);
            }
            else if (response == ConsoleKey.U)
            {
           //     UpdateRide();
            }
            else
                UserMenu();
        }

        static void ViewMyBookings(string userId)
        {
            BookingService bookingService = new BookingService();
            Console.Clear();
            List<Booking> bookings = bookingService.GetMyBookings(userId);
            foreach (Booking request in bookings)
            {
                Console.WriteLine($"Booking Id :{request.BookingId}");
                Console.WriteLine($"Ride Id :{request.RideId}");
                Console.WriteLine($"Source :{request.Source}");
                Console.WriteLine($"Destination :{request.Destination}");
                Console.WriteLine($"Name :{request.Ride.User.FirstName}");
                Console.WriteLine($"Contact :{request.Ride.User.ContactNumber}");
                Console.WriteLine($"Status :{request.Status}\n");
            }
            Console.ReadKey();
        }

        static void DeleteUser()
        {
            UserService userService = new UserService();
            Console.Clear();
            Console.WriteLine("Are You Sure You Want To Delete Your Account? {Y}:Yes  {N}:No");
            if (Console.ReadKey().Key == ConsoleKey.Y)
                userService.DeleteUser(activeUser);
        }

        static void UserMenu()
        {
            Console.Clear();
            Console.WriteLine("1.Create a Ride\n2.Book a Car\n3.View Bookings\n4.View Rides\n5.View Profile\n6.Delete Account\n7.Log Out");
            ConsoleKey Response = Console.ReadKey().Key;
            switch (Response)
            {
                case ConsoleKey.D1:
                    CreateRide(); UserMenu();
                    break;
                case ConsoleKey.D2:
                    BookRide();UserMenu();
                    break;
                case ConsoleKey.D3:
                    ViewBookingMenu();UserMenu();
                    break;
                case ConsoleKey.D4:
                    ViewCreatedRides(activeUser);UserMenu();
                    break;
                case ConsoleKey.D5:
                    ViewProfile(); UserMenu();
                    break;
                case ConsoleKey.D6:
                    DeleteUser();HomePage();
                    break;
                case ConsoleKey.D7:
                    HomePage();
                    break;
            }

        }

        static void Main(string[] args)
        {
            HomePage();
            
            Console.ReadKey();
        }
    }
}
