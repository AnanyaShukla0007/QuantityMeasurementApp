using QMAService.Model.Entities;

namespace QMAService.Repository.Interface;

public interface IQuantityMeasurementRepository
{
    void Save(QuantityMeasurementEntity entity);
    List<QuantityMeasurementEntity> GetAll();
    QuantityMeasurementEntity? GetById(int id);
    void Delete(int id);
    int Count();
}