using System.Net;
using AutoMapper;
using FormBuilder.Data;
using FormBuilder.Domains.Forms.Models;
using kr.bbon.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FormBuilder.Domains.Forms.Commands.UpdateForm;

public class UpdateFormCommandHandler : IRequestHandler<UpdateFormCommand, FormModel>
{
    public UpdateFormCommandHandler(AppDbContext dbContext, IMapper mapper, ILogger<UpdateFormCommandHandler> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<FormModel> Handle(UpdateFormCommand request, CancellationToken cancellationToken = default)
    {
        var form = await _dbContext.Forms
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (form == null)
        {
            throw new ApiException(HttpStatusCode.NotFound);
        }

        form.Title = request.Title;
        form.Content = request.Content;

        var updated = _dbContext.Update(form);
        await _dbContext.SaveChangesAsync(cancellationToken);
        var model = _mapper.Map<FormModel>(form);

        return model;
    }

    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
}