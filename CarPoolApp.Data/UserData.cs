using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarPoolApp.Models;

namespace CarPoolApp.Data
{
    public class UserData
    {
        public void AddUser(User user)
        {
            using (var db = new CarPoolContext())
            {
                db.Add(user);
                db.SaveChanges();
            }
        }

        public User GetUserById(string userId)
        {
            using (var db = new CarPoolContext())
            {
                return db.Users.Where(user => user.UserId == userId).SingleOrDefault();
            }
        }
    }
}
