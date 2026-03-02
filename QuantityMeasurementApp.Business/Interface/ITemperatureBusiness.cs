using QuantityMeasurementApp.Model.DTOs;

namespace QuantityMeasurementApp.Business.Interfaces
{
    public interface ITemperatureBusiness
    {
        ApiResponseDto<double> Convert(ConvertRequestDto dto);
        ApiResponseDto<bool> Equal(EqualityRequestDto dto);
    }
}