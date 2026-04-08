using Microsoft.AspNetCore.Mvc;
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

        // ─────────────────────────────────────────────
        // CORE OPERATIONS
        // ─────────────────────────────────────────────

        [HttpPost("compare")]
        public ActionResult<QuantityResponse> Compare([FromBody] BinaryQuantityRequest request)
        {
            try
            {
                return Ok(_service.CompareQuantities(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("convert")]
        public ActionResult<QuantityResponse> Convert([FromBody] ConversionRequest request)
        {
            try
            {
                return Ok(_service.ConvertQuantity(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add")]
        public ActionResult<QuantityResponse> Add([FromBody] BinaryQuantityRequest request)
        {
            try
            {
                return Ok(_service.AddQuantities(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("subtract")]
        public ActionResult<QuantityResponse> Subtract([FromBody] BinaryQuantityRequest request)
        {
            try
            {
                return Ok(_service.SubtractQuantities(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("divide")]
        public ActionResult<DivisionResponse> Divide([FromBody] BinaryQuantityRequest request)
        {
            try
            {
                return Ok(_service.DivideQuantities(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ─────────────────────────────────────────────
        // HISTORY APIs (RESTORED)
        // ─────────────────────────────────────────────

        [HttpGet("history")]
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