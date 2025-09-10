
using Azure;
using Azure.Data.Tables;

namespace AzuriteEmulator;

public class MyEntity : ITableEntity
{
    public required string PartitionKey { get; set; }

    public required string RowKey { get; set; }

    public required string Name { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }   

    public DateTimeOffset? Timestamp { get; set; }

    public ETag ETag { get; set; }
}
