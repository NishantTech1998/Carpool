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
            const double PI = 3.14159265F;
            return (x * PI / 180);
        }

        public double Distance(string source,string destination)
        {
            const double RADIUS = 6371;
            List<double> Coordinates = new List<double>();
            Coordinates.AddRange(GetLatitudeAndLongitude(source));
            Coordinates.AddRange(GetLatitudeAndLongitude(destination));
            double latDistance = Radians(Coordinates[0] - Coordinates[2]);
            double lngDistance = Radians(Coordinates[1] - Coordinates[3]);
            double a = Math.Pow(Math.Sin(latDistance / 2), 2)
                + Math.Pow(Math.Sin(lngDistance / 2), 2)
                + Math.Cos(Radians(Coordinates[0]))
                * Math.Cos(Radians(Coordinates[2]));
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return RADIUS * c;
        }
    }
}
