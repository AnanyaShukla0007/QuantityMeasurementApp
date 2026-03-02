using System;
using QuantityMeasurementApp.Model.DTOs;

namespace QuantityMeasurementApp.Business.Validators
{
    public static class RequestValidator
    {
        public static void Validate(ConvertRequestDto dto)
        {
            if (dto == null)
                throw new ArgumentException("Request cannot be null.");

            if (string.IsNullOrWhiteSpace(dto.FromUnit))
                throw new ArgumentException("FromUnit is required.");

            if (string.IsNullOrWhiteSpace(dto.ToUnit))
                throw new ArgumentException("ToUnit is required.");

            if (!double.IsFinite(dto.Value))
                throw new ArgumentException("Invalid numeric value.");
        }

        public static void Validate(EqualityRequestDto dto)
        {
            if (dto == null)
                throw new ArgumentException("Request cannot be null.");

            if (string.IsNullOrWhiteSpace(dto.Unit1) ||
                string.IsNullOrWhiteSpace(dto.Unit2))
                throw new ArgumentException("Units are required.");

            if (!double.IsFinite(dto.Value1) ||
                !double.IsFinite(dto.Value2))
                throw new ArgumentException("Invalid numeric values.");
        }

        public static void Validate(ArithmeticRequestDto dto)
        {
            if (dto == null)
                throw new ArgumentException("Request cannot be null.");

            if (string.IsNullOrWhiteSpace(dto.Unit1) ||
                string.IsNullOrWhiteSpace(dto.Unit2))
                throw new ArgumentException("Units are required.");

            if (!double.IsFinite(dto.Value1) ||
                !double.IsFinite(dto.Value2))
                throw new ArgumentException("Invalid numeric values.");
        }
    }
}