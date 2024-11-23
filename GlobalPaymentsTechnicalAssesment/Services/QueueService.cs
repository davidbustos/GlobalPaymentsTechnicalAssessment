using System.Collections.Concurrent;
using System.Threading.Tasks;
using GlobalPaymentsTechnicalAssesment.Models;
using Newtonsoft.Json;

namespace GlobalPaymentsTechnicalAssesment.Services
{
    public interface IQueueService
    {
        Task EnqueueRequestAsync(FloorRequest request);
        Task<FloorRequest> DequeueRequestAsync();
    }

    public class QueueService : IQueueService
    {
        // Thread-safe in-memory queue
        private static readonly ConcurrentQueue<FloorRequest> _queue = new();

        public Task EnqueueRequestAsync(FloorRequest request)
        {
            // Add the request to the queue
            _queue.Enqueue(request);

            // Log (optional)
            Console.WriteLine($"[QueueService] Enqueued request: {JsonConvert.SerializeObject(request)}");

            return Task.CompletedTask;
        }

        public Task<FloorRequest> DequeueRequestAsync()
        {
            // Try to dequeue a request
            if (_queue.TryDequeue(out var request))
            {
                // Log (optional)
                Console.WriteLine($"[QueueService] Dequeued request: {JsonConvert.SerializeObject(request)}");
                return Task.FromResult(request);
            }

            // Return null if the queue is empty
            return Task.FromResult<FloorRequest>(null);
        }
    }
}
