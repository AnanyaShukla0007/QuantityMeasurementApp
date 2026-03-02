using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementApp.Business.Interfaces;
using QuantityMeasurementApp.Model.DTOs;

namespace QuantityMeasurementApp.API.Controllers
{
    [ApiController]
    [Route("api/length")]
    public class LengthController : ControllerBase
    {
        private readonly ILengthBusiness _business;

        public LengthController(ILengthBusiness business)
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

        [HttpPost("add")]
        public IActionResult Add([FromBody] ArithmeticRequestDto dto)
        {
            return Ok(_business.Add(dto));
        }
    }
}