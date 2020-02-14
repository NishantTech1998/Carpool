using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CarPoolApp.Services
{
    public class GeoService
    {
        private const string csvFile = @"C:\Users\nishant.k\source\repos\Geolocation.csv";
        public bool IsCityAvailable(string city)
        {
            using (StreamReader file = new StreamReader(csvFile))
            {
                string line;
                int count=0;
                while((line=file.ReadLine())!=null)
                {
                    if (line.Split(',')[0] == city) { count++;break;}
                }
                if (count > 0)
                    return true;
                else
                    return false;
            }
        }
    }
}
