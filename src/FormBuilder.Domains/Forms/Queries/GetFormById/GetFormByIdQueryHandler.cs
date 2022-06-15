using System.Net;
using AutoMapper;
using FormBuilder.Data;
using FormBuilder.Domains.Forms.Models;
using kr.bbon.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FormBuilder.Domains.Forms.Queries.GetFormById;

public class GetFormByIdQueryHandler : IRequestHandler<GetFormByIdQuery, FormModel>
{
    public GetFormByIdQueryHandler(AppDbContext dbContext, IMapper mapper, ILogger<GetFormByIdQueryHandler> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<FormModel> Handle(GetFormByIdQuery request, CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.Forms
            .Include(x=>x.Locales)
                // .ThenInclude(x=>x.Language)
            .Include(x => x.Items.OrderBy(item => item.Ordinal))
                .ThenInclude(x=>x.Locales)
                    // .ThenInclude(x=>x.Language)
            .Include(x => x.Items.OrderBy(item => item.Ordinal))
                .ThenInclude(x => x.Options.OrderBy(option => option.Ordinal))
                    .ThenInclude(x=>x.Locales)
                        // .ThenInclude(x=>x.Language)
            .Include(x => x.Results)
            .Where(x => x.Id == request.Id)
            // .Select(x => _mapper.Map<FormModel>(x))
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);


        var formModel = _mapper.Map<FormModel>(result);
        
        if (formModel == null)
        {
            throw new ApiException(HttpStatusCode.NotFound);
        }

        return formModel;
    }

    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
}