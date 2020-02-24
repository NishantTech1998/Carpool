using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApp.Models;
using System.Linq;
using CarPoolApp.Data.DataInterfaces;

namespace CarPoolApp.Data
{
    public class RideData:IRideData
    {
        public void AddRide(Ride ride)
        {
            using (var db = new CarPoolContext())
            {

                db.Rides.Add(ride);

                foreach (ViaPoint c in ride.ViaPoints)
                {
                    db.Cities.Add(c);
                }
                db.SaveChanges();
            }
        }

        public void RemoveRide(Ride ride)
        {
            using (var db = new CarPoolContext())
            {
                db.Rides.Remove(ride);

                foreach (ViaPoint city in db.Cities)
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

        public Ride GetRideByRideId(string rideId)
        {
            using (var db = new CarPoolContext())
            {
                return db.Rides.Where(r => r.Id == rideId).SingleOrDefault();
            }
        }

        public List<Ride> GetRidesByUserId(string userId)
        {
            using (var db = new CarPoolContext())
            {
                return db.Rides.Where(r => r.UserId == userId).ToList();
            }
        }

        public List<Ride> GetAllRides()
        {
            using (var db = new CarPoolContext())
            {
                return db.Rides.ToList();
            }
        }

        
    }
}
