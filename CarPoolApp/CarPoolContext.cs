using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using CarPoolApp.Models;

namespace CarPoolApp
{
    public class CarPoolContext:DbContext
    {
        private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=CarPool;Trusted_Connection=True;";

        public CarPoolContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString,b=>b.MigrationsAssembly("CarPoolApp"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasOne(e => e.Car).WithOne(e => e.User).HasForeignKey<Car>(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Car>().ToTable("Users");
            modelBuilder.Entity<User>().HasMany(r => r.Rides).WithOne(u => u.User).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasMany(b => b.Bookings).WithOne(u => u.User).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Ride>().HasMany(r => r.Route).WithOne(r => r.Ride).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Ride>().HasMany(b => b.BookingRequest).WithOne(r => r.Ride).OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}
