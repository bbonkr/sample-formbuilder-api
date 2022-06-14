using AutoMapper;
using FormBuilder.Data;
using FormBuilder.Domains.Forms.Models;
using FormBuilder.Domains.Forms.Queries.GetFormById;
using FormBuilder.Entities;
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
        var newForm = new Form
        {
            Title = request.Title,
            Content = string.Empty,
        };
        
        if (request.Items?.Any() ?? false)
        {
            foreach (var item in request.Items)
            {
                var formItem = _mapper.Map<FormItem>(item);

                if (item.Locales.Any())
                {
                    foreach (var itemLocaled in item.Locales)
                    {
                        var formItemLocaled = _mapper.Map<FormItemLocaled>(itemLocaled);

                        formItem.Locales.Add(formItemLocaled);
                    }
                }

                if (item.Options.Any())
                {
                    foreach (var itemOption in item.Options)
                    {
                        var formItemOption = _mapper.Map<FormItemOption>(itemOption);

                        if (itemOption.Locales.Any())
                        {
                            foreach (var itemOptionLocaled in itemOption.Locales)
                            {
                                var formItemOptionLocaled = _mapper.Map<FormItemOptionLocaled>(itemOptionLocaled);

                                formItemOption.Locales.Add(formItemOptionLocaled);
                            }
                        }
                        
                        formItem.Options.Add(formItemOption);
                    }
                }

                newForm.Items.Add(formItem);
            }
        }

        if (request.Locales?.Any() ?? false)
        {
            foreach (var localed in request.Locales)
            {
                var formLocaled = _mapper.Map<FormLocaled>(localed);

                newForm.Locales.Add(formLocaled);
            }
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