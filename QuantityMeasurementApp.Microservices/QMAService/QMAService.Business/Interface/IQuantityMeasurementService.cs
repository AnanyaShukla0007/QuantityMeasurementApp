using QMAService.Model.DTOs;
using QMAService.Model.Entities;

namespace QMAService.Business.Interface;

public interface IQuantityMeasurementService
{
    QuantityResponse ConvertQuantity(ConversionRequest request);
    QuantityResponse AddQuantities(BinaryQuantityRequest request);
    QuantityResponse SubtractQuantities(BinaryQuantityRequest request);
    List<QuantityMeasurementEntity> GetAllHistory();
}