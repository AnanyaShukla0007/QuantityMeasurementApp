using QuantityMeasurementApp.Business.Interface;
using QuantityMeasurementApp.Model.DTOs;

namespace QuantityMeasurementApp.Controller.Controllers
{
    public class QuantityMeasurementController
    {
        private readonly IQuantityMeasurementService _service;

        public QuantityMeasurementController(IQuantityMeasurementService service)
        {
            _service = service;
        }

        public QuantityResponse PerformComparison(QuantityDTO q1, QuantityDTO q2)
        {
            return _service.CompareQuantities(
                new BinaryQuantityRequest { Quantity1 = q1, Quantity2 = q2 });
        }

        public QuantityResponse PerformConversion(QuantityDTO source, string targetUnit)
        {
            return _service.ConvertQuantity(
                new ConversionRequest { Source = source, TargetUnit = targetUnit });
        }

        public QuantityResponse PerformAddition(QuantityDTO q1, QuantityDTO q2)
        {
            return _service.AddQuantities(
                new BinaryQuantityRequest { Quantity1 = q1, Quantity2 = q2 });
        }

        public QuantityResponse PerformSubtraction(QuantityDTO q1, QuantityDTO q2)
        {
            return _service.SubtractQuantities(
                new BinaryQuantityRequest { Quantity1 = q1, Quantity2 = q2 });
        }

        public DivisionResponse PerformDivision(QuantityDTO q1, QuantityDTO q2)
        {
            return _service.DivideQuantities(
                new BinaryQuantityRequest { Quantity1 = q1, Quantity2 = q2 });
        }
    }
}