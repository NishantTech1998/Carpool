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
        public string Id { get; set; }
        [ForeignKey("Ride")]
        public string RideId { get; set; }
        public string Status { get; set; }
        public string Source { get; set; }
        public int SeatsBooked { get; set; }
        public string Destination { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
    }
}
