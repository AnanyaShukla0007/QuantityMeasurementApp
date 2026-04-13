using QMAService.Model.DTOs;

namespace QMAService.Business.Validators;

public static class RequestValidator
{
    public static bool IsValid(ConversionRequest request)
        => request.Value >= 0 && !string.IsNullOrWhiteSpace(request.FromUnit) && !string.IsNullOrWhiteSpace(request.ToUnit);

    public static bool IsValid(BinaryQuantityRequest request)
        => request.Value1 >= 0 && request.Value2 >= 0;
}