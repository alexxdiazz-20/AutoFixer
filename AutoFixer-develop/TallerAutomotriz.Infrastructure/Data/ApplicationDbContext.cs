using TallerAutomotriz.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TallerAutomotriz.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Mechanic> Mechanics { get; set; }
        public DbSet<ServiceOrder> ServiceOrders { get; set; }
        public DbSet<ServiceOrderDetail> ServiceOrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customer configuration
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Vehicles)
                .WithOne(v => v.Customer)
                .HasForeignKey(v => v.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Vehicle configuration
            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.ServiceOrders)
                .WithOne(so => so.Vehicle)
                .HasForeignKey(so => so.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Mechanic configuration
            modelBuilder.Entity<Mechanic>()
                .HasMany(m => m.ServiceOrders)
                .WithOne(so => so.Mechanic)
                .HasForeignKey(so => so.MechanicId)
                .OnDelete(DeleteBehavior.Restrict);

            // ServiceOrder configuration
            modelBuilder.Entity<ServiceOrder>()
                .HasMany(so => so.Details)
                .WithOne(sod => sod.ServiceOrder)
                .HasForeignKey(sod => sod.ServiceOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Service configuration
            modelBuilder.Entity<Service>()
                .HasMany(s => s.ServiceOrderDetails)
                .WithOne(sod => sod.Service)
                .HasForeignKey(sod => sod.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}