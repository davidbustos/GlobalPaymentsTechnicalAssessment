using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using GlobalPaymentsTechnicalAssesment.Models;

namespace GlobalPaymentsTechnicalAssesment.Services
{
    public interface IQueueService
    {
        Task EnqueueRequestAsync(FloorRequest request);
    }

    public class QueueService : IQueueService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string QueueName = "elevator-queue";

        public QueueService()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public Task EnqueueRequestAsync(FloorRequest request)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request));
            _channel.BasicPublish(exchange: "", routingKey: QueueName, basicProperties: null, body: body);
            return Task.CompletedTask;
        }
    }
}
