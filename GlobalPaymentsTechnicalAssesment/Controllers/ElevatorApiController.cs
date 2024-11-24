using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GlobalPaymentsTechnicalAssesment.Services;
using GlobalPaymentsTechnicalAssesment.Models;

namespace GlobalPaymentsTechnicalAssesment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ElevatorApiController : ControllerBase
    {
        private readonly IQueueService _queueService;
        private readonly IElevatorService _elevatorService;

        public ElevatorApiController(IQueueService queueService, IElevatorService elevatorService)
        {
            _queueService = queueService;
            _elevatorService = elevatorService;
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

        [HttpGet("state")]
        public IActionResult GetElevatorState()
        {
            var state = _elevatorService.GetElevatorState();
            return Ok(state);
        }

        [HttpGet("queue")]
        public IActionResult GetCurrentQueue()
        {
            var queue = _queueService.GetCurrentQueue();
            return Ok(queue);
        }

        [HttpGet("history")]
        public IActionResult GetRequestHistory()
        {
            var history = _queueService.GetRequestHistory();
            return Ok(history);
        }
    }
}
