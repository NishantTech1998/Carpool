using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApp.Models;

namespace CarPoolApp.Repository.DataInterfaces
{
    public interface IUserRepository
    {
        User GetUserById(string userId);
        void AddUser(User user);
        void RemoveUser(string userId);
        void UpdateUser(User user);
    }
}
