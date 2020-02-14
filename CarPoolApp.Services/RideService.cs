using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApp.Models;
using System.Linq;

namespace CarPoolApp.Services
{
    public class RideService
    {
        public void CreateRide(Ride ride)
        {
            //using (var db = new CarPoolContext( ))
            //{
            //    db.Rides.Add(ride);
            //    db.Cities.AddRange(ride.Route);
            //    db.SaveChanges();
            //}
        }

        public void CancelRide() { }
        public void UpdateRide() { }
        public void SearchRide() { }
        public void ViewOfferedRides() { }
    }
}
