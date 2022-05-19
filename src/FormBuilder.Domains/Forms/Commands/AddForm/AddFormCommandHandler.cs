using AutoMapper;
using FormBuilder.Data;
using FormBuilder.Domains.Forms.Models;
using FormBuilder.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FormBuilder.Domains.Forms.Commands.AddForm;

public class AddFormCommandHandler : IRequestHandler<AddFormCommand, FormModel>
{
    public AddFormCommandHandler(AppDbContext dbContext, IMapper mapper, ILogger<AddFormCommandHandler> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<FormModel> Handle(AddFormCommand request, CancellationToken cancellationToken = default)
    {
        var newForm = new Form
        {
            Title = request.Title,
            Content = request.Content,
        };
        
        var added = _dbContext.Forms.Add(newForm);

        await _dbContext.SaveChangesAsync(cancellationToken);

        var model = _mapper.Map<FormModel>(added.Entity);

        return model;
    }

    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
}