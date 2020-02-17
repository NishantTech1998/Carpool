using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApp.Models;
using CarPoolApp.Data;
using System.Linq;

namespace CarPoolApp.Services
{
    public class BookingService
    {

        public bool CreateBooking(Booking booking)
        {
            using (var db = new CarPoolContext())
            {
                if (db.Bookings.Count() > 0)
                {
                    if ((db.Bookings.Where(b => b.UserId == booking.UserId && b.RideId == booking.RideId).Count() > 0) || GetAvailableSeatAtSource(booking) < 1)
                        return false;
                }
                db.Bookings.Add(booking);
                db.SaveChanges();
                UpdateAvailableSeat(booking);
                return true;
            }
        }

        public void UpdateAvailableSeat(Booking booking)
        {
            using (var db = new CarPoolContext())
            {
                int sourceId = db.Cities.Where(c => c.CityName == booking.Source).Select(c => c.Id).First();
                int destinationId = db.Cities.Where(c => c.CityName == booking.Destination).Select(c => c.Id).First();

                foreach (City city in db.Cities.Where(c => c.RideID == booking.RideId))
                {
                    if (city.Id >= sourceId && city.Id < destinationId)
                        city.SeatAvailable -= booking.SeatsBooked;
                }
                db.SaveChanges();
            }
        }

        public int GetAvailableSeatAtSource(Booking booking)
        {
            using (var db = new CarPoolContext())
            {
                List<City> cities = db.Cities.Where(c => c.RideID == booking.RideId).ToList();
                
            }
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
