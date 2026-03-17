using QuantityMeasurementApp.Model.DTOs;

namespace QuantityMeasurementApp.Controller.Interfaces
{
    public interface IQuantityMeasurementController
    {
        void Compare(BinaryQuantityRequest request);

        void Convert(ConversionRequest request);

        void Add(BinaryQuantityRequest request);

        void Subtract(BinaryQuantityRequest request);

        void Divide(BinaryQuantityRequest request);

        void ShowHistory();

        void ClearHistory();
    }
}