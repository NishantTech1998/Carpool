using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApp.Models;
using CarPoolApp.Data;

namespace CarPoolApp.Services
{
    public class BookingService
    {
        public bool CreateBooking(Booking booking)
        {
            BookingData bookingData = new BookingData();
            bool isSuccess;
            try
            {
                isSuccess=bookingData.AddBooking(booking);
            }
            catch(Exception)
            {
                return false;
            }
            return isSuccess;
        }

        public void DeleteBooking(Booking booking)
        {

        }
        
        public List<Booking> GetBookingRequest(string userId)
        {
            BookingData bookingData = new BookingData();
            return bookingData.GetBookingRequest(userId);
        }

        public void AcceptBooking(Booking booking,string status)
        {
            BookingData bookingData = new BookingData();
            bookingData.UpdateBookingStatus(booking,status);
        }

        public List<Booking> GetMyBookings(string userId)
        {
            BookingData bookingData = new BookingData();
            return bookingData.GetMyBookings(userId);
        }

        public int GetAvailableSeatAtSource(Booking booking)
        {
            BookingData bookingData = new BookingData();
            return bookingData.GetAvailableSeatAtSource(booking);
        }
    }
}
