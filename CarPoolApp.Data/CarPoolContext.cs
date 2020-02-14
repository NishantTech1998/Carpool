using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using CarPoolApp.Models;

namespace CarPoolApp.Data
{
    public class CarPoolContext:DbContext
    {
        private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=CarPool;Trusted_Connection=True;";

        public CarPoolContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<User>().HasOne(e => e.Car).WithOne(e => e.User).HasForeignKey<User>(e => e.CarID);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}
