using Azure.Storage.Queues;

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
}
