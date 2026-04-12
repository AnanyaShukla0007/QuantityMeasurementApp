using QuantityMeasurementApp.Model.DTOs;
using QuantityMeasurementApp.Model.Entities;
using System.Collections.Generic;

namespace QuantityMeasurementApp.Business.Interface
{
    public interface IQuantityMeasurementService
    {
        QuantityResponse CompareQuantities(BinaryQuantityRequest request, string username);

        QuantityResponse ConvertQuantity(ConversionRequest request, string username);

        QuantityResponse AddQuantities(BinaryQuantityRequest request, string username);

        QuantityResponse SubtractQuantities(BinaryQuantityRequest request, string username);

        DivisionResponse DivideQuantities(BinaryQuantityRequest request, string username);

        List<QuantityMeasurementEntity> GetAllHistory();

        List<QuantityMeasurementEntity> GetErroredHistory();

        int GetTotalOperations();
    }
}