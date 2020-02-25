using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApp.Models;

namespace CarPoolApp.Services.IServices
{
    public interface IRideService
    {
        bool CreateRide(Ride ride);
        void CancelRide(Ride ride);
        List<Ride> GetMyRides(string userId);
        List<Ride> GetRideByRoute(string source, string destination);
        Ride GetRideByRideId(string rideId);
    }
}
