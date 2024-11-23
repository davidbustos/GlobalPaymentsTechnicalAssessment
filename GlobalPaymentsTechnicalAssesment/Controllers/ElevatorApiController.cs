using GlobalPaymentsTechnicalAssesment.Models;
using GlobalPaymentsTechnicalAssesment.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalPaymentsTechnicalAssesment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElevatorApiController : ControllerBase
    {
        private readonly IQueueService _queueService;

        public ElevatorApiController(IQueueService queueService)
        {
            _queueService = queueService;
        }

        [HttpPost("request-floor")]
        public async Task<IActionResult> RequestFloor([FromBody] FloorRequest request)
        {
            if (request.Floor < 1 || request.Floor > 5)
            {
                return BadRequest("Floor must be between 1 and 5.");
            }

            await _queueService.EnqueueRequestAsync(request);
            return Ok("Request queued successfully.");
        }
    }
}
