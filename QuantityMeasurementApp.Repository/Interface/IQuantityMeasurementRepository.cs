// IQuantityMeasurementRepository.cs

using System.Collections.Generic;
using QuantityMeasurementApp.Model.Entities;

namespace QuantityMeasurementApp.Repository.Interface
{
    public interface IQuantityMeasurementRepository
    {
        void Save(QuantityMeasurementEntity entity);

        List<QuantityMeasurementEntity> GetAll();

        List<QuantityMeasurementEntity> GetByUsername(string username);

        void Clear();

        int GetTotalCount();
    }
}