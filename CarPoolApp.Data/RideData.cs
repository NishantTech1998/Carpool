using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarPoolApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CarPoolApp.Data
{
    public class RideData
    {
        public List<string> GetRideIdByRoute(string source, string destination)
        {
            using (var db = new CarPoolContext())
            {
                List<string> RideId1 = db.Cities.Where(c => c.CityName == source && c.SeatAvaible > 0).Select(r => r.RideID).ToList();
                List<string> RideId2 = db.Cities.Where(c => c.CityName == destination).Select(r => r.RideID).ToList();
                return RideId1.Intersect(RideId2).ToList();
            }
        }

        public List<Ride> GetRideByRoute(string source, string destination)
        {
            List<Ride> AvailableRide = new List<Ride>();
            List<string> AvailableRideId = GetRideIdByRoute(source, destination);
            using (var db = new CarPoolContext())
            {
                foreach (string id in AvailableRideId)
                {
                    AvailableRide.Add(db.Rides.Where(r => r.RideId == id).Include(r => r.User).ThenInclude(r=>r.Car).SingleOrDefault());
                }
            }
            return AvailableRide;
        }

        public void AddNewRide(Ride ride)
        {
            using (var db = new CarPoolContext())
            {
                db.Rides.Add(ride);
                foreach (City c in ride.Route)
                {
                    db.Cities.Add(c);
                }
                db.SaveChanges();
            }

        }

    }
}
