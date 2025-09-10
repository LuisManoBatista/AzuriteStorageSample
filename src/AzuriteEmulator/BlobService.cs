using System.Runtime.CompilerServices;
using Azure.Storage.Blobs;

namespace AzuriteEmulator;

public class BlobService : StorageServiceBase
{
    override protected string ServiceEndpointType => "BlobEndpoint";

    override protected string ServiceEndpointPort => "10000";
    public override async Task ExecuteAsync()
    {
        var client = new BlobContainerClient(ConnectionString, "my-images");

        var stream = await new HttpClient().GetStreamAsync("https://www.element61.be/sites/default/files/img_competences/Azure%2520Blob%2520Storage.png");

        var blob = client.GetBlobClient("AzureBlobStorage.png");
        await blob.UploadAsync(stream);

        Console.WriteLine($"Image {blob.Name} was uploaded successfully!");
    }
}
