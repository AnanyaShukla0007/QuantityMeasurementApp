using System;
using System.Collections.Generic;
using System.Linq;
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

        public QuantityResponse ConvertQuantity(ConversionRequest request, string username)
        {
            RequestValidator.ValidateConversion(request);

            double baseVal = ToBase(request.Source);
            double result = FromBase(baseVal, request.TargetUnit, request.Source.Category);

            SaveHistory(username, OperationType.CONVERT, request.Source.Category,
                request.Source.Value, request.Source.Unit,
                0, "", result, request.TargetUnit);

            return new QuantityResponse
            {
                Success = true,
                Message = "Conversion successful",
                FormattedResult = $"{result:F2} {request.TargetUnit}"
            };
        }

        public QuantityResponse AddQuantities(BinaryQuantityRequest request, string username)
        {
            RequestValidator.ValidateBinary(request);

            double base1 = ToBase(request.Quantity1);
            double base2 = ToBase(request.Quantity2);

            double resultBase = base1 + base2;
            string unit = request.TargetUnit ?? request.Quantity1.Unit;
            double result = FromBase(resultBase, unit, request.Quantity1.Category);

            SaveHistory(username, OperationType.ADD, request.Quantity1.Category,
                request.Quantity1.Value, request.Quantity1.Unit,
                request.Quantity2.Value, request.Quantity2.Unit,
                result, unit);

            return new QuantityResponse
            {
                Success = true,
                Message = "Addition successful",
                FormattedResult = $"{result:F2} {unit}"
            };
        }

        public QuantityResponse SubtractQuantities(BinaryQuantityRequest request, string username)
        {
            RequestValidator.ValidateBinary(request);

            double base1 = ToBase(request.Quantity1);
            double base2 = ToBase(request.Quantity2);

            double resultBase = base1 - base2;
            string unit = request.TargetUnit ?? request.Quantity1.Unit;
            double result = FromBase(resultBase, unit, request.Quantity1.Category);

            SaveHistory(username, OperationType.SUBTRACT, request.Quantity1.Category,
                request.Quantity1.Value, request.Quantity1.Unit,
                request.Quantity2.Value, request.Quantity2.Unit,
                result, unit);

            return new QuantityResponse
            {
                Success = true,
                Message = "Subtraction successful",
                FormattedResult = $"{result:F2} {unit}"
            };
        }

        public QuantityResponse CompareQuantities(BinaryQuantityRequest request, string username)
        {
            RequestValidator.ValidateBinary(request);

            double base1 = ToBase(request.Quantity1);
            double base2 = ToBase(request.Quantity2);

            bool equal = Math.Abs(base1 - base2) < 0.0001;

            SaveHistory(username, OperationType.COMPARE, request.Quantity1.Category,
                request.Quantity1.Value, request.Quantity1.Unit,
                request.Quantity2.Value, request.Quantity2.Unit,
                equal ? 1 : 0,
                equal ? "Equal" : "Not Equal");

            return new QuantityResponse
            {
                Success = true,
                Message = equal ? "Equal" : "Not Equal",
                FormattedResult = equal ? "Equal" : "Not Equal"
            };
        }

        public DivisionResponse DivideQuantities(BinaryQuantityRequest request, string username)
        {
            RequestValidator.ValidateBinary(request);

            double base1 = ToBase(request.Quantity1);
            double base2 = ToBase(request.Quantity2);

            if (base2 == 0)
                throw new DivideByZeroException("Cannot divide by zero.");

            double ratio = base1 / base2;

            SaveHistory(username, OperationType.DIVIDE, request.Quantity1.Category,
                request.Quantity1.Value, request.Quantity1.Unit,
                request.Quantity2.Value, request.Quantity2.Unit,
                ratio, "Ratio");

            return new DivisionResponse
            {
                Success = true,
                Ratio = ratio,
                Interpretation = $"Ratio = {ratio:F2}"
            };
        }

        private void SaveHistory(
            string username,
            OperationType operation,
            MeasurementCategory category,
            double operand1Value,
            string operand1Unit,
            double operand2Value,
            string operand2Unit,
            double resultValue,
            string resultUnit)
        {
            _repository.Save(new QuantityMeasurementEntity
            {
                Username = username,
                OperationType = operation,
                MeasurementCategory = category,
                Operand1Value = operand1Value,
                Operand1Unit = operand1Unit,
                Operand2Value = operand2Value,
                Operand2Unit = operand2Unit,
                ResultValue = resultValue,
                ResultUnit = resultUnit,
                ErrorMessage = null,
                Timestamp = DateTime.UtcNow
            });
        }

        public List<QuantityMeasurementEntity> GetAllHistory()
        {
            return _repository.GetAll();
        }

        public List<QuantityMeasurementEntity> GetErroredHistory()
        {
            return _repository.GetAll()
                .Where(x => !string.IsNullOrEmpty(x.ErrorMessage))
                .ToList();
        }

        public int GetTotalOperations()
        {
            return _repository.GetTotalCount();
        }

        private double ToBase(QuantityDTO quantity)
        {
            return quantity.Category switch
            {
                MeasurementCategory.Length => quantity.Value * GetLengthFactor(quantity.Unit),
                MeasurementCategory.Weight => quantity.Value * GetWeightFactor(quantity.Unit),
                MeasurementCategory.Volume => quantity.Value * GetVolumeFactor(quantity.Unit),
                MeasurementCategory.Temperature => ToCelsius(quantity.Value, quantity.Unit),
                _ => throw new Exception("Unsupported category")
            };
        }

        private double FromBase(double value, string unit, MeasurementCategory category)
        {
            return category switch
            {
                MeasurementCategory.Length => value / GetLengthFactor(unit),
                MeasurementCategory.Weight => value / GetWeightFactor(unit),
                MeasurementCategory.Volume => value / GetVolumeFactor(unit),
                MeasurementCategory.Temperature => FromCelsius(value, unit),
                _ => throw new Exception("Unsupported category")
            };
        }

        private double GetLengthFactor(string unit) => unit.ToLower() switch
        {
            "inch" => 1,
            "inches" => 1,
            "feet" => 12,
            "foot" => 12,
            "yard" => 36,
            "yards" => 36,
            _ => throw new Exception("Invalid length unit")
        };

        private double GetWeightFactor(string unit) => unit.ToLower() switch
        {
            "gram" => 1,
            "grams" => 1,
            "kilogram" => 1000,
            "kilograms" => 1000,
            "tonne" => 1000000,
            _ => throw new Exception("Invalid weight unit")
        };

        private double GetVolumeFactor(string unit) => unit.ToLower() switch
        {
            "milliliter" => 1,
            "milliliters" => 1,
            "liter" => 1000,
            "liters" => 1000,
            "gallon" => 3785.41,
            _ => throw new Exception("Invalid volume unit")
        };

        private double ToCelsius(double value, string unit) => unit.ToLower() switch
        {
            "celsius" => value,
            "fahrenheit" => (value - 32) * 5 / 9,
            "kelvin" => value - 273.15,
            _ => throw new Exception("Invalid temperature unit")
        };

        private double FromCelsius(double value, string unit) => unit.ToLower() switch
        {
            "celsius" => value,
            "fahrenheit" => (value * 9 / 5) + 32,
            "kelvin" => value + 273.15,
            _ => throw new Exception("Invalid temperature unit")
        };
    }
}