using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPoolApp.Models
{
   public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public string AadharNumber { get; set; }
        public string Password { get; set; }
        public Address CurrentAddress { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public Car Car { get; set; }
        public User()
        {
            CurrentAddress = new Address();
        }
    }
}
