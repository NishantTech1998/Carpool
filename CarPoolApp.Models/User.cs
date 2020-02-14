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
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public string AadharNumber { get; set; }
        public string Password { get; set; }
        public virtual Address CurrentAddress { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public virtual Car Car { get; set; }
        public virtual ICollection<Ride> Rides { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public User()
        {
            Rides = new List<Ride>();
            Bookings = new List<Booking>();
        }
    }
}
