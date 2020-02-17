﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPoolApp.Models
{
    
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        public string FirstLine { get; set; }
        public string SecondLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string UserId { get; set; }
    }
}
