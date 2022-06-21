using AutoMapper;
using FormBuilder.Data;
using FormBuilder.Domains.Results.Models;
using FormBuilder.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FormBuilder.Domains.Results.Commands.AddResult;

public class AddResultCommandHandler : IRequestHandler<AddResultCommand, ResultModel>
{
    public AddResultCommandHandler(AppDbContext dbContext, IMapper mapper, ILogger<AddResultCommandHandler> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResultModel> Handle(AddResultCommand request, CancellationToken cancellationToken = default)
    {
        var result = new Result
        {
            FormId = request.FormId,
            // TODO: remove
            Content = request.Content,
            Items = request.Items.Select(x => new ResultItem
            {
                FormItemId = x.FormItemId,
                Values = x.Values.Select(v => new ResultItemValue
                {
                    Value = v.Value,
                }).ToList(),
            }).ToList(),
        };

        var added = _dbContext.Results.Add(result);
        await _dbContext.SaveChangesAsync(cancellationToken);
        var model = _mapper.Map<ResultModel>(added.Entity);

        return model;
    }

    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
}