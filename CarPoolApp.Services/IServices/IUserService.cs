using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApp.Models;

namespace CarPoolApp.Services.IServices
{
    public interface IUserService
    {
        bool Login(string userID, string password);
        User GetUserById(string userId);
        User GetProfile(string userId);
        void DeleteUser(string userId);
        void UpdateProfile(User user);
        bool SignUp(User user);
    }
}
