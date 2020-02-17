using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPoolApp.Models
{
    
    public class City
    {
       public int Id { get; set; }
       public string CityName { get; set; }
       public int SeatAvailable { get; set; }
       [ForeignKey("Ride")]
       public string RideID { get; set; }
    }
}
