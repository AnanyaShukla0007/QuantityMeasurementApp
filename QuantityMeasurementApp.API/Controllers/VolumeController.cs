using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementApp.Business.Interfaces;
using QuantityMeasurementApp.Model.DTOs;

namespace QuantityMeasurementApp.API.Controllers
{
    [ApiController]
    [Route("api/volume")]
    public class VolumeController : ControllerBase
    {
        private readonly IVolumeBusiness _business;

        public VolumeController(IVolumeBusiness business)
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

        [HttpPost("subtract")]
        public IActionResult Subtract([FromBody] ArithmeticRequestDto dto)
        {
            return Ok(_business.Subtract(dto));
        }

        [HttpPost("divide")]
        public IActionResult Divide([FromBody] ArithmeticRequestDto dto)
        {
            return Ok(_business.Divide(dto));
        }
    }
}