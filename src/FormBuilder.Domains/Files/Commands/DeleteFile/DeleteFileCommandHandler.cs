using System.Net;
using AutoMapper;
using FormBuilder.Data;
using FormBuilder.Services.FileServices;
using kr.bbon.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FormBuilder.Domains.Files.Commands.DeleteFile;

public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, Unit>
{
    public DeleteFileCommandHandler(AppDbContext dbContext, IMapper mapper, AzureBlobStorageFileService azureBlobStorageFileService, ILogger<DeleteFileCommandHandler> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _azureBlobStorageFileService = azureBlobStorageFileService;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteFileCommand request, CancellationToken cancellationToken = default)
    {
        var blobName = await _azureBlobStorageFileService.DeleteAsync(request.Uri, cancellationToken);

        var deleteCandidate = await _dbContext.Files.Where(x => x.Name == blobName)
            .FirstOrDefaultAsync(cancellationToken);
        if (deleteCandidate == null)
        {
            throw new ApiException(HttpStatusCode.NotFound);
        }

        _dbContext.Remove(deleteCandidate);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly AzureBlobStorageFileService _azureBlobStorageFileService;
}