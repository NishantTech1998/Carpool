using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarPoolApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CarPoolApp.Data
{
    public class BookingData
    {
        

        

        

        public List<Booking> GetBookingRequest(string userId)
        {
            List<Booking> Request;
            using (var db = new CarPoolContext())
            {
                Request = db.Bookings.Include(b=>b.Ride).Include(b=>b.User).Where(b => b.Ride.UserId ==userId).ToList();
            }
            return Request;
        }

        public List<Booking> GetMyBookings(string userId)
        {
            List<Booking> Request;
            using (var db = new CarPoolContext())
            {
                Request = db.Bookings.Include(b => b.Ride).ThenInclude(b => b.User).Where(b => b.UserId == userId).ToList();
            }
            return Request;
        }

        public void UpdateBookingStatus(Booking booking,string status)
        {
            
            using (var db = new CarPoolContext())
            {
                Booking request = db.Bookings.Where(b => b.Id == booking.Id).SingleOrDefault();
                if (status == "accept") 
                request.Status = "Confirm";
                else
                request.Status = "Reject";
                db.SaveChanges();
            }
        }
    }
}
