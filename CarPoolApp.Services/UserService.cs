using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApp.Models;
using System.Linq;
using CarPoolApp.Data;

namespace CarPoolApp.Services
{
    public class UserService
    {
        readonly UserData userData = new UserData();

        public bool Login(string userID, string password)
        {
            User user = userData.GetUserById(userID);
            return (user == null || (user != null && user.Password == password));
        }

        public bool SignUp(User user)
        {
            try
            {
                userData.AddUser(user);
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }

        public void ForgetPassword() { }

        public User GetProfile(string userId)
        {
            return userData.GetUserById(userId);
        }

     // public void UpdateProfile() { }
    }
}
