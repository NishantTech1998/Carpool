using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApp.Models;

namespace CarPoolApp.Repository.DataInterfaces
{
    public interface IRideRepository
    {
        void AddRide(Ride ride);
        void RemoveRide(Ride ride);
        Ride GetRideByRideId(string rideId);
        List<Ride> GetRidesByUserId(string userId);
        List<Ride> GetAllRides();
    }
}
