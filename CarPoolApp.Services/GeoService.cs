using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CarPoolApp.Services.IServices;

namespace CarPoolApp.Services
{
    public class GeoService:IGeoService
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
                    if (line.Split(',')[0] == city) {
                        count++;break;
                    }
                }
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<double> GetLatitudeAndLongitude(string city)
        {
            using (StreamReader file = new StreamReader(csvFile))
            {
                string line;
                List<double> Coordinates = new List<double>();
                while((line=file.ReadLine())!=null)
                {
                    if (line.Split(',')[0] == city)
                        break;
                }
                Coordinates.Add(double.Parse(line.Split(',')[1]));
                Coordinates.Add(double.Parse(line.Split(',')[2]));
                return Coordinates;
            }
        }

        public double Radians(double x)
        {
            return (x/57.29577951);
        }

        public double Distance(string source,string destination)
        {
            //const double RADIUS = 6371;
            List<double> Coordinates = new List<double>();
            Coordinates.AddRange(GetLatitudeAndLongitude(source));
            Coordinates.AddRange(GetLatitudeAndLongitude(destination));
            double latDistance = Radians(Coordinates[0] - Coordinates[2]);
            double lngDistance = Radians(Coordinates[1] - Coordinates[3]);
            double distanceInKm = 1.609344 * 396.30 * Math.Acos((Math.Sin(Coordinates[0]) * Math.Sin(Coordinates[2]))
                + Math.Cos(Coordinates[0]) * Math.Cos(Coordinates[2]) * Math.Cos(lngDistance));
            return distanceInKm;
        }
    }
}
