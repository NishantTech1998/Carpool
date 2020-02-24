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
    public class BookingService:IBookingService
    {
        readonly IBookingData _bookingData;
        readonly IViaPointData _viaPointData;

        public BookingService(IBookingData bookingData,IViaPointData viaPointData)
        {
            _bookingData=bookingData;
            _viaPointData = viaPointData;
        }

        public bool CreateBooking(Booking booking)
        {
                if ((_bookingData.GetBookingsByUserIdAndRideId(booking).Count() > 0) || GetAvailableSeatAtSource(booking) < 1)
                  return false;
                _bookingData.AddBooking(booking);
                UpdateAvailableSeat(booking,false);
                return true;
        }

        public void UpdateAvailableSeat(Booking booking,bool isCancel)
        {
           
          int sourceId = _viaPointData.GetViaPointsByName(booking.Source).Select(c => c.Id).First();
          int destinationId = _viaPointData.GetViaPointsByName(booking.Destination).Select(c => c.Id).First();

          foreach (ViaPoint city in _viaPointData.GetAllViaPointsByBookedRideId(booking.RideId))
          {
              if (city.Id >= sourceId && city.Id < destinationId)
              {
                  if (isCancel == true)
                      city.SeatAvailable += booking.SeatsBooked;
                  else
                      city.SeatAvailable -= booking.SeatsBooked;
                  _viaPointData.UpdateAvailableSeat(city, city.SeatAvailable);
              }
          }
        }

        public int GetAvailableSeatAtSource(Booking booking)
        {

           List<ViaPoint> cities = _viaPointData.GetAllViaPointsByBookedRideId(booking.RideId);
           ViaPoint SourceCity = cities.Where(c => c.CityName == booking.Source).Single();
           ViaPoint DestinationCity = cities.Where(c => c.CityName == booking.Destination).Single();
           int seat=10;
           foreach(ViaPoint city in cities)
           {
               if(city.Id>=SourceCity.Id&&city.Id<DestinationCity.Id)
               {
                   if (seat >= city.SeatAvailable)
                       seat = city.SeatAvailable;
               }
           }
           return seat;                
        }

        public bool CancelBooking(string bookingId)
        {
               Booking booking = _bookingData.GetBookingbyBookingId(bookingId);
                if (booking != null)
                {
                    UpdateAvailableSeat(booking, true);
                    _bookingData.CancelBooking(booking);
                    return true;
                }
                else
                    return false;
           
        }

        public List<Booking> GetBookingByRideId(string rideId)
        {
       
            return _bookingData.GetBookingsByRideId(rideId);
        }

        public bool UpdateBookingStatus(string bookingId, bool isConfirm)
        {

                Booking booking = _bookingData.GetBookingbyBookingId(bookingId);
                if (booking != null)
                {
                    booking.Status = isConfirm == true ? "Confirm" : "Reject";
                _bookingData.UpdateBookingStatus(booking);
                    return true;
                }
                else
                    return false;
            
        }

        public List<Booking> GetMyBookings(string userId)
        {
            return _bookingData.GetBookingsByUserId(userId);
        }
    }
}
