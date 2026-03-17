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
    }
}