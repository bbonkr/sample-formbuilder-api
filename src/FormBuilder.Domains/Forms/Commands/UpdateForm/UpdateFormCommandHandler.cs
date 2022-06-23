using System.Net;
using AutoMapper;
using FormBuilder.Data;
using FormBuilder.Domains.Forms.Models;
using FormBuilder.Domains.Forms.Queries.GetFormById;
using FormBuilder.Entities;
using kr.bbon.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FormBuilder.Domains.Forms.Commands.UpdateForm;

public class UpdateFormCommandHandler : IRequestHandler<UpdateFormCommand, FormModel>
{
    public UpdateFormCommandHandler(AppDbContext dbContext, IMediator mediator, IMapper mapper, ILogger<UpdateFormCommandHandler> logger)
    {
        _dbContext = dbContext;
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<FormModel> Handle(UpdateFormCommand request, CancellationToken cancellationToken = default)
    {
        var hasRespondings = _dbContext.Results
            .Any(x => x.FormId == request.Id);

        if (hasRespondings)
        {
            throw new ApiException(HttpStatusCode.NotAcceptable, "The form Could not modify. This form has result data.");
        }

        var defaultLanguage = _dbContext.Languages
            .OrderBy(x => x.Ordinal).FirstOrDefault();

        if (defaultLanguage == null)
        {
            throw new ApiException(HttpStatusCode.NotFound, "Could not find default language information");
        }

        var form = await _dbContext.Forms
            .Include(x => x.Locales)
            .Include(x => x.Items)
                .ThenInclude(x => x.Options)
                    .ThenInclude(x => x.Locales)
            .Include(x => x.Items)
                .ThenInclude(x => x.Locales)
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (form == null)
        {
            throw new ApiException(HttpStatusCode.NotFound);
        }

        form.Title = request.Title;

        if (form.Items.Any())
        {
            form.Items.Clear();
        }

        if (form.Locales.Any())
        {
            form.Locales.Clear();
        }

        if (request.Items?.Any() ?? false)
        {
            var formItemEntities = request.Items
                .Select(x => _mapper.Map<FormItem>(x))
                .ToList();

            foreach (var formItemEntity in formItemEntities)
            {
                var hasDefaultFormItemLocaled = formItemEntity.Locales
                    .Any(x => x.LanguageId == defaultLanguage.Id);

                if (!hasDefaultFormItemLocaled)
                {
                    var defaultFormItemLocaled = new FormItemLocaled
                    {
                        FormItemId = formItemEntity.Id,
                        LanguageId = defaultLanguage.Id,
                        Label = formItemEntity.Label,
                        Description = formItemEntity.Description,
                        Placeholder = formItemEntity.Placeholder,
                    };
                    formItemEntity.Locales.Add(defaultFormItemLocaled);
                }

                if (formItemEntity.Options.Any())
                {
                    foreach (var formItemOption in formItemEntity.Options)
                    {
                        var hasDefaultLocaled = formItemOption.Locales.Any(x => x.LanguageId == defaultLanguage.Id);

                        if (!hasDefaultLocaled)
                        {
                            var defaultItemOptionLocaled = new FormItemOptionLocaled
                            {
                                FormItemOptionId = formItemOption.Id,
                                LanguageId = defaultLanguage.Id,
                                Text = formItemOption.Text,
                            };

                            formItemOption.Locales.Add(defaultItemOptionLocaled);
                        }
                    }
                }

                form.Items.Add(formItemEntity);
            }
        }

        var formLocaledEntities = request.Locales
            .Select(x => _mapper.Map<FormLocaled>(x))
            .ToList();

        var hasDefaultLocale = request.Locales
            .Any(x => x.LanguageCode == defaultLanguage.Code);

        if (!hasDefaultLocale)
        {
            var defaultLocaledItem = new FormLocaled
            {
                FormId = form.Id,
                LanguageId = defaultLanguage.Id,
                Title = request.Title,
            };

            formLocaledEntities.Add(defaultLocaledItem);
        }

        foreach (var formLocaledEntity in formLocaledEntities)
        {
            form.Locales.Add(formLocaledEntity);
        }

        var updated = _dbContext.Update(form);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var reloadedForm = await _mediator.Send(new GetFormByIdQuery(form.Id), cancellationToken);

        return reloadedForm;
    }

    private readonly AppDbContext _dbContext;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
}