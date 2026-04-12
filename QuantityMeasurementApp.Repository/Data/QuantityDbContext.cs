using Microsoft.EntityFrameworkCore;
using QuantityMeasurementApp.Model.Entities;

namespace QuantityMeasurementApp.Repository.Data
{
    public class QuantityDbContext : DbContext
    {
        public QuantityDbContext(DbContextOptions<QuantityDbContext> options)
            : base(options)
        {
        }

        public DbSet<QuantityMeasurementEntity> Measurements { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<QuantityMeasurementEntity>()
                .ToTable("Measurements");

            modelBuilder.Entity<UserEntity>()
                .ToTable("Users");
        }
    }
}