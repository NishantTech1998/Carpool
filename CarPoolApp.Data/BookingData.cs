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
        public bool AddBooking(Booking booking)
        {
            using (var db = new CarPoolContext())
            {
                if (db.Bookings.Count()> 0)
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
                int sourceId = db.Cities.Where(c => c.CityName == booking.Source).Select(c => c.ID).First();
                int destinationId = db.Cities.Where(c => c.CityName == booking.Destination).Select(c => c.ID).First();
                foreach (City city in db.Cities.Where(c=>c.RideID==booking.RideId))
                {
                    if(city.CityName==booking.Source||city.CityName==booking.Destination)
                    { city.SeatAvaible -= 1; }
                    else if(city.CityName!=booking.Source && city.CityName!=booking.Destination && city.ID >sourceId && city.ID<destinationId)
                    { city.SeatAvaible -= 1; }
                    else
                    { city.SeatAvaible -= 0; }
                }
                db.SaveChanges();
            }
        }

        public int GetAvailableSeatAtSource(Booking booking)
        {
            using (var db = new CarPoolContext())
            {
                List<City> cities = db.Cities.Where(c => c.RideID == booking.RideId).ToList();
                int indexOfSource = cities.FindIndex(c => c.CityName == booking.Source);
                int indexOfDestination = cities.FindIndex(c => c.CityName == booking.Destination);
                if (cities[indexOfSource].SeatAvaible < cities[indexOfDestination].SeatAvaible && indexOfSource + 1 == indexOfDestination)
                    return cities[indexOfDestination].SeatAvaible;
                else if (cities[indexOfSource].SeatAvaible >= cities[indexOfDestination].SeatAvaible && indexOfSource + 1 == indexOfDestination)
                    return cities[indexOfSource].SeatAvaible;
                else if (cities[indexOfSource].SeatAvaible >= cities[indexOfDestination].SeatAvaible && indexOfSource + 1 != indexOfDestination)
                {
                    int i;
                    for (i = indexOfSource; i <= indexOfDestination; i++)
                    {
                        if (cities[indexOfSource].SeatAvaible < cities[i].SeatAvaible)
                            break;
                    }
                    return cities[i].SeatAvaible;
                }
                else
                {
                    int i;
                    for (i = indexOfSource; i <= indexOfDestination; i++)
                    {
                        if (cities[indexOfSource].SeatAvaible < cities[i].SeatAvaible)
                            break;
                    }
                    return cities[i].SeatAvaible;
                }
            }
        }

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
                Booking request = db.Bookings.Where(b => b.BookingId == booking.BookingId).SingleOrDefault();
                if (status == "accept") 
                request.Status = "Confirm";
                else
                request.Status = "Reject";
                db.SaveChanges();
            }
        }
    }
}
