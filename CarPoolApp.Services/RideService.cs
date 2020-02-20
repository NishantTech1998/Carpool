using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CarPoolApp.Services
{
    public class RideService
    {
        public bool CreateRide(Ride ride)
        {
          using (var db = new CarPoolContext())
          {
                try
                {
                    db.Rides.Add(ride);

                    foreach (City c in ride.ViaPoints)
                    {
                        db.Cities.Add(c);
                    }
                }

                catch(Exception)
                {
                    return false;
                }

                db.SaveChanges();
                return true;
          }
        }

        public List<Ride> GetRideByRoute(string source, string destination)
        {
            List<Ride> AvailableRide = new List<Ride>();

            using (var db = new CarPoolContext())
            {
                City Source = null;
                List < Ride > Rides= db.Rides.ToList();

               foreach(City city in db.Cities)
                {
                    if (city.CityName == source)
                        Source = city;

                    if (city.CityName == destination && Source != null && city.RideID == Source.RideID && city.Id > Source.Id)
                        AvailableRide.Add(Rides.Where(r => r.Id == Source.RideID).SingleOrDefault());
                }
            }
            return AvailableRide;
        }

        public List<Ride> GetMyRides(string userId)
        {
            using (var db = new CarPoolContext())
            {
                return db.Rides.Where(r => r.UserId == userId).ToList();
            }
        }

        public void CancelRide(Ride ride)
        {
            using (var db = new CarPoolContext())
            {
                db.Rides.Remove(ride);

                foreach(City city in db.Cities)
                {
                    if (city.RideID == ride.Id)
                        db.Cities.Remove(city);
                }

                foreach (Booking booking in db.Bookings)
                {
                    if (booking.RideId == ride.Id)
                        db.Bookings.Remove(booking);
                }

                db.SaveChanges();
            }
        }

        public static Ride GetRideByRideId(string rideId)
        {
            using (var db = new CarPoolContext())
            {
                return db.Rides.Where(r => r.Id == rideId).SingleOrDefault();
            }
        }

    }
}
