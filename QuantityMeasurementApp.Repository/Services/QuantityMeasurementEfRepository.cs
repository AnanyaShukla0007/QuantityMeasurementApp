// QuantityMeasurementEfRepository.cs

using System.Linq;
using QuantityMeasurementApp.Repository.Interface;
using QuantityMeasurementApp.Model.Entities;
using QuantityMeasurementApp.Repository.Data;

namespace QuantityMeasurementApp.Repository.Services
{
    public class QuantityMeasurementEfRepository : IQuantityMeasurementRepository
    {
        private readonly QuantityDbContext _context;

        public QuantityMeasurementEfRepository(QuantityDbContext context)
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
                .OrderByDescending(x => x.Timestamp)
                .ToList();
        }

        public List<QuantityMeasurementEntity> GetByUsername(string username)
        {
            return _context.Measurements
                .Where(x => x.Username == username)
                .OrderByDescending(x => x.Timestamp)
                .ToList();
        }

        public void Clear()
        {
            _context.Measurements.RemoveRange(_context.Measurements);
            _context.SaveChanges();
        }

        public int GetTotalCount()
        {
            return _context.Measurements.Count();
        }
    }
}