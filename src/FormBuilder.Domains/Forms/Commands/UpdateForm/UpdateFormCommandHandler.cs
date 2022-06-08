using System.Net;
using AutoMapper;
using FormBuilder.Data;
using FormBuilder.Domains.Forms.Models;
using FormBuilder.Entities;
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
            .Include(x => x.Items)
                .ThenInclude(x => x.Options)
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (form == null)
        {
            throw new ApiException(HttpStatusCode.NotFound);
        }

        form.Title = request.Title;
        // TODO remove
        form.Content = string.Empty; // request.Content;

        if (request.Items.Count > 0)
        {
            _dbContext.FormItems.RemoveRange(form.Items);

            foreach (var item in request.Items)
            {
                var formItem = _mapper.Map<FormItem>(item);
                _dbContext.FormItems.Add(formItem);
            }
        }

        var updated = _dbContext.Update(form);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var reloadedForm = await _dbContext.Forms
            .Include(x => x.Items)
                .ThenInclude(x => x.Options)
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        var model = _mapper.Map<FormModel>(reloadedForm);

        return model;
    }

    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
}