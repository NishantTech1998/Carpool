using System;
//using CarPoolApp.Models;
//using CarPoolApp.Services;

namespace CarPoolApp
{
    class Program
    {
//        static string activeUser;
//        static void HomePage()
//        {
//            ConsoleKey Response;
//            do
//            {
//                Console.Clear();
//                Console.WriteLine("\t\t\t\t\tCAR POOl APPLICATION");
//                Console.WriteLine("1.Log In\n2.Sign Up");
//                Response = Console.ReadKey().Key;
//                if (Response == ConsoleKey.D1) { LogIn(); }
//                else if (Response == ConsoleKey.D2) { SignUp(); }
//                else { }
//            } while (Response != ConsoleKey.D1 && Response != ConsoleKey.D2);
//        }
//        static void SignUp()
//        {
//            User user = new User();
//            Console.Clear();
//            Console.WriteLine("First Name");
//            user.FirstName = Console.ReadLine();
//            Console.WriteLine("Last Name");
//            user.LastName = Console.ReadLine();
//            Console.WriteLine("Date Of Birth");
//            user.Dob = DateTime.Parse(Console.ReadLine()).Date;
//            Console.WriteLine("Aadhar Number");
//            user.AadharNumber = Console.ReadLine();
//            Console.WriteLine("Contact Number");
//            user.ContactNumber = Console.ReadLine();
//            Console.WriteLine("Email ID");
//            user.Email = Console.ReadLine();
//            Console.WriteLine("Address Line 1");
//            user.CurrentAddress.FirstLine = Console.ReadLine();
//            Console.WriteLine("Address Line 2");
//            user.CurrentAddress.SecondLine = Console.ReadLine();
//            Console.WriteLine("City");
//            user.CurrentAddress.City = Console.ReadLine();
//            Console.WriteLine("State");
//            user.CurrentAddress.State = Console.ReadLine();
//            Console.WriteLine("Pincode");
//            user.CurrentAddress.Pincode = Console.ReadLine();
//            Console.Clear();
//            Console.WriteLine("Car Details\n\n");
//            Console.WriteLine("Car Brand");
//            user.Car.Brand = Console.ReadLine();
//            Console.WriteLine("Car Model");
//            user.Car.Model = Console.ReadLine();
//            Console.WriteLine("Car Color");
//            user.Car.Color = Console.ReadLine();
//            Console.WriteLine("Total Seats excluding driver");
//            user.Car.TotalSeat = Int32.Parse(Console.ReadLine());
//            Console.WriteLine("License Number");
//            //user.Car.CarId = Console.ReadLine();
//            //Console.WriteLine("User ID");
//            //user.UserId = Console.ReadLine();
//            //Console.WriteLine("PassWord");
//            //user.Password = Console.ReadLine();
//            //user.CurrentAddress.UserID = user.UserId;
//            //user.Car.OwnerID= user.UserId;
//           // UserService userService = new UserService();
//           // if (userService.SignUp(user))
//          //  { Console.WriteLine("Added Successfully");HomePage(); }
//          //  else
//          //  { SignUp(); }
//        }

//        static void LogIn()
//        {
//            Console.Clear();
//            Console.WriteLine("User ID");
//            string UserID = Console.ReadLine();
//            Console.WriteLine("PassWord");
//            string Password = Console.ReadLine();
//            UserService userService = new UserService();
//            //if(userService.Login(UserID,Password))
//            //{
//            //    activeUser = UserID;
//            //    UserMenu();
//            //}
//            //else
//            //{
//            //    HomePage();
//            //}
//        }

//        static void ViewProfile()
//        {
//            UserService userService = new UserService();
//            Console.Clear();
//           // User user = userService.GetProfile(activeUser);
//            //Console.WriteLine($"Name :{user.FirstName+" "+user.LastName}");
//            //Console.WriteLine($"User ID :{user.ID}");
//            //Console.WriteLine($"BirthDate :{user.Dob}");
//            //Console.WriteLine($"Email ID :{user.EmailID}");
//            //Console.WriteLine($"Contact Number :{user.ContactNumber}");
//            //Console.WriteLine("\n\nCar Details");
//            //Console.WriteLine($"Car Brand :{user.Car.Brand}");
//            //Console.WriteLine($"Car Model :{user.Car.Model}");
//            //Console.WriteLine($"Car Color :{user.Car.Color}");
//            //Console.WriteLine($"Car License :{user.Car.ID}");
//            Console.ReadKey();
//        }

//        static void CreateRide()
//        {
//            Ride ride = new Ride();
//            City cities = new City();
//            UserService userService=new UserService();
//           // User user = userService.GetProfile(activeUser);
//            ride.RideId = "R" + activeUser.Substring(0, 3) + DateTime.Now.Hour+ DateTime.Now.Minute;
//            GeoService geoService = new GeoService();
//            string city;
//            Console.Clear();
//            do
//            {
//                Console.WriteLine("Enter Source city");
//                city = Console.ReadLine();
//                if(geoService.IsCityAvailable(city))
//                {
//                    cities.CityName = city;
//                    cities.RideID = ride.RideId;
//                    ride.Route.Add(cities);
//                }
//            } while (!geoService.IsCityAvailable(city));
//            ConsoleKey Response;
//            do
//            {
//                Console.WriteLine("Add via points? (Y/N)");
//                Response = Console.ReadKey().Key;
//                if (Response == ConsoleKey.N)
//                    break;
//                do
//                {
//                    Console.WriteLine("\nEnter ViaPoint city");
//                    city = Console.ReadLine();
//                    if (geoService.IsCityAvailable(city))
//                    {
//                        cities.CityName = city;
//                        cities.RideID = ride.RideId;
//                        ride.Route.Add(cities);
//                    }
//                } while (!geoService.IsCityAvailable(city));
//            } while (Response==ConsoleKey.Y||Response!=ConsoleKey.N);

//            do
//            {
//                Console.WriteLine("Enter Destination city");
//                city = Console.ReadLine();
//                if (geoService.IsCityAvailable(city))
//                {
//                    cities.CityName = city;
//                    cities.RideID = ride.RideId;
//                    ride.Route.Add(cities);
//                }
//            } while (!geoService.IsCityAvailable(city));

//            Console.WriteLine("\nEnter Start Date and Time");
//            ride.StartTime= DateTime.Parse(Console.ReadLine());

//            Console.WriteLine("Enter Price per km");
//            ride.PricePerKm = Decimal.Parse(Console.ReadLine());
//         //   ride.Car = user.Car;

//            Console.WriteLine("Enter Available Seat");
//            ride.AvailableSeat = Int32.Parse(Console.ReadLine());

//            ride.UserId = activeUser;
//            RideService rideService = new RideService();
//            rideService.CreateRide(ride);
//        }

//        static void UserMenu()
//        {
//            Console.Clear();
//            Console.WriteLine("1.Create a Ride\n2.Book a Car\n3.View Bookings\n4.View Rides\n5.View Profile\n6.Delete Account\n7.Log Out");
//            ConsoleKey Response = Console.ReadKey().Key;
//            switch (Response)
//            {
//                case ConsoleKey.D1:CreateRide();UserMenu();
//                    break;
//                case ConsoleKey.D5: ViewProfile(); UserMenu(); 
//                    break;
//                case ConsoleKey.D7: HomePage();
//                    break;
//            }
            
//        }

        static void Main(string[] args)
        {
//            HomePage();
            
            Console.ReadKey();
        }
    }
}
