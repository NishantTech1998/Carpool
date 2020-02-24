using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApp.Models;
using CarPoolApp.Services;
using CarPoolApp.Data;

namespace CarPoolApp.UI
{
    class BookingUI
    {
        static string activeUser;

        public static void BookRide()
        {
            string Source,Destination;
            activeUser = UserUI.activeUser;
            GeoService geoService = new GeoService();
            Booking booking = new Booking();
            Console.Clear();
            booking.Id = "Bid" + activeUser.Substring(0, 3) + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;

            do
            {
                Console.WriteLine("Enter Your Source Location");
                Source = Console.ReadLine().NotEmptyValidator().NameValidator();
                if (geoService.IsCityAvailable(Source))
                {
                    booking.Source = Source;
                }
            } while (!geoService.IsCityAvailable(Source));

            do
            {
                Console.WriteLine("Enter Your Destination Location");
                Destination = Console.ReadLine().NotEmptyValidator().NameValidator();
                if (geoService.IsCityAvailable(Destination))
                {
                    booking.Destination = Destination;
                }
            } while (!geoService.IsCityAvailable(Destination));

            Console.WriteLine("Prefer Same Gender? {Y}:Yes {N}:No");
            string GenderPreference = Console.ReadLine().NotEmptyValidator().YesNOValidator();

            RideUI.ViewAvailableRide(booking.Source, booking.Destination,GenderPreference);
            Console.WriteLine("\nEnter the ride Id to Book or B to go back");
            string Response = Console.ReadLine().NotEmptyValidator();
            
           
            if (Response == "B")
                Program.UserChoices();
            else
            {
                RideService rideService = new RideService(new RideData(), new ViaPointData());
                BookingService bookingServices = new BookingService(new BookingData(),new ViaPointData());
                Ride ride = rideService.GetRideByRideId(Response);
                if (ride == null) { Console.WriteLine("Wrong Ride Id");Console.ReadKey();Program.UserChoices(); }
                
                Console.WriteLine("\nEnter the total seats you want");
                booking.SeatsBooked = int.Parse(Console.ReadLine().NotEmptyValidator().DigitValidator());
                booking.RideId = ride.Id;
                booking.UserId = activeUser;
                booking.Status = "Waiting";
                if (bookingServices.GetAvailableSeatAtSource(booking) < booking.SeatsBooked)
                {
                    Console.WriteLine("Seats exceeds available");
                    Console.ReadKey();
                    Program.UserChoices();
                }
                if (bookingServices.CreateBooking(booking))
                {
                    Console.WriteLine("Booking Created Successfully");
                }
                else
                {
                    Console.WriteLine("Error Encountered During Booking,You have booked this already");
                }
                Console.WriteLine("Press any key to go back");
                Console.ReadKey();
                Program.UserChoices();
            }
        }

        public static void ViewBookingsForMyRides()
        {
            RideService rideService = new RideService(new RideData(), new ViaPointData());
            Console.Clear();
            activeUser = UserUI.activeUser;
            List<Ride> myRides = rideService.GetMyRides(activeUser);
            foreach (Ride createdRide in myRides)
            {
                Console.WriteLine($"Ride Id :{createdRide.Id}");
                Console.WriteLine($"Ride Start:{createdRide.StartTime}");
                Console.WriteLine($"PricePerKm{createdRide.PricePerKm}\n");
            }

            Console.WriteLine("\n\nEnter Ride Id To View Bookings or {B} : Go Back");
            string value = Console.ReadLine().NotEmptyValidator();
            if (value != "B")
            {
                BookingService bookingService = new BookingService(new BookingData(), new ViaPointData());
                UserService userService = new UserService(new UserData());
                List<Booking> bookings = bookingService.GetBookingByRideId(value);
                if (bookings.Count > 0)
                {
                    foreach (Booking booking in bookings)
                    {
                        User user = userService.GetProfile(booking.UserId);
                        Console.WriteLine($"Booking Id :{booking.Id}");
                        Console.WriteLine($"Booked Seats :{booking.SeatsBooked}");
                        Console.WriteLine($"Source :{booking.Source}");
                        Console.WriteLine($"Destination :{booking.Destination}");
                        Console.WriteLine($"Name :{user.FirstName}");
                        Console.WriteLine($"Contact :{user.ContactNumber}");
                        Console.WriteLine($"Status :{booking.Status}\n");
                    }
                    ConfirmBookingsOnRides();
                }
                else
                {
                    Console.WriteLine("No any Bookings Found or Invalid Ride Id");
                }
            }
            Console.ReadKey();
            Program.UserChoices();
        }

        public static void ConfirmBookingsOnRides()
        {
            BookingService bookingService = new BookingService(new BookingData(), new ViaPointData());
            Console.WriteLine("Enter Booking Id to Update or B to go back");
            string value = Console.ReadLine().NotEmptyValidator();
            if (value != "B")
            {
                bool isUpdated;
                Console.WriteLine("\n{Y} : Accept   {N} : Reject");
                string response = Console.ReadLine().NotEmptyValidator().YesNOValidator();

                if (response == "Y")
                    isUpdated=bookingService.UpdateBookingStatus(value, true);
                else
                    isUpdated = bookingService.UpdateBookingStatus(value, false);
                if (!isUpdated)
                { Console.WriteLine("Booking Id Not Present"); }
            }
            
            Program.UserChoices();
        }

        public static void ViewMyBookings()
        {
            BookingService bookingService = new BookingService(new BookingData(), new ViaPointData());
            RideService rideService = new RideService(new RideData(), new ViaPointData());
            UserService userService = new UserService(new UserData());
            Console.Clear();
            List<Booking> bookings = bookingService.GetMyBookings(UserUI.activeUser);

            foreach (Booking booking in bookings)
            {
                User user = userService.GetProfile(rideService.GetRideByRideId(booking.RideId).UserId);
                Console.WriteLine($"Booking Id :{booking.Id}");
                Console.WriteLine($"Ride Id :{booking.RideId}");
                Console.WriteLine($"Source :{booking.Source}");
                Console.WriteLine($"Destination :{booking.Destination}");
                Console.WriteLine($"Name :{user.FirstName}");
                Console.WriteLine($"Contact :{user.ContactNumber}");
                Console.WriteLine($"Status :{booking.Status}\n");
            }

            Console.WriteLine("Enter Booking Id to delete or B to go back");
            string value = Console.ReadLine().NotEmptyValidator();
            if (value != "B")
            {
                Console.WriteLine("{D} :Delete {any key} : Go Back");
                ConsoleKey response = Console.ReadKey().Key;
                if (response == ConsoleKey.D)
                {

                    if (!bookingService.CancelBooking(value))
                    {
                        Console.WriteLine("Wrong Id");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Your ride is cancelled");
                        Console.ReadKey();
                    }
                }
            }
            Program.UserChoices();
        }
    }

       
    
}
