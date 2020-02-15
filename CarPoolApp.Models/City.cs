using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPoolApp.Models
{
    
    public class City
    {
       public int ID { get; set; }
       public string CityName { get; set; }
       public int SeatAvaible { get; set; }
       public virtual Ride Ride { get; set; }
       public string RideID { get; set; }
    }
}
