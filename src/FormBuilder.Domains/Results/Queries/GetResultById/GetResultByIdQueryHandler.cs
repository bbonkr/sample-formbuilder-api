using System.Net;
using AutoMapper;
using FormBuilder.Data;
using FormBuilder.Domains.Results.Models;
using kr.bbon.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FormBuilder.Domains.Results.Queries.GetResultById;

public class GetResultByIdQueryHandler : IRequestHandler<GetResultByIdQuery, ResultModel>
{
    public GetResultByIdQueryHandler(AppDbContext dbContext, IMapper mapper, ILogger<GetResultByIdQueryHandler> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<ResultModel> Handle(GetResultByIdQuery request, CancellationToken cancellationToken = default)
    {
        var resultModel = await _dbContext.Results
            .Where(x => x.Id == request.Id)
            .Select(x => _mapper.Map<ResultModel>(x))
            .FirstOrDefaultAsync();

        if (resultModel == null)
        {
            throw new ApiException(HttpStatusCode.NotFound);
        }

        return resultModel;
    }
    
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
}