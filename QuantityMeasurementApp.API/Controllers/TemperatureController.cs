using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementApp.Business.Interfaces;
using QuantityMeasurementApp.Model.DTOs;

namespace QuantityMeasurementApp.API.Controllers
{
    [ApiController]
    [Route("api/temperature")]
    public class TemperatureController : ControllerBase
    {
        private readonly ITemperatureBusiness _business;

        public TemperatureController(ITemperatureBusiness business)
        {
            _business = business;
        }

        [HttpPost("convert")]
        public IActionResult Convert([FromBody] ConvertRequestDto dto)
        {
            return Ok(_business.Convert(dto));
        }

        [HttpPost("equal")]
        public IActionResult Equal([FromBody] EqualityRequestDto dto)
        {
            return Ok(_business.Equal(dto));
        }
    }
}