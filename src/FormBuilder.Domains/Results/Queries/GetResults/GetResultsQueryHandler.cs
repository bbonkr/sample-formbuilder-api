using AutoMapper;
using FormBuilder.Data;
using FormBuilder.Domains.Results.Models;
using kr.bbon.Core.Models;
using kr.bbon.EntityFrameworkCore.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FormBuilder.Domains.Results.Queries.GetResults;

public class GetResultsQueryHandler : IRequestHandler<GetResultsQuery, PagedModel<ResultModel>>
{
    public GetResultsQueryHandler(AppDbContext dbContext, IMapper mapper, ILogger<GetResultsQueryHandler> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PagedModel<ResultModel>> Handle(GetResultsQuery request, CancellationToken cancellationToken = default)
    {
        var resultPagedModel = await _dbContext.Results
            .Include(x => x.Form)
            .Include(x => x.Items)
                .ThenInclude(x => x.Values)
            .Include(x => x.Items)
                .ThenInclude(x => x.FormItem)
            .OrderByDescending(x => x.CreatedAt)
            .WhereDependsOn(request.FormId.HasValue, x => x.FormId == request.FormId)
            .Select(x => _mapper.Map<ResultModel>(x))
            .ToPagedModelAsync(request.Page, request.Limit, cancellationToken);

        return resultPagedModel;
    }

    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
}