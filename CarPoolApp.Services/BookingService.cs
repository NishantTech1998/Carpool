using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CarPoolApp.Services.IServices;

namespace CarPoolApp.Services
{
    public class BookingService:IBookingService
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
                UpdateAvailableSeat(booking,false);
                return true;
            }
        }

        public void UpdateAvailableSeat(Booking booking,bool isCancel)
        {
            using (var db = new CarPoolContext())
            {
                int sourceId = db.Cities.Where(c => c.CityName == booking.Source).Select(c => c.Id).First();
                int destinationId = db.Cities.Where(c => c.CityName == booking.Destination).Select(c => c.Id).First();

                foreach (City city in db.Cities.Where(c => c.RideID == booking.RideId))
                {
                    if (city.Id >= sourceId && city.Id < destinationId)
                    {
                        if (isCancel == true)
                            city.SeatAvailable += booking.SeatsBooked;
                        else
                            city.SeatAvailable -= booking.SeatsBooked;
                    }
                }
                db.SaveChanges();
            }
        }

        public int GetAvailableSeatAtSource(Booking booking)
        {
            using (var db = new CarPoolContext())
            {
                List<City> cities = db.Cities.Where(c => c.RideID == booking.RideId).ToList();
                City SourceCity = cities.Where(c => c.CityName == booking.Source).Single();
                City DestinationCity = cities.Where(c => c.CityName == booking.Destination).Single();
                int seat=10;
                foreach(City city in cities)
                {
                    if(city.Id>=SourceCity.Id&&city.Id<DestinationCity.Id)
                    {
                        if (seat >= city.SeatAvailable)
                            seat = city.SeatAvailable;
                    }
                }
                return seat;                
            }
        }

        public bool CancelBooking(string bookingId)
        {
            Booking booking;
            using (var db = new CarPoolContext())
            {
                booking = db.Bookings.Where(b => b.Id == bookingId).SingleOrDefault();
                if (booking != null)
                {
                    UpdateAvailableSeat(booking, true);
                    db.Bookings.Remove(booking);
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }

        public List<Booking> GetBookingByRideId(string RideId)
        {
            List<Booking> Booking;
            using (var db = new CarPoolContext())
            {
                Booking = db.Bookings.Where(b =>b.RideId == RideId).ToList();
            }
            return Booking;
        }

        public bool UpdateBookingStatus(string bookingId, bool isConfirm)
        {

            using (var db = new CarPoolContext())
            {
                Booking booking = db.Bookings.Where(b => b.Id == bookingId).SingleOrDefault();
                if (booking != null)
                {
                    if (isConfirm == true)
                        booking.Status = "Confirm";
                    else
                        booking.Status = "Reject";
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }

        public List<Booking> GetMyBookings(string userId)
        {
            List<Booking> BookingList;
            using (var db = new CarPoolContext())
            {
                BookingList = db.Bookings.Where(b => b.UserId == userId).ToList();
            }
            return BookingList;
        }

    }
}
