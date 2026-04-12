// QuantityMeasurementApiController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Swashbuckle.AspNetCore.Annotations;

using QuantityMeasurementApp.Business.Interface;
using QuantityMeasurementApp.Model.DTOs;
using QuantityMeasurementApp.Model.Entities;
using QuantityMeasurementApp.API.Services;

namespace QuantityMeasurementApp.API.Controllers
{
    [ApiController]
    [Route("api/v1/quantities")]
    [Authorize]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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

        [HttpGet("history")]
        public async Task<ActionResult<List<QuantityMeasurementEntity>>> GetHistory()
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value
                           ?? User.Identity?.Name;

            if (string.IsNullOrWhiteSpace(username))
                return Unauthorized();

            var cacheKey = $"history_{username}";

            var cached = await _cache.GetAsync<List<QuantityMeasurementEntity>>(cacheKey);

            if (cached != null)
                return Ok(cached);

            var data = _service.GetAllHistory()
                .Where(x => x.Username == username)
                .ToList();

            await _cache.SetAsync(cacheKey, data);

            return Ok(data);
        }

        [HttpGet("history/errored")]
        public async Task<ActionResult<List<QuantityMeasurementEntity>>> GetErroredHistory()
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value
                           ?? User.Identity?.Name;

            if (string.IsNullOrWhiteSpace(username))
                return Unauthorized();

            var cacheKey = $"errored_{username}";

            var cached = await _cache.GetAsync<List<QuantityMeasurementEntity>>(cacheKey);

            if (cached != null)
                return Ok(cached);

            var data = _service.GetErroredHistory()
                .Where(x => x.Username == username)
                .ToList();

            await _cache.SetAsync(cacheKey, data);

            return Ok(data);
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> GetTotalOperations()
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value
                           ?? User.Identity?.Name;

            if (string.IsNullOrWhiteSpace(username))
                return Unauthorized();

            var cacheKey = $"count_{username}";

            var cached = await _cache.GetAsync<int?>(cacheKey);

            if (cached != null)
                return Ok(cached.Value);

            var count = _service.GetAllHistory()
                .Count(x => x.Username == username);

            await _cache.SetAsync(cacheKey, count);

            return Ok(count);
        }
    }
}