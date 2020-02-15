using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPoolApp.Models
{
    public class Car
    {
        public string LicenseNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int TotalSeat { get; set; }
        public virtual User User { get; set; }
        [Key]
        public string UserId{ get; set; }
    }
}
