using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CarPoolApp.Models
{
    public class Booking
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string BookingId { get; set; }
        public virtual Ride Ride { get; set; }
        public string RideId { get; set; }
        public string Status { get; set; }
        public string Source { get; set; }
        public int SeatsBooked { get; set; }
        public string Destination { get; set; }
        public virtual User User { get; set; }
        public string UserId { get; set; }
    }
}
