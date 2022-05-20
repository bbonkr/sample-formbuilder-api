using AutoMapper;
using FormBuilder.Data;
using FormBuilder.Domains.Files.Models;
using FormBuilder.Domains.Files.Queries.GetFileByUri;
using FormBuilder.Entities;
using FormBuilder.Services.FileServices;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FormBuilder.Domains.Files.Commands.Upload;

public class UploadCommandHandler:IRequestHandler<UploadCommand, UploadFileMediaModel>
{
    public UploadCommandHandler(AppDbContext dbContext, IMapper mapper, AzureBlobStorageFileService azureBlobStorageFileService, ILogger<UploadCommandHandler> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _azureBlobStorageFileService = azureBlobStorageFileService;
        _logger = logger;
    }

    public async Task<UploadFileMediaModel> Handle(UploadCommand request, CancellationToken cancellationToken = default)
    {
        var fileName = request.Name;
        var containerName = request.ContainerName;
        var savedFile = await _azureBlobStorageFileService.SaveAsAsync(
            request.FileContent.ToArray(),
            containerName,
            fileName,
            request.ContentType,
            cancellationToken);

        var fileMedia = new FileMedia
        {
            Name = savedFile.Name,
            ContainerName = savedFile.ContainerName,
            Size = savedFile.Size,
            ContentType = savedFile.ContentType,
            Uri = savedFile.Uri,
            Path = savedFile.Uri,
        };

        _dbContext.Files.Add(fileMedia);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var uriForDeletion = _azureBlobStorageFileService.GetUriForDelete(savedFile.Uri);

        var resultModel = new UploadFileMediaModel
        {
            Name = fileMedia.Name,
            ContentType = fileMedia.ContentType,
            Content = null,
            Size = fileMedia.Size,
            Uri = savedFile.Uri,
            UriForDeletion = uriForDeletion,
        };

        return resultModel;
    }

    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly AzureBlobStorageFileService _azureBlobStorageFileService;
}