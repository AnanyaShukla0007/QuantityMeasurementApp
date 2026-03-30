using System.Collections.Generic;
using QuantityMeasurementApp.Model.Entities;

namespace QuantityMeasurementApp.Repository.Interface
{
    public interface IQuantityMeasurementRepository
    {
        void Save(QuantityMeasurementEntity entity);

        List<QuantityMeasurementEntity> GetAll();

        void Clear();

        int GetTotalCount();
    }
}