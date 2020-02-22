using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CarPoolApp.Services.IServices;

namespace CarPoolApp.Services
{
    public class UserService:IUserService
    { 

        public bool Login(string userID, string password)
        {
            User user = GetUserById(userID);
            return ((user != null && user.Password == password));
        }

        public User GetUserById(string userId)
        {
            using (var db = new CarPoolContext())
            {
                return db.Users.Where(user => user.Id == userId).Include(s => s.Car).SingleOrDefault();
            }
        }


        public User GetProfile(string userId)
        {
            return GetUserById(userId);
        }

        public void DeleteUser(string userId)
        {
            using (var db = new CarPoolContext())
            {
                db.Users.Remove(GetUserById(userId));

                foreach (Ride ride in db.Rides)
                {
                    if (ride.UserId == userId)
                        db.Rides.Remove(ride);
                    foreach(City city in db.Cities)
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

        public void UpdateProfile(User user)
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
        public bool SignUp(User user)
        {
            using (var db = new CarPoolContext())
            {
                try
                {
                    db.Add(user);
                }
                catch (Exception)
                {
                    return false;
                }
                db.SaveChanges();
                return true;
            }
        }
    }
}
