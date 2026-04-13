using QMAService.Business.Interface;
using QMAService.Business.Validators;
using QMAService.Model.DTOs;
using QMAService.Model.Entities;
using QMAService.Repository.Interface;

namespace QMAService.Business.Services;

public class QuantityMeasurementServiceImpl : IQuantityMeasurementService
{
    private readonly IQuantityMeasurementRepository _repository;

    public QuantityMeasurementServiceImpl(IQuantityMeasurementRepository repository)
    {
        _repository = repository;
    }

    public QuantityResponse ConvertQuantity(ConversionRequest request)
    {
        if (!RequestValidator.IsValid(request))
            return new QuantityResponse { Result = 0, Message = "Invalid request" };

        double result = request.Value;

        if (request.FromUnit.ToLower() == "feet" && request.ToUnit.ToLower() == "inch")
            result = request.Value * 12;
        else if (request.FromUnit.ToLower() == "inch" && request.ToUnit.ToLower() == "feet")
            result = request.Value / 12;

        _repository.Save(new QuantityMeasurementEntity { Operation = "Convert", Result = result });
        return new QuantityResponse { Result = result, Message = "Success" };
    }

    public QuantityResponse AddQuantities(BinaryQuantityRequest request)
    {
        if (!RequestValidator.IsValid(request))
            return new QuantityResponse { Result = 0, Message = "Invalid request" };

        var result = request.Value1 + request.Value2;
        _repository.Save(new QuantityMeasurementEntity { Operation = "Add", Result = result });
        return new QuantityResponse { Result = result, Message = "Success" };
    }

    public QuantityResponse SubtractQuantities(BinaryQuantityRequest request)
    {
        if (!RequestValidator.IsValid(request))
            return new QuantityResponse { Result = 0, Message = "Invalid request" };

        var result = request.Value1 - request.Value2;
        _repository.Save(new QuantityMeasurementEntity { Operation = "Subtract", Result = result });
        return new QuantityResponse { Result = result, Message = "Success" };
    }

    public List<QuantityMeasurementEntity> GetAllHistory()
    {
        return _repository.GetAll();
    }
}