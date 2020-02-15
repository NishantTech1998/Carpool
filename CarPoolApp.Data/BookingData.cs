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
        public void AddBooking(Booking booking)
        {
            using (var db = new CarPoolContext())
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                UpdateAvailableSeat(booking);
            }
        }

        public void UpdateAvailableSeat(Booking booking)
        {
            using (var db = new CarPoolContext())
            {
                int sourceId = db.Cities.Where(c => c.CityName == booking.Source).Select(c => c.ID).First();
                foreach (City city in db.Cities.Where(c=>c.RideID==booking.RideId))
                {
                    if(city.CityName==booking.Source)
                    { city.SeatAvaible -= 1; }
                    else if(city.CityName!=booking.Source && city.CityName!=booking.Destination && city.ID >sourceId )
                    { city.SeatAvaible -= 1; }
                    else
                    { city.SeatAvaible -= 0; }
                }
                db.SaveChanges();
            }
        }

        public int GetAvailableSeat(Booking booking)
        {
            using (var db = new CarPoolContext())
            {
                return db.Cities.Where(c => c.CityName == booking.Source && c.RideID == booking.RideId).Select(c => c.SeatAvaible).First();
            }
        }
    }
}
