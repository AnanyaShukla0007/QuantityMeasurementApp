using Microsoft.AspNetCore.Mvc;
using QMAService.Business.Interface;
using QMAService.Model.DTOs;

namespace QMAService.API.Controllers;

[ApiController]
[Route("api/v1/quantities")]
public class QuantityMeasurementApiController : ControllerBase
{
    private readonly IQuantityMeasurementService _service;
    public QuantityMeasurementApiController(IQuantityMeasurementService service) => _service = service;

    [HttpPost("convert")]
    public IActionResult Convert(ConversionRequest request) => Ok(_service.ConvertQuantity(request));

    [HttpPost("add")]
    public IActionResult Add(BinaryQuantityRequest request) => Ok(_service.AddQuantities(request));

    [HttpPost("subtract")]
    public IActionResult Subtract(BinaryQuantityRequest request) => Ok(_service.SubtractQuantities(request));

    [HttpGet("history")]
    public IActionResult History() => Ok(_service.GetAllHistory());
}