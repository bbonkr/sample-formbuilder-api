using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
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
            await containerClient.CreateAsync(publicAccessType: Azure.Storage.Blobs.Models.PublicAccessType.None, cancellationToken: cancellationToken);
        }

        var blobName = name;

        var tokens = name.Split('.');
        var extension = string.Empty;
        if (tokens.Count() > 1)
        {
            extension = tokens.Last();
            var tempTokens = new List<string>(tokens.Take(tokens.Count() - 1));
            tempTokens.Add(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString());
            if (!string.IsNullOrWhiteSpace(extension))
            {
                tempTokens.Add(extension);
            }
            blobName = string.Join(".", tempTokens.ToArray());
        }

        await containerClient.UploadBlobAsync(blobName, new BinaryData(fileContent), cancellationToken);

        var blobClient = containerClient.GetBlobClient(blobName);

        var properties = await blobClient.GetPropertiesAsync();

        var model = new FileModel
        {
            Name = blobName,
            ContainerName = containerName,
            ContentType = properties.Value.ContentType,
            Extension = extension,
            Size = properties.Value.ContentLength,
            Uri = blobClient.Uri.ToString(),
        };

        return model;
    }

    public async Task<FileModel> GetAsync(string uri, CancellationToken cancellationToken = default)
    {
        var model = new FileModel();
        var blobClient = new BlobClient(new Uri(uri),
            new StorageSharedKeyCredential(_options.AccountName, _options.AccountKey));

        var containerClient = _blobServiceClient.GetBlobContainerClient(blobClient.BlobContainerName);
        var actualBlobClient = containerClient.GetBlobClient(blobClient.Name);

        var properties = await actualBlobClient.GetPropertiesAsync(cancellationToken: cancellationToken);

        model.ContainerName = actualBlobClient.BlobContainerName;
        model.Name = actualBlobClient.Name;
        model.Size = properties.Value.ContentLength;
        model.ContentType = properties.Value.ContentType;

        var memoryStream = new MemoryStream();

        await actualBlobClient.DownloadToAsync(memoryStream, cancellationToken);

        memoryStream.Position = 0;

        model.Stream = memoryStream;

        return model;
    }

    public async Task<string> DeleteAsync(string uri, CancellationToken cancellationToken = default)
    {
        var blobClient = new BlobClient(new Uri(uri),
            new StorageSharedKeyCredential(_options.AccountName, _options.AccountKey));
        var blobName = blobClient.Name;

        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(blobClient.BlobContainerName);
        var actualBlobClient = blobContainerClient.GetBlobClient(blobClient.Name);

        var isExists = await actualBlobClient.ExistsAsync(cancellationToken);
        if (!isExists)
        {
            throw new Exception("FIle not found");
        }

        await actualBlobClient.DeleteAsync(cancellationToken: cancellationToken);

        return blobName;
    }

    public string GetUriForDelete(string uri, DateTimeOffset? expiresOn = null)
    {
        var actualExpiresOn = expiresOn ?? DateTimeOffset.Now.AddHours(1);

        var blobClient = new BlobClient(new Uri(uri),
            new StorageSharedKeyCredential(_options.AccountName, _options.AccountKey));

        var builder = new BlobSasBuilder()
        {
            BlobContainerName = blobClient.BlobContainerName,
            Resource = "b",
        };

        //builder.SetPermissions(BlobSasPermissions.Read);
        builder.SetPermissions(BlobSasPermissions.Write);
        builder.SetPermissions(BlobSasPermissions.Delete);
        builder.SetPermissions(BlobSasPermissions.PermanentDelete);
        builder.ExpiresOn = actualExpiresOn;

        var sasUri = blobClient.GenerateSasUri(builder);

        return sasUri.ToString();
    }

    private AzureBlobStorageFileServiceOptions _options;
    private BlobServiceClient _blobServiceClient;
}