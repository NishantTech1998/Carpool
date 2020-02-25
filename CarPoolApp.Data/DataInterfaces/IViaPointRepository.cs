using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApp.Models;

namespace CarPoolApp.Repository.DataInterfaces
{
    public interface IViaPointRepository
    {
        List<ViaPoint> GetAllViaPointsByBookedRideId(string rideId);
        List<ViaPoint> GetViaPointsByName(string name);
        void UpdateAvailableSeat(ViaPoint city, int updatedSeats);
        List<ViaPoint> GetAllViaPoints();
    }
}
