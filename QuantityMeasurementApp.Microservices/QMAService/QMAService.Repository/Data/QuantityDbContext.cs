using Microsoft.EntityFrameworkCore;
using QMAService.Model.Entities;

namespace QMAService.Repository.Data;

public class QuantityDbContext : DbContext
{
    public QuantityDbContext(DbContextOptions<QuantityDbContext> options) : base(options) { }

    public DbSet<QuantityMeasurementEntity> Measurements => Set<QuantityMeasurementEntity>();
}