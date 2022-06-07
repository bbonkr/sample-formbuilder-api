using AutoMapper;
using FormBuilder.Data;
using FormBuilder.Domains.Forms.Models;
using kr.bbon.Core.Models;
using kr.bbon.EntityFrameworkCore.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FormBuilder.Domains.Forms.Queries.GetForms;

public class GetFormsQueryHandler : IRequestHandler<GetFormsQuery, PagedModel<FormModel>>
{
    public GetFormsQueryHandler(AppDbContext dbContext, IMapper mapper, ILogger<GetFormsQueryHandler> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PagedModel<FormModel>> Handle(GetFormsQuery request, CancellationToken cancellationToken = default)
    {
        var formPagedModel = await _dbContext.Forms
            .Include(x => x.Items.OrderBy(item => item.Ordinal))
                .ThenInclude(x => x.Options.OrderBy(option => option.Ordinal))
            .Include(x => x.Results)
            .WhereDependsOn(!string.IsNullOrWhiteSpace(request.Keyword),
                x => EF.Functions.Like(x.Title, $"%{request.Keyword}%"))
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => _mapper.Map<FormModel>(x))
            .ToPagedModelAsync(request.Page, request.Limit, cancellationToken);

        return formPagedModel;
    }

    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
}