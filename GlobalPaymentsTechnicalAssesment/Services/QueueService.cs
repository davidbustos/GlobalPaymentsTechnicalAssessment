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
        List<FloorRequest> GetCurrentQueue(); // Returns current queue
        List<ProcessedRequest> GetRequestHistory(); // Returns history log
        void AddToHistory(FloorRequest request); // Adds a processed request to history
    }

    public class QueueService : IQueueService
    {
        private static readonly ConcurrentQueue<FloorRequest> _queue = new();
        private static readonly List<ProcessedRequest> _history = new(); // History log

        public Task EnqueueRequestAsync(FloorRequest request)
        {
            _queue.Enqueue(request);
            return Task.CompletedTask;
        }

        public Task<FloorRequest> DequeueRequestAsync()
        {
            if (_queue.TryDequeue(out var request))
            {
                return Task.FromResult(request);
            }
            return Task.FromResult<FloorRequest>(null);
        }

        public List<FloorRequest> GetCurrentQueue()
        {
            return new List<FloorRequest>(_queue); // Returns a copy of the queue
        }

        public List<ProcessedRequest> GetRequestHistory()
        {
            return new List<ProcessedRequest>(_history); // Returns a copy of the history
        }

        public void AddToHistory(FloorRequest request)
        {
            _history.Add(new ProcessedRequest
            {
                Source = request.Source,
                Floor = request.Floor,
                ProcessedAt = DateTime.Now,
                Direction = request.Direction
            });
        }
    }
}
