using System;
using System.Collections.Generic;
using System.Linq;

using QuantityMeasurementApp.Business.Exceptions;
using QuantityMeasurementApp.Business.Interface;
using QuantityMeasurementApp.Business.Validators;

using QuantityMeasurementApp.Model.DTOs;
using QuantityMeasurementApp.Model.Entities;
using QuantityMeasurementApp.Model.Enums;

using QuantityMeasurementApp.Repository.Interface;

namespace QuantityMeasurementApp.Business.Services
{
    public class QuantityMeasurementServiceImpl : IQuantityMeasurementService
    {
        private readonly IQuantityMeasurementRepository _repository;

        public QuantityMeasurementServiceImpl(IQuantityMeasurementRepository repository)
        {
            _repository = repository;
        }

        // ─────────────────────────────────────────────
        // FACTORS (ALL CATEGORIES)
        // ─────────────────────────────────────────────

        private Dictionary<string, double> Length = new(StringComparer.OrdinalIgnoreCase)
        {
            { "meter", 1 }, { "meters", 1 },
            { "centimeter", 0.01 }, { "centimeters", 0.01 },
            { "feet", 0.3048 }, { "foot", 0.3048 },
            { "inch", 0.0254 }, { "inches", 0.0254 }
        };

        private Dictionary<string, double> Weight = new(StringComparer.OrdinalIgnoreCase)
        {
            { "kilogram", 1 }, { "kilograms", 1 },
            { "gram", 0.001 }, { "grams", 0.001 },
            { "pound", 0.453592 }, { "pounds", 0.453592 }
        };

        private Dictionary<string, double> Volume = new(StringComparer.OrdinalIgnoreCase)
        {
            { "litre", 1 }, { "litres", 1 },
            { "millilitre", 0.001 }, { "millilitres", 0.001 },
            { "gallon", 3.78541 }, { "gallons", 3.78541 }
        };

        private double ToBase(QuantityDTO q)
        {
            var unit = q.Unit.Trim();

            return q.Category switch
            {
                MeasurementCategory.Length => Convert(q.Value, unit, Length),
                MeasurementCategory.Weight => Convert(q.Value, unit, Weight),
                MeasurementCategory.Volume => Convert(q.Value, unit, Volume),
                MeasurementCategory.Temperature => ToKelvin(q.Value, unit),
                _ => throw new Exception("Invalid category")
            };
        }

        private double FromBase(double value, string targetUnit, MeasurementCategory category)
        {
            targetUnit = targetUnit.Trim();

            return category switch
            {
                MeasurementCategory.Length => value / Length[targetUnit],
                MeasurementCategory.Weight => value / Weight[targetUnit],
                MeasurementCategory.Volume => value / Volume[targetUnit],
                MeasurementCategory.Temperature => FromKelvin(value, targetUnit),
                _ => throw new Exception("Invalid category")
            };
        }

        private double Convert(double value, string unit, Dictionary<string, double> map)
        {
            if (!map.ContainsKey(unit))
                throw new Exception($"Invalid unit: {unit}");

            return value * map[unit];
        }

        // ─────────────────────────────────────────────
        // TEMPERATURE
        // ─────────────────────────────────────────────

        private double ToKelvin(double value, string unit)
        {
            return unit.ToLower() switch
            {
                "celsius" => value + 273.15,
                "fahrenheit" => (value - 32) * 5 / 9 + 273.15,
                "kelvin" => value,
                _ => throw new Exception("Invalid temperature unit")
            };
        }

        private double FromKelvin(double value, string unit)
        {
            return unit.ToLower() switch
            {
                "celsius" => value - 273.15,
                "fahrenheit" => (value - 273.15) * 9 / 5 + 32,
                "kelvin" => value,
                _ => throw new Exception("Invalid temperature unit")
            };
        }

        // ─────────────────────────────────────────────
        // CONVERT
        // ─────────────────────────────────────────────

        public QuantityResponse ConvertQuantity(ConversionRequest request)
        {
            RequestValidator.ValidateConversion(request);

            double baseVal = ToBase(request.Source);
            double result = FromBase(baseVal, request.TargetUnit, request.Source.Category);

            return new QuantityResponse
            {
                Success = true,
                Message = "Conversion successful",
                FormattedResult = $"{result:F2} {request.TargetUnit}"
            };
        }

        public QuantityResponse AddQuantities(BinaryQuantityRequest request)
        {
            RequestValidator.ValidateBinary(request);

            double base1 = ToBase(request.Quantity1);
            double base2 = ToBase(request.Quantity2);

            double resultBase = base1 + base2;

            string unit = request.TargetUnit ?? request.Quantity1.Unit;

            double result = FromBase(resultBase, unit, request.Quantity1.Category);

            return new QuantityResponse
            {
                Success = true,
                Message = "Addition successful",
                FormattedResult = $"{result:F2} {unit}"
            };
        }

        public QuantityResponse SubtractQuantities(BinaryQuantityRequest request)
        {
            RequestValidator.ValidateBinary(request);

            double base1 = ToBase(request.Quantity1);
            double base2 = ToBase(request.Quantity2);

            double resultBase = base1 - base2;

            string unit = request.TargetUnit ?? request.Quantity1.Unit;

            double result = FromBase(resultBase, unit, request.Quantity1.Category);

            return new QuantityResponse
            {
                Success = true,
                Message = "Subtraction successful",
                FormattedResult = $"{result:F2} {unit}"
            };
        }

        public QuantityResponse CompareQuantities(BinaryQuantityRequest request)
        {
            RequestValidator.ValidateBinary(request);

            double base1 = ToBase(request.Quantity1);
            double base2 = ToBase(request.Quantity2);

            return new QuantityResponse
            {
                Success = Math.Abs(base1 - base2) < 0.0001,
                Message = Math.Abs(base1 - base2) < 0.0001 ? "Equal" : "Not Equal"
            };
        }

        public DivisionResponse DivideQuantities(BinaryQuantityRequest request)
        {
            RequestValidator.ValidateBinary(request);

            double base1 = ToBase(request.Quantity1);
            double base2 = ToBase(request.Quantity2);

            if (base2 == 0)
                throw new DivideByZeroException();

            double ratio = base1 / base2;

            return new DivisionResponse
            {
                Success = true,
                Ratio = ratio,
                Interpretation = $"Ratio = {ratio:F2}"
            };
        }

        public List<QuantityMeasurementEntity> GetAllHistory() => _repository.GetAll();
        public List<QuantityMeasurementEntity> GetErroredHistory() => _repository.GetAll();
        public int GetTotalOperations() => _repository.GetTotalCount();
    }
}