using Microsoft.EntityFrameworkCore;
using QMAService.Model.Entities;
using QMAService.Repository.Data;
using QMAService.Repository.Interface;

namespace QMAService.Repository.Services;

public class QuantityMeasurementRepository : IQuantityMeasurementRepository
{
    private readonly QuantityDbContext _context;

    public QuantityMeasurementRepository(QuantityDbContext context)
    {
        _context = context;
    }

    public void Save(QuantityMeasurementEntity entity)
    {
        _context.Measurements.Add(entity);
        _context.SaveChanges();
    }

    public List<QuantityMeasurementEntity> GetAll()
    {
        return _context.Measurements
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ToList();
    }

    public QuantityMeasurementEntity? GetById(int id)
    {
        return _context.Measurements.FirstOrDefault(x => x.Id == id);
    }

    public void Delete(int id)
    {
        var item = _context.Measurements.FirstOrDefault(x => x.Id == id);
        if (item == null) return;

        _context.Measurements.Remove(item);
        _context.SaveChanges();
    }

    public int Count()
    {
        return _context.Measurements.Count();
    }
}