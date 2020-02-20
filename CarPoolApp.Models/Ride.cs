using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPoolApp.Models
{
    public class Ride
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        public DateTime StartTime { get; set; }
        public string Status { get; set; }
        public double PricePerKm { get; set; }
        public List<City> ViaPoints = new List<City>();
        public int AvailableSeats { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
       
    }
}
