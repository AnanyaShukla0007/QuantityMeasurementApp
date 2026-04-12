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

        public QuantityResponse ConvertQuantity(ConversionRequest request)
        {
            RequestValidator.ValidateConversion(request);

            double baseVal = ToBase(request.Source);
            double result = FromBase(baseVal, request.TargetUnit, request.Source.Category);

            _repository.Save(new QuantityMeasurementEntity
            {
                Username = "Guest",
                OperationType = OperationType.CONVERT,
                MeasurementCategory = request.Source.Category,
                Operand1Value = request.Source.Value,
                Operand1Unit = request.Source.Unit,
                Operand2Value = 0,
                Operand2Unit = "",
                ResultValue = result,
                ResultUnit = request.TargetUnit,
                ErrorMessage = null,
                Timestamp = DateTime.UtcNow
            });

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

            _repository.Save(new QuantityMeasurementEntity
            {
                Username = "Guest",
                OperationType = OperationType.ADD,
                MeasurementCategory = request.Quantity1.Category,
                Operand1Value = request.Quantity1.Value,
                Operand1Unit = request.Quantity1.Unit,
                Operand2Value = request.Quantity2.Value,
                Operand2Unit = request.Quantity2.Unit,
                ResultValue = result,
                ResultUnit = unit,
                ErrorMessage = null,
                Timestamp = DateTime.UtcNow
            });

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

            _repository.Save(new QuantityMeasurementEntity
            {
                Username = "Guest",
                OperationType = OperationType.SUBTRACT,
                MeasurementCategory = request.Quantity1.Category,
                Operand1Value = request.Quantity1.Value,
                Operand1Unit = request.Quantity1.Unit,
                Operand2Value = request.Quantity2.Value,
                Operand2Unit = request.Quantity2.Unit,
                ResultValue = result,
                ResultUnit = unit,
                ErrorMessage = null,
                Timestamp = DateTime.UtcNow
            });

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

            bool equal = Math.Abs(base1 - base2) < 0.0001;

            _repository.Save(new QuantityMeasurementEntity
            {
                Username = "Guest",
                OperationType = OperationType.COMPARE,
                MeasurementCategory = request.Quantity1.Category,
                Operand1Value = request.Quantity1.Value,
                Operand1Unit = request.Quantity1.Unit,
                Operand2Value = request.Quantity2.Value,
                Operand2Unit = request.Quantity2.Unit,
                ResultValue = equal ? 1 : 0,
                ResultUnit = equal ? "Equal" : "Not Equal",
                ErrorMessage = null,
                Timestamp = DateTime.UtcNow
            });

            return new QuantityResponse
            {
                Success = true,
                Message = equal ? "Equal" : "Not Equal",
                FormattedResult = equal ? "Equal" : "Not Equal"
            };
        }

        public DivisionResponse DivideQuantities(BinaryQuantityRequest request)
        {
            RequestValidator.ValidateBinary(request);

            double base1 = ToBase(request.Quantity1);
            double base2 = ToBase(request.Quantity2);

            if (base2 == 0)
                throw new DivideByZeroException("Cannot divide by zero.");

            double ratio = base1 / base2;

            _repository.Save(new QuantityMeasurementEntity
            {
                Username = "Guest",
                OperationType = OperationType.DIVIDE,
                MeasurementCategory = request.Quantity1.Category,
                Operand1Value = request.Quantity1.Value,
                Operand1Unit = request.Quantity1.Unit,
                Operand2Value = request.Quantity2.Value,
                Operand2Unit = request.Quantity2.Unit,
                ResultValue = ratio,
                ResultUnit = "Ratio",
                ErrorMessage = null,
                Timestamp = DateTime.UtcNow
            });

            return new DivisionResponse
            {
                Success = true,
                Ratio = ratio,
                Interpretation = $"Ratio = {ratio:F2}"
            };
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