using QuantityMeasurementApp.Model.DTOs;

namespace QuantityMeasurementApp.Business.Interfaces
{
    public interface IWeightBusiness
    {
        ApiResponseDto<double> Convert(ConvertRequestDto dto);
        ApiResponseDto<bool> Equal(EqualityRequestDto dto);
        ApiResponseDto<double> Add(ArithmeticRequestDto dto);
        ApiResponseDto<double> Subtract(ArithmeticRequestDto dto);
        ApiResponseDto<double> Divide(ArithmeticRequestDto dto);
    }
}