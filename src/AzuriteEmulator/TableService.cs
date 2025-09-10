using Azure.Data.Tables;

namespace AzuriteEmulator;

public class TableService : StorageServiceBase
{
    override protected string ServiceEndpointType => "TableEndpoint";

    override protected string ServiceEndpointPort => "10002";

    public override Task ExecuteAsync()
    {
        var client = new TableClient(ConnectionString, "products");

        var entities = new List<MyEntity>
        {
            new() {
                PartitionKey = "Clothing",
                RowKey = Guid.NewGuid().ToString(),
                Name = "T-Shirt",
                Price = 49,
                Quantity = 5
            },
            new() {
                PartitionKey = "Clothing",
                RowKey = Guid.NewGuid().ToString(),
                Name = "Jeans",
                Price = 79,
                Quantity = 3
            }
        };

        entities.ForEach(async e =>
        {
            await client.AddEntityAsync(e);
            Console.WriteLine($"{e.Name} added to the product table.");
        });

        return Task.CompletedTask;
    }
}