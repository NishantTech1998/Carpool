using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApp.Models;
using CarPoolApp.Data;

namespace CarPoolApp.Services
{
    public class BookingServices
    {
        public bool CreateBooking(Booking booking)
        {
            BookingData bookingData = new BookingData();
            //try
            //{
                bookingData.AddBooking(booking);
            //}
            //catch(Exception)
            //{
                return false;
           // }
            //return true;
        }

        public void CancelBooking() { }
        public void UpdateBooking() { }
        public void ViewBookings() { }

    }
}
