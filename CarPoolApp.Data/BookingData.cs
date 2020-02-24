using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarPoolApp.Models;
using CarPoolApp.Data.DataInterfaces;

namespace CarPoolApp.Data
{
    public class BookingData:IBookingData
    {
        public void AddBooking(Booking booking)
        {
            using (var db = new CarPoolContext())
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
            }
        }

        public List<Booking> GetBookingsByUserIdAndRideId(Booking booking)
        {
            using (var db = new CarPoolContext())
            {
                return db.Bookings.Where(b => b.UserId == booking.UserId && b.RideId == booking.RideId).ToList();
            }
        }

        public List<Booking> GetBookingsByRideId(string rideId)
        {
            using (var db = new CarPoolContext())
            {
                return db.Bookings.Where(b => b.RideId == rideId).ToList();
            }
        }

        public List<Booking> GetBookingsByUserId(string userId)
        {
            using (var db = new CarPoolContext())
            {
                return db.Bookings.Where(b => b.UserId == userId).ToList();
            }
        }

        public Booking GetBookingbyBookingId(string bookingId)
        {
            using (var db = new CarPoolContext())
            {
                return db.Bookings.Where(b => b.Id == bookingId).SingleOrDefault();
            }
        }

        public List<Booking> GetAllBookings()
        {
            using (var db = new CarPoolContext())
            {
                return db.Bookings.ToList();
            }
        }

        public void CancelBooking(Booking booking)
        {
            using (var db = new CarPoolContext())
            {
                db.Bookings.Remove(booking);
                db.SaveChanges();
            }
        }

        public void UpdateBookingStatus(Booking booking)
        {
            using (var db = new CarPoolContext())
            {
                Booking currentBooking = db.Bookings.Where(b => b.Id == booking.Id).SingleOrDefault();
                currentBooking.Status = booking.Status;
                db.SaveChanges();
            }
        }
    }
}
