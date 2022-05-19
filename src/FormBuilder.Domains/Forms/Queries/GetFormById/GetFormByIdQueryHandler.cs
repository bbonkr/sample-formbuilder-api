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
        var formModel = await _dbContext.Forms.Where(x => x.Id == request.Id)
            .Select(x => _mapper.Map<FormModel>(x))
            .FirstOrDefaultAsync();

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