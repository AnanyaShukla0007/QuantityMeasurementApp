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
            try
            {
                _context.Measurements.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public List<QuantityMeasurementEntity> GetAll()
        {
            try
            {
                return _context.Measurements
                    .OrderByDescending(x => x.Timestamp)
                    .ToList();
            }
            catch
            {
                return new List<QuantityMeasurementEntity>();
            }
        }

        public List<QuantityMeasurementEntity> GetByUsername(string username)
        {
            try
            {
                return _context.Measurements
                    .Where(x => x.Username == username)
                    .OrderByDescending(x => x.Timestamp)
                    .ToList();
            }
            catch
            {
                return new List<QuantityMeasurementEntity>();
            }
        }

        public void Clear()
        {
            try
            {
                _context.Measurements.RemoveRange(_context.Measurements);
                _context.SaveChanges();
            }
            catch
            {
            }
        }

        public int GetTotalCount()
        {
            try
            {
                return _context.Measurements.Count();
            }
            catch
            {
                return 0;
            }
        }
    }
}