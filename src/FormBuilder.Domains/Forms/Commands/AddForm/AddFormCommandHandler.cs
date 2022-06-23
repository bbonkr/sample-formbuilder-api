using System.Net;
using AutoMapper;
using FormBuilder.Data;
using FormBuilder.Domains.Forms.Models;
using FormBuilder.Domains.Forms.Queries.GetFormById;
using FormBuilder.Entities;
using kr.bbon.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FormBuilder.Domains.Forms.Commands.AddForm;

public class AddFormCommandHandler : IRequestHandler<AddFormCommand, FormModel>
{
    public AddFormCommandHandler(AppDbContext dbContext, IMediator mediator, IMapper mapper, ILogger<AddFormCommandHandler> logger)
    {
        _dbContext = dbContext;
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<FormModel> Handle(AddFormCommand request, CancellationToken cancellationToken = default)
    {
        var defaultLanguage = _dbContext.Languages
            .OrderBy(x => x.Ordinal).FirstOrDefault();

        if (defaultLanguage == null)
        {
            throw new ApiException(HttpStatusCode.NotFound, "Could not find default language information");
        }

        var newForm = new Form
        {
            Title = request.Title,
            //Content = string.Empty,
        };

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

                newForm.Items.Add(formItemEntity);
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
                FormId = newForm.Id,
                LanguageId = defaultLanguage.Id,
                Title = request.Title,
            };

            formLocaledEntities.Add(defaultLocaledItem);
        }

        foreach (var formLocaledEntity in formLocaledEntities)
        {
            newForm.Locales.Add(formLocaledEntity);
        }

        var added = _dbContext.Forms.Add(newForm);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var adeddFormModel = await _mediator.Send(new GetFormByIdQuery(added.Entity.Id), cancellationToken);

        return adeddFormModel;
    }

    private readonly AppDbContext _dbContext;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
}