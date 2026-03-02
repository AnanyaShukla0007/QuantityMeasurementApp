using System;
using QuantityMeasurementApp.Model.DTOs;
using QuantityMeasurementApp.Service.Interfaces;
using QuantityMeasurementApp.Business.Validators;
using QuantityMeasurementApp.Business.Interfaces;
namespace QuantityMeasurementApp.Business.Implementations
{
    public class TemperatureBusiness : ITemperatureBusiness
    {
        private readonly ITemperatureService _service;

        public TemperatureBusiness(ITemperatureService service)
        {
            _service = service;
        }

        public ApiResponseDto<double> Convert(ConvertRequestDto dto)
        {
            try
            {
                RequestValidator.Validate(dto);
                var result = _service.Convert(dto.Value, dto.FromUnit, dto.ToUnit);

                return new ApiResponseDto<double>
                {
                    Success = true,
                    Data = result,
                    Message = "Conversion successful"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<double>
                {
                    Success = false,
                    Data = default,
                    Message = ex.Message
                };
            }
        }

        public ApiResponseDto<bool> Equal(EqualityRequestDto dto)
        {
            try
            {
                RequestValidator.Validate(dto);
                var result = _service.Equal(dto.Value1, dto.Unit1, dto.Value2, dto.Unit2);

                return new ApiResponseDto<bool>
                {
                    Success = true,
                    Data = result,
                    Message = "Equality check successful"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Data = false,
                    Message = ex.Message
                };
            }
        }
    }
}