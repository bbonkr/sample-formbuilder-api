using AutoMapper;
using FormBuilder.Data;
using FormBuilder.Domains.Languages.Models;
using kr.bbon.Core.Models;
using kr.bbon.EntityFrameworkCore.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FormBuilder.Domains.Languages.Queries.GetLanguages;

public class GetLanguagesQueryHandler : IRequestHandler<GetLanguagesQuery,PagedModel<LanguageModel>>
{
    public GetLanguagesQueryHandler(AppDbContext dbContext, IMapper mapper, ILogger<GetLanguagesQueryHandler> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PagedModel<LanguageModel>> Handle(GetLanguagesQuery request,
        CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.Languages
            .WhereDependsOn(!string.IsNullOrWhiteSpace(request.Keyword),
                x => EF.Functions.Like(x.Name, $"{request.Keyword}%") ||
                     EF.Functions.Like(x.Code, $"{request.Keyword}%"))
            .OrderBy(x => x.Ordinal)
            .AsNoTracking()
            .Select(x => _mapper.Map<LanguageModel>(x))
            .ToPagedModelAsync(request.Page, request.Limit, cancellationToken);

        return result;
    }

    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    
}