
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AzuriteEmulator;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddLogging();
        // Register QueueSubscriberService as a hosted service
        services.AddHostedService(provider =>
        {
            var logger = provider.GetRequiredService<ILogger<QueueService.QueueSubscriberService>>();
            // Use the same connection string and queue name as QueueService
            var queueService = new QueueService();
            return new QueueService.QueueSubscriberService(queueService.ConnectionString ?? "UseYourConnectionStringHere", "my-queue", logger);
        });
    });

var host = builder.Build();

// Run storage services in parallel
List<IStorageService> storageServices = [
    new BlobService(),
    new QueueService(),
    new TableService()
];

var storageTasks = storageServices.Select(x => x.ExecuteAsync());
await Task.WhenAll(storageTasks);

await host.RunAsync();
