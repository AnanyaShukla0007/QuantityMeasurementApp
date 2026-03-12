using System;
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
                    FormattedResult = $"{request.Quantity1.Value} {request.Quantity1.Unit} vs {request.Quantity2.Value} {request.Quantity2.Unit}"
                };

                _repository.Save(new QuantityMeasurementEntity(OperationType.COMPARE, response.FormattedResult!));

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

                double result = request.Source.Value; // placeholder

                var response = new QuantityResponse
                {
                    Success = true,
                    Message = "Conversion successful",
                    FormattedResult = $"{request.Source.Value} {request.Source.Unit} → {result} {request.TargetUnit}"
                };

                _repository.Save(new QuantityMeasurementEntity(OperationType.CONVERT, response.FormattedResult!));

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
                    FormattedResult = $"{request.Quantity1.Value} + {request.Quantity2.Value} = {result}"
                };

                _repository.Save(new QuantityMeasurementEntity(OperationType.ADD, response.FormattedResult!));

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
                    FormattedResult = $"{request.Quantity1.Value} - {request.Quantity2.Value} = {result}"
                };

                _repository.Save(new QuantityMeasurementEntity(OperationType.SUBTRACT, response.FormattedResult!));

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
                    Interpretation = $"The first quantity is {ratio:F2} times the second."
                };

                _repository.Save(new QuantityMeasurementEntity(OperationType.DIVIDE, response.Interpretation));

                return response;
            }
            catch (Exception ex)
            {
                throw new QuantityMeasurementException(ex.Message);
            }
        }
    }
}