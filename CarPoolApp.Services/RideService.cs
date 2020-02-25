using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CarPoolApp.Services.IServices;
using CarPoolApp.Repository.DataInterfaces;
using CarPoolApp.Repository;
using SimpleInjector;
using CarPoolApp.Helper;

namespace CarPoolApp.Services
{
    public class RideService:IRideService
    {
        readonly IRideRepository _rideData;
        readonly IViaPointRepository _viaPointData;

        public RideService()
        {
            _rideData = DependencyResolver.Get<RideRepository>();
            _viaPointData = DependencyResolver.Get<ViaPointRepository>();
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
