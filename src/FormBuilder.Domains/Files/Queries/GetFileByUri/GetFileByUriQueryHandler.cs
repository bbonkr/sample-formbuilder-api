using System.Net;
using AutoMapper;
using FormBuilder.Data;
using FormBuilder.Domains.Files.Models;
using FormBuilder.Services.FileServices;
using kr.bbon.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FormBuilder.Domains.Files.Queries.GetFileByUri;

public class GetFileByUriQueryHandler : IRequestHandler<GetFileByUriQuery, DownloadFileMediaModel>
{
    public GetFileByUriQueryHandler(AppDbContext dbContext, IMapper mapper, AzureBlobStorageFileService azureBlobStorageFileService, ILogger<GetFileByUriQueryHandler> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _azureBlobStorageFileService = azureBlobStorageFileService;
        _logger = logger;
    }

    public async Task<DownloadFileMediaModel> Handle(GetFileByUriQuery request,
        CancellationToken cancellationToken = default)
    {
        var blob = await _azureBlobStorageFileService.GetAsync(request.Uri, cancellationToken);

        if (blob.FileContent == null && blob.Stream == null)
        {
            throw new ApiException(HttpStatusCode.NotFound);
        }

        var model = new DownloadFileMediaModel
        {
            Name = blob.Name,
            ContentType = blob.ContentType,
            Size = blob.Size,
            Content = blob.FileContent,
            Stream = blob.Stream,
        };

        return model;
    }

    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly AzureBlobStorageFileService _azureBlobStorageFileService;
}