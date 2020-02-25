using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApp.Models;

namespace CarPoolApp.Repository.DataInterfaces
{
    public interface IBookingRepository
    {
        void AddBooking(Booking booking);
        List<Booking> GetBookingsByUserIdAndRideId(Booking booking);
        List<Booking> GetBookingsByRideId(string rideId);
        List<Booking> GetBookingsByUserId(string userId);
        Booking GetBookingbyBookingId(string bookingId);
        void CancelBooking(Booking booking);
        void UpdateBookingStatus(Booking booking);
        List<Booking> GetAllBookings();
    }
}
