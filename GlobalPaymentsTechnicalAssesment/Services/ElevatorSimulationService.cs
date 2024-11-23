using System.Threading;
using System.Threading.Tasks;
using GlobalPaymentsTechnicalAssesment.Models;
using Microsoft.Extensions.Hosting;

namespace GlobalPaymentsTechnicalAssesment.Services
{
    public class ElevatorSimulationService : BackgroundService
    {
        private readonly IQueueService _queueService;
        private readonly IElevatorService _elevatorService;

        public ElevatorSimulationService(IQueueService queueService, IElevatorService elevatorService)
        {
            _queueService = queueService;
            _elevatorService = elevatorService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var request = await _queueService.DequeueRequestAsync();
                if (request != null)
                {
                    await _elevatorService.ProcessFloorRequestAsync(request);
                }
                else
                {
                    await Task.Delay(500);
                }
            }
        }
    }
}
