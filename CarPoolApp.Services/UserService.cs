using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CarPoolApp.Services.IServices;
using CarPoolApp.Repository.DataInterfaces;
using CarPoolApp.Repository;
using SimpleInjector;
using CarPoolApp.Helper;

namespace CarPoolApp.Services
{
    public class UserService : IUserService
    {
        readonly IUserRepository _userData;
        
        public UserService()
        {
            _userData = DependencyResolver.Get<UserRepository>();
        }

        public bool Login(string userID, string password)
        {
            User user = _userData.GetUserById(userID);
            return ((user != null && user.Password == password));
        }

        public User GetProfile(string userId)
        {
            return _userData.GetUserById(userId);
        }

        public void DeleteUser(string userId)
        {
            _userData.RemoveUser(userId);
        }

        public void UpdateProfile(User user)
        {
            _userData.UpdateUser(user);
        }

        public bool SignUp(User user)
        {
            try
            {
                _userData.AddUser(user);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    
    }
}
