using System;
using QuantityMeasurementApp.Model.DTOs;

namespace QuantityMeasurementApp.Business.Validators
{
    public static class RequestValidator
    {
        public static void ValidateQuantity(QuantityDTO quantity)
        {
            if (quantity == null)
                throw new ArgumentException("Quantity cannot be null.");

            if (string.IsNullOrWhiteSpace(quantity.Unit))
                throw new ArgumentException("Unit is required.");

            if (double.IsNaN(quantity.Value) || double.IsInfinity(quantity.Value))
                throw new ArgumentException("Invalid numeric value.");
        }

        public static void ValidateBinary(BinaryQuantityRequest request)
        {
            if (request == null)
                throw new ArgumentException("Request cannot be null.");

            ValidateQuantity(request.Quantity1);
            ValidateQuantity(request.Quantity2);
        }

        public static void ValidateConversion(ConversionRequest request)
        {
            if (request == null)
                throw new ArgumentException("Request cannot be null.");

            ValidateQuantity(request.Source);

            if (string.IsNullOrWhiteSpace(request.TargetUnit))
                throw new ArgumentException("Target unit is required.");
        }
    }
}