using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApp.Models;

namespace CarPoolApp.Data.DataInterfaces
{
    public interface IUserData
    {
        User GetUserById(string userId);
        void AddUser(User user);
        void RemoveUser(string userId);
        void UpdateUser(User user);
    }
}
