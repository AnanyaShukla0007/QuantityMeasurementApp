using QuantityMeasurementApp.Model.DTOs;

namespace QuantityMeasurementApp.Business.Interface
{
    public interface IQuantityMeasurementService
    {
        QuantityResponse CompareQuantities(BinaryQuantityRequest request);

        QuantityResponse ConvertQuantity(ConversionRequest request);

        QuantityResponse AddQuantities(BinaryQuantityRequest request);

        QuantityResponse SubtractQuantities(BinaryQuantityRequest request);

        DivisionResponse DivideQuantities(BinaryQuantityRequest request);
    }
}