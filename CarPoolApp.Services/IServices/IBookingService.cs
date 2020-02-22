using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApp.Models;

namespace CarPoolApp.Services.IServices
{
    public interface IBookingService
    {
        bool CreateBooking(Booking booking);
        void UpdateAvailableSeat(Booking booking, bool isCancel);
        int GetAvailableSeatAtSource(Booking booking);
        bool CancelBooking(string bookingId);
        List<Booking> GetBookingByRideId(string RideId);
        bool UpdateBookingStatus(string bookingId, bool isConfirm);
        List<Booking> GetMyBookings(string userId);
    }
}
