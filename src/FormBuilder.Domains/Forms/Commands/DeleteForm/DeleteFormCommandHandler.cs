using System.Net;
using AutoMapper;
using FormBuilder.Data;
using kr.bbon.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FormBuilder.Domains.Forms.Commands.DeleteForm;

public class DeleteFormCommandHandler : IRequestHandler<DeleteFormCommand, Unit>
{
    public DeleteFormCommandHandler(AppDbContext dbContext, IMapper mapper, ILogger<DeleteFormCommandHandler> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteFormCommand request, CancellationToken cancellationToken = default)
    {
        var hasRespondings = _dbContext.Results
            .Any(x => x.FormId == request.Id);

        if (hasRespondings)
        {
            throw new ApiException(HttpStatusCode.NotAcceptable, "The form Could not delete. This form has result data.");
        }

        var form = await _dbContext.Forms
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (form == null)
        {
            throw new ApiException(HttpStatusCode.NotFound);
        }

        _dbContext.Remove(form);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
}