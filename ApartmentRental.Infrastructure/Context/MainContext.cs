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
        public DbSet<Apartment> { get; set; }
        public DbSet<Account> { get; set; }
        public DbSet<Image> { get; set; }
        public DbSet<Landlord> { get; set; }
        public DbSet<Tenant> { get; set; }
        public DbSet<Address> { get; set; }

        public MainContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=dbo.ApartmentRental.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apartment>()
                .HasMany(navigationExpression x: Apartment => x.Images)
                .WithOne(navigationExtensions x: Image => x.Apartment)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Landlord>()
                        .HasMany(navigationExpression x: Landlord => x.Apartments)
                        .WithOne(navigationExtensions x: Apartment => x.Landlord)
                        .OnDelete(DeleteBehavior.Cascade);
}
    }
}
