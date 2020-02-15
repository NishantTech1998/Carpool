using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApp.Models;
using CarPoolApp.Data;
using System.Linq;

namespace CarPoolApp.Services
{
    public class RideService
    {
        readonly RideData rideData=new RideData();
        public bool CreateRide(Ride ride)
        {
            try
            {
                rideData.AddNewRide(ride);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public void CancelRide() { }

        public void UpdateRide() { }

        public List<Ride> SearchRide(string source,string destination)
        {
            return rideData.GetRideByRoute(source, destination);
        }

        public void ViewOfferedRides() { }
    }
}
