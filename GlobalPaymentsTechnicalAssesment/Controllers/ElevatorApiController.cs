using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GlobalPaymentsTechnicalAssesment.Services;
using GlobalPaymentsTechnicalAssesment.Models;
using GlobalPaymentsTechnicalAssesment.Services;

namespace GlobalPaymentsTechnicalAssesment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            return Ok("Request added to queue.");
        }

        [HttpGet("next-request")]
        public async Task<IActionResult> GetNextRequest()
        {
            var request = await _queueService.DequeueRequestAsync();

            if (request == null)
            {
                return NotFound("No pending requests in the queue.");
            }

            return Ok(request);
        }
    }
}
