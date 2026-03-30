using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

using QuantityMeasurementApp.Business.Interface;
using QuantityMeasurementApp.Model.DTOs;
using QuantityMeasurementApp.Model.Entities;
using QuantityMeasurementApp.API.Services;

namespace QuantityMeasurementApp.API.Controllers
{
    [ApiController]
    [Route("api/v1/quantities")]
    [Tags("Quantity Measurements")]
    [Authorize(Roles = "Admin")]
    public class QuantityMeasurementApiController : ControllerBase
    {
        private readonly IQuantityMeasurementService _service;
        private readonly RedisCacheService _cache;

        public QuantityMeasurementApiController(
            IQuantityMeasurementService service,
            RedisCacheService cache)
        {
            _service = service;
            _cache = cache;
        }

        [HttpPost("compare")]
        [SwaggerOperation(
            Summary = "Compare two quantities",
            Description = "Checks whether two quantities are equal."
        )]
        [ProducesResponseType(typeof(QuantityResponse), 200)]
        public ActionResult<QuantityResponse> Compare([FromBody] BinaryQuantityRequest request)
        {
            return Ok(_service.CompareQuantities(request));
        }

        [HttpPost("convert")]
        [SwaggerOperation(
            Summary = "Convert quantity",
            Description = "Converts a quantity from one unit to another."
        )]
        [ProducesResponseType(typeof(QuantityResponse), 200)]
        public ActionResult<QuantityResponse> Convert([FromBody] ConversionRequest request)
        {
            return Ok(_service.ConvertQuantity(request));
        }

        [HttpPost("add")]
        [SwaggerOperation(
            Summary = "Add quantities",
            Description = "Adds two quantities and returns the result."
        )]
        [ProducesResponseType(typeof(QuantityResponse), 200)]
        public ActionResult<QuantityResponse> Add([FromBody] BinaryQuantityRequest request)
        {
            return Ok(_service.AddQuantities(request));
        }

        [HttpPost("subtract")]
        [SwaggerOperation(
            Summary = "Subtract quantities",
            Description = "Subtracts the second quantity from the first."
        )]
        [ProducesResponseType(typeof(QuantityResponse), 200)]
        public ActionResult<QuantityResponse> Subtract([FromBody] BinaryQuantityRequest request)
        {
            return Ok(_service.SubtractQuantities(request));
        }

        [HttpPost("divide")]
        [SwaggerOperation(
            Summary = "Divide quantities",
            Description = "Divides the first quantity by the second and returns the ratio."
        )]
        [ProducesResponseType(typeof(DivisionResponse), 200)]
        public ActionResult<DivisionResponse> Divide([FromBody] BinaryQuantityRequest request)
        {
            return Ok(_service.DivideQuantities(request));
        }

        [HttpGet("history")]
        [SwaggerOperation(
            Summary = "Get operation history",
            Description = "Returns all stored quantity measurement operations."
        )]
        [ProducesResponseType(typeof(List<QuantityMeasurementEntity>), 200)]
        public async Task<ActionResult<List<QuantityMeasurementEntity>>> GetHistory()
        {
            const string cacheKey = "quantity_history";

            var cached = await _cache.GetAsync<List<QuantityMeasurementEntity>>(cacheKey);

            if (cached != null)
                return Ok(cached);

            var data = _service.GetAllHistory();

            await _cache.SetAsync(cacheKey, data);

            return Ok(data);
        }

        [HttpGet("history/errored")]
        [SwaggerOperation(
            Summary = "Get errored operations",
            Description = "Returns operations that resulted in errors."
        )]
        [ProducesResponseType(typeof(List<QuantityMeasurementEntity>), 200)]
        public async Task<ActionResult<List<QuantityMeasurementEntity>>> GetErroredHistory()
        {
            const string cacheKey = "errored_history";

            var cached = await _cache.GetAsync<List<QuantityMeasurementEntity>>(cacheKey);

            if (cached != null)
                return Ok(cached);

            var data = _service.GetErroredHistory();

            await _cache.SetAsync(cacheKey, data);

            return Ok(data);
        }

        [HttpGet("count")]
        [SwaggerOperation(
            Summary = "Get total operation count",
            Description = "Returns the total number of stored measurement operations."
        )]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<ActionResult<int>> GetTotalOperations()
        {
            const string cacheKey = "operation_count";

            var cached = await _cache.GetAsync<int?>(cacheKey);

            if (cached != null)
                return Ok(cached);

            var count = _service.GetTotalOperations();

            await _cache.SetAsync(cacheKey, count);

            return Ok(count);
        }
    }
}