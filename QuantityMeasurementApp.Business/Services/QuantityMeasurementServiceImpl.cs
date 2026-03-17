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

        public QuantityResponse CompareQuantities(BinaryQuantityRequest request)
        {
            try
            {
                RequestValidator.ValidateBinary(request);

                bool result = request.Quantity1.Value == request.Quantity2.Value;

                var response = new QuantityResponse
                {
                    Success = true,
                    Message = result ? "Quantities are equal" : "Quantities are not equal",
                    FormattedResult =
                        $"{request.Quantity1.Value} {request.Quantity1.Unit} vs {request.Quantity2.Value} {request.Quantity2.Unit}"
                };

                var entity = new QuantityMeasurementEntity
                {
                    OperationType = OperationType.COMPARE,
                    MeasurementCategory = MeasurementCategory.Length,

                    Operand1Value = request.Quantity1.Value,
                    Operand1Unit = request.Quantity1.Unit,

                    Operand2Value = request.Quantity2.Value,
                    Operand2Unit = request.Quantity2.Unit,

                    ResultValue = result ? 1 : 0,
                    ResultUnit = ""
                };

                _repository.Save(entity);

                return response;
            }
            catch (Exception ex)
            {
                throw new QuantityMeasurementException(ex.Message);
            }
        }

        public QuantityResponse ConvertQuantity(ConversionRequest request)
        {
            try
            {
                RequestValidator.ValidateConversion(request);

                double result = request.Source.Value;

                var response = new QuantityResponse
                {
                    Success = true,
                    Message = "Conversion successful",
                    FormattedResult =
                        $"{request.Source.Value} {request.Source.Unit} → {result} {request.TargetUnit}"
                };

                var entity = new QuantityMeasurementEntity
                {
                    OperationType = OperationType.CONVERT,
                    MeasurementCategory = MeasurementCategory.Length,

                    Operand1Value = request.Source.Value,
                    Operand1Unit = request.Source.Unit,

                    Operand2Value = 0,
                    Operand2Unit = "",

                    ResultValue = result,
                    ResultUnit = request.TargetUnit
                };

                _repository.Save(entity);

                return response;
            }
            catch (Exception ex)
            {
                throw new QuantityMeasurementException(ex.Message);
            }
        }

        public QuantityResponse AddQuantities(BinaryQuantityRequest request)
        {
            try
            {
                RequestValidator.ValidateBinary(request);

                double result = request.Quantity1.Value + request.Quantity2.Value;

                var response = new QuantityResponse
                {
                    Success = true,
                    Message = "Addition successful",
                    FormattedResult =
                        $"{request.Quantity1.Value} + {request.Quantity2.Value} = {result}"
                };

                var entity = new QuantityMeasurementEntity
                {
                    OperationType = OperationType.ADD,
                    MeasurementCategory = MeasurementCategory.Length,

                    Operand1Value = request.Quantity1.Value,
                    Operand1Unit = request.Quantity1.Unit,

                    Operand2Value = request.Quantity2.Value,
                    Operand2Unit = request.Quantity2.Unit,

                    ResultValue = result,
                    ResultUnit = request.Quantity1.Unit
                };

                _repository.Save(entity);

                return response;
            }
            catch (Exception ex)
            {
                throw new QuantityMeasurementException(ex.Message);
            }
        }

        public QuantityResponse SubtractQuantities(BinaryQuantityRequest request)
        {
            try
            {
                RequestValidator.ValidateBinary(request);

                double result = request.Quantity1.Value - request.Quantity2.Value;

                var response = new QuantityResponse
                {
                    Success = true,
                    Message = "Subtraction successful",
                    FormattedResult =
                        $"{request.Quantity1.Value} - {request.Quantity2.Value} = {result}"
                };

                var entity = new QuantityMeasurementEntity
                {
                    OperationType = OperationType.SUBTRACT,
                    MeasurementCategory = MeasurementCategory.Length,

                    Operand1Value = request.Quantity1.Value,
                    Operand1Unit = request.Quantity1.Unit,

                    Operand2Value = request.Quantity2.Value,
                    Operand2Unit = request.Quantity2.Unit,

                    ResultValue = result,
                    ResultUnit = request.Quantity1.Unit
                };

                _repository.Save(entity);

                return response;
            }
            catch (Exception ex)
            {
                throw new QuantityMeasurementException(ex.Message);
            }
        }

        public DivisionResponse DivideQuantities(BinaryQuantityRequest request)
        {
            try
            {
                RequestValidator.ValidateBinary(request);

                if (request.Quantity2.Value == 0)
                    throw new DivideByZeroException("Division by zero is not allowed.");

                double ratio = request.Quantity1.Value / request.Quantity2.Value;

                var response = new DivisionResponse
                {
                    Success = true,
                    Message = "Division successful",
                    Ratio = ratio,
                    Interpretation =
                        $"The first quantity is {ratio:F2} times the second."
                };

                var entity = new QuantityMeasurementEntity
                {
                    OperationType = OperationType.DIVIDE,
                    MeasurementCategory = MeasurementCategory.Length,

                    Operand1Value = request.Quantity1.Value,
                    Operand1Unit = request.Quantity1.Unit,

                    Operand2Value = request.Quantity2.Value,
                    Operand2Unit = request.Quantity2.Unit,

                    ResultValue = ratio,
                    ResultUnit = "ratio"
                };

                _repository.Save(entity);

                return response;
            }
            catch (Exception ex)
            {
                throw new QuantityMeasurementException(ex.Message);
            }
        }

        public List<QuantityMeasurementEntity> GetAllHistory()
        {
            return _repository.GetAll();
        }

        public List<QuantityMeasurementEntity> GetErroredHistory()
        {
            return _repository
                .GetAll()
                .Where(x => !string.IsNullOrEmpty(x.ErrorMessage))
                .ToList();
        }

        public int GetTotalOperations()
        {
            return _repository.GetTotalCount();
        }
    }
}