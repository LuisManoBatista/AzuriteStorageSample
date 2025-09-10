namespace AzuriteEmulator;

public abstract class StorageServiceBase : IStorageService
{
    protected virtual string EndpointsProtocol => "http";

    protected virtual string AccountName => "devstoreaccount1";

    protected virtual string AccountKey => "Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==";

    protected abstract string ServiceEndpointType { get; }

    protected abstract string ServiceEndpointPort { get; }

    protected virtual string ServiceEndpointUrl => $"{EndpointsProtocol}://127.0.0.1:{ServiceEndpointPort}/{AccountName}";

    public virtual string ConnectionString => $"DefaultEndpointsProtocol={EndpointsProtocol};AccountName={AccountName};AccountKey={AccountKey};{ServiceEndpointType}={ServiceEndpointUrl};";

    public abstract Task ExecuteAsync();
}
