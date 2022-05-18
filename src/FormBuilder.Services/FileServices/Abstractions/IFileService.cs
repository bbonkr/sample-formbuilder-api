using FormBuilder.Services.FileServices.Models;

namespace FormBuilder.Services.FileServices.Abstractions;

public interface IFileService
{
    public Task<FileModel> SaveAsAsync(byte[] fileContent, string containerName, string name, string contentType = "application/octet-stream", CancellationToken cancellationToken = default);

    public Task<FileModel> GetAsync(string uri, CancellationToken cancellationToken = default);

    public Task DeleteAsync(string uri, CancellationToken cancellationToken = default);
}