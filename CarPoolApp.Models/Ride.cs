using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPoolApp.Models
{
    public class Ride
    {
        public Ride()
        {
            
            BookingRequest = new List<Booking>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string RideId { get; set; }

        public DateTime StartTime { get; set; }
        public string Status { get; set; }
        public double PricePerKm { get; set; }
        public int AvailableSeat { get; set; }
        public ICollection<City> Route { get; set; }
        public ICollection<Booking> BookingRequest { get; set; }
        public virtual User User { get; set; }
        public string UserId { get; set; }
       
    }
}
