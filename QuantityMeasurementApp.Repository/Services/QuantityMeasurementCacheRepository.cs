using QuantityMeasurementApp.Model.Entities;
using QuantityMeasurementApp.Repository.Interface;

namespace QuantityMeasurementApp.Repository.Services
{
    public class QuantityMeasurementCacheRepository : IQuantityMeasurementRepository
    {
        private static QuantityMeasurementCacheRepository? _instance;

        private readonly List<QuantityMeasurementEntity> _cache;

        private QuantityMeasurementCacheRepository()
        {
            _cache = new List<QuantityMeasurementEntity>();
        }

        public static QuantityMeasurementCacheRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new QuantityMeasurementCacheRepository();
            }

            return _instance;
        }

        public void Save(QuantityMeasurementEntity entity)
        {
            _cache.Add(entity);
        }

        public List<QuantityMeasurementEntity> GetAll()
        {
            return _cache.ToList();
        }

        public void Clear()
        {
            _cache.Clear();
        }
    }
}