using ApartmentRental.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentRental.Infrastructure.Context
{
    internal class MainContext : DbContext
    {
        public DbSet<Apartment> Apartment { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<LandLord> LandLord { get; set; }
        public DbSet<Tenant> Tenant { get; set; }
        public DbSet<Address> Address { get; set; }
        public MainContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=dbo.ApartmentRental.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apartment>()
                .HasMany(x => x.Images)
                .WithOne(x => x.Apartment)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LandLord>()
                .HasMany(x => x.Apartments)
                .WithOne(x => x.Landlord)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
