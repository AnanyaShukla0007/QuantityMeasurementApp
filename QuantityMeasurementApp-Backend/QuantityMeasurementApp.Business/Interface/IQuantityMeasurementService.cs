using QuantityMeasurementApp.Model.DTOs;
using QuantityMeasurementApp.Model.Entities;
using System.Collections.Generic;

namespace QuantityMeasurementApp.Business.Interface
{
    public interface IQuantityMeasurementService
    {
        QuantityResponse CompareQuantities(BinaryQuantityRequest request);

        QuantityResponse ConvertQuantity(ConversionRequest request);

        QuantityResponse AddQuantities(BinaryQuantityRequest request);

        QuantityResponse SubtractQuantities(BinaryQuantityRequest request);

        DivisionResponse DivideQuantities(BinaryQuantityRequest request);

        List<QuantityMeasurementEntity> GetAllHistory();

        List<QuantityMeasurementEntity> GetErroredHistory();

        int GetTotalOperations();
    }
}