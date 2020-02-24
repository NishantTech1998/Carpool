using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CarPoolApp.Services.IServices;
using CarPoolApp.Data.DataInterfaces;

namespace CarPoolApp.Services
{
    public class RideService:IRideService
    {
        readonly IRideData _rideData;
        readonly IViaPointData _viaPointData;
        public RideService(IRideData dataService,IViaPointData viaPointData)
        {
            _rideData = dataService;
            _viaPointData = viaPointData;
        }

        public bool CreateRide(Ride ride)
        {
                try
                {
                    _rideData.AddRide(ride);
                }

                catch(Exception)
                {
                    return false;
                }

                return true;
          
        }

        public void CancelRide(Ride ride)
        {
            _rideData.RemoveRide(ride);
        }

        public List<Ride> GetMyRides(string userId)
        {
           return _rideData.GetRidesByUserId(userId);
        }

        public List<Ride> GetRideByRoute(string source, string destination)
        {
            List<Ride> AvailableRide = new List<Ride>();
                ViaPoint Source = null;
                List < Ride > Rides= _rideData.GetAllRides();

               foreach(ViaPoint city in _viaPointData.GetAllViaPoints())
                {
                    if (city.CityName == source)
                        Source = city;

                    if (city.CityName == destination && Source != null && city.RideID == Source.RideID && city.Id > Source.Id)
                        AvailableRide.Add(Rides.Where(r => r.Id == Source.RideID).SingleOrDefault());
                }
            return AvailableRide;
        }

        public Ride GetRideByRideId(string rideId)
        {
            return _rideData.GetRideByRideId(rideId);
        }

    }
}
