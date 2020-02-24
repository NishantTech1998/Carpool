using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CarPoolApp.Models;
using CarPoolApp.Data.DataInterfaces;

namespace CarPoolApp.Data
{
    public class UserData:IUserData
    {
        public User GetUserById(string userId)
        {
            using (var db = new CarPoolContext())
            {
                return db.Users.Where(user => user.Id == userId).Include(s => s.Car).SingleOrDefault();
            }
        }

        public void AddUser(User user)
        {
            using (var db = new CarPoolContext())
            {
                db.Add(user);
                db.SaveChanges();
            }
        }

        public void RemoveUser(string userId)
        {
            using (var db = new CarPoolContext())
            {
                db.Users.Remove(GetUserById(userId));
                foreach (Ride ride in db.Rides)
                {
                    if (ride.UserId == userId)
                        db.Rides.Remove(ride);
                    foreach (ViaPoint city in db.Cities)
                    {
                        if (city.RideID == ride.Id)
                            db.Cities.Remove(city);
                    }
                }



                foreach (Booking booking in db.Bookings)
                {
                    if (booking.UserId == userId)
                        db.Bookings.Remove(booking);
                }

                db.SaveChanges();
            }
        }

        public void UpdateUser(User user)
        {
            using (var db = new CarPoolContext())
            {
                User usertoupdate = db.Users.Include(c => c.Car).Where(u => u.Id == user.Id).Single();
                usertoupdate.FirstName = user.FirstName;
                usertoupdate.LastName = user.LastName;
                usertoupdate.Email = user.Email;
                usertoupdate.ContactNumber = user.ContactNumber;
                usertoupdate.Car.Brand = user.Car.Brand;
                usertoupdate.Car.Model = user.Car.Model;
                usertoupdate.Car.Color = user.Car.Color;
                usertoupdate.Car.VehicleNumber = user.Car.VehicleNumber;
                usertoupdate.Car.TotalSeats = user.Car.TotalSeats;
                db.SaveChanges();
            }
        }
    }
}
