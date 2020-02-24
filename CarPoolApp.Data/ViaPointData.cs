using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarPoolApp.Models;
using CarPoolApp.Data.DataInterfaces;

namespace CarPoolApp.Data
{
    public class ViaPointData:IViaPointData
    {
        public List<ViaPoint> GetAllViaPointsByBookedRideId(string rideId)
        {
            using (var db = new CarPoolContext())
            {
                return db.Cities.Where(c => c.RideID == rideId).ToList();
            }
        }

        public List<ViaPoint> GetViaPointsByName(string name)
        {
            using (var db = new CarPoolContext())
            {
                return db.Cities.Where(c => c.CityName == name).ToList();
            }
        }

        public void UpdateAvailableSeat(ViaPoint city, int updatedSeats)
        {
            using (var db = new CarPoolContext())
            {
                ViaPoint City = db.Cities.Where(c => c.Id == city.Id).Single();
                City.SeatAvailable = updatedSeats;
                db.SaveChanges();
            }
        }

        public List<ViaPoint> GetAllViaPoints()
        {
            using (var db = new CarPoolContext())
            {
                return db.Cities.ToList();
            }
        }

    }
}
