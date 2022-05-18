using Azure.Storage.Blobs;
using FormBuilder.Services.FileServices.Abstractions;
using FormBuilder.Services.FileServices.Models;
using Microsoft.Extensions.Options;

namespace FormBuilder.Services.FileServices;

public class AzureBlobStorageFileService : IFileService
{
    public AzureBlobStorageFileService(IOptionsMonitor<AzureBlobStorageFileServiceOptions> optionsAccessrot)
    {
        _options = optionsAccessrot.CurrentValue ??
                   throw new ArgumentException("Please configure AzureBlobStorage section in appsettings");

        _blobServiceClient = new BlobServiceClient(_options.ConnectionString);
    }

    public async Task<FileModel> SaveAsAsync(byte[] fileContent, string containerName, string name,
        string contentType = "application/octet-stream", CancellationToken cancellationToken = default)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var isExists = await containerClient.ExistsAsync();
        if (!isExists)
        {
            containerClient.CreateAsync(cancellationToken: cancellationToken);
        }

        var blobName = name;
        var response = await containerClient.UploadBlobAsync(blobName, new BinaryData(fileContent), cancellationToken);
        var blobClient = containerClient.GetBlobClient(blobName);

        var properties = await blobClient.GetPropertiesAsync();

        var model = new FileModel
        {
            Name = blobName,
            ContainerName = containerName,
            ContentType = properties.Value.ContentType,
            Extension = "",
            Size = properties.Value.ContentLength,
            Uri = blobClient.Uri.ToString(),
        };

        return model;
    }

    public async Task<FileModel> GetAsync(string uri, CancellationToken cancellationToken = default)
    {
        var model = new FileModel();
        var blobClient = new BlobClient(new Uri(uri));
        var properties = await blobClient.GetPropertiesAsync(cancellationToken: cancellationToken);

        model.ContainerName = blobClient.BlobContainerName;
        model.Name = blobClient.Name;
        model.Size = properties.Value.ContentLength;
        model.ContentType = properties.Value.ContentType;

        using var memoryStream = new MemoryStream();
        var res = await blobClient.DownloadStreamingAsync();
        await res.Value.Content.CopyToAsync(memoryStream);
        await memoryStream.FlushAsync();

        model.FileContent = memoryStream.ToArray();

        return model;
    }

    public async Task DeleteAsync(string uri, CancellationToken cancellationToken = default)
    {
        var blobClient = new BlobClient(new Uri(uri));
        var isExists = await blobClient.ExistsAsync(cancellationToken:cancellationToken);
        
        if (!isExists)
        {
            throw new Exception("FIle not found");
        }

        await blobClient.DeleteAsync(cancellationToken: cancellationToken);
    }

    private AzureBlobStorageFileServiceOptions _options;
    private BlobServiceClient _blobServiceClient;
}