using AzuriteEmulator;  

List<IStorageService> storageServices = [
        new BlobService(),
        new QueueService(),
        new TableService()
    ];

var storageTasks = storageServices.Select(x => x.ExecuteAsync());
await Task.WhenAll(storageTasks);
// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");
