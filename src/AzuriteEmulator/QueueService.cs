using Azure.Storage.Queues;


using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
namespace AzuriteEmulator;

public class QueueService : StorageServiceBase
{
    override protected string ServiceEndpointType => "QueueEndpoint";

    override protected string ServiceEndpointPort => "10001";

    public override async Task ExecuteAsync()
    {
        var client = new QueueClient(ConnectionString, "my-queue");
        await client.SendMessageAsync("message #1");
        await client.SendMessageAsync("message #2");
        var peekedMessages = (await client.PeekMessagesAsync(maxMessages: 2)).Value.Select(x => x.Body.ToString());
        foreach (var message in peekedMessages)
        {
            Console.WriteLine($"{message} peeked from the queue");
        }
    }

    // BackgroundService implementation for subscriber
    public class QueueSubscriberService : BackgroundService
    {
        private readonly string _connectionString;
        private readonly string _queueName;
        private readonly ILogger<QueueSubscriberService> _logger;

        public QueueSubscriberService(string connectionString, string queueName, ILogger<QueueSubscriberService> logger)
        {
            _connectionString = connectionString;
            _queueName = queueName;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var client = new QueueClient(_connectionString, _queueName);
            _logger.LogInformation($"QueueSubscriberService started for queue: {_queueName}");
            while (!stoppingToken.IsCancellationRequested)
            {
                var messages = await client.ReceiveMessagesAsync(maxMessages: 10, visibilityTimeout: TimeSpan.FromSeconds(30), cancellationToken: stoppingToken);
                foreach (var message in messages.Value)
                {
                    _logger.LogInformation($"Received: {message.Body}");
                    // Process the message here
                    await client.DeleteMessageAsync(message.MessageId, message.PopReceipt, stoppingToken);
                }
                await Task.Delay(1000, stoppingToken);
            }
            _logger.LogInformation("QueueSubscriberService stopped.");
        }
    }
}
