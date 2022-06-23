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

        using (var transaction = _dbContext.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
        {
            try
            {
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

                if (form.Items.Any())
                {
                    foreach (var formItem in form.Items)
                    {
                        if (formItem.Locales.Any())
                        {
                            foreach (var formItemLocaled in formItem.Locales)
                            {
                                //formItem.Locales.Remove(formItemLocaled);
                                _dbContext.Set<FormItemLocaled>().Remove(formItemLocaled);
                            }

                            //formItem.Locales.Clear();
                        }

                        if (formItem.Options.Any())
                        {
                            foreach (var formItemOption in formItem.Options)
                            {
                                if (formItemOption.Locales.Any())
                                {
                                    _dbContext.Set<FormItemOptionLocaled>().RemoveRange(formItemOption.Locales);
                                    //foreach (var formItemOptionLocaled in formItemOption.Locales)
                                    //{
                                    //    formItemOption.Locales.Remove(formItemOptionLocaled);
                                    //}
                                    //formItemOption.Locales.Clear();
                                }

                                //formItem.Options.Remove(formItemOption);

                            }

                            _dbContext.Set<FormItemOption>().RemoveRange(formItem.Options);
                            //formItem.Options.Clear();
                        }

                        //form.Items.Remove(formItem);
                    }

                    //form.Items.Clear();
                    _dbContext.Set<FormItem>().RemoveRange(form.Items);
                }

                if (form.Locales.Any())
                {
                    //foreach (var formLocale in form.Locales)
                    //{
                    //    form.Locales.Remove(formLocale);
                    //}

                    //form.Locales.Clear();
                    _dbContext.Set<FormLocaled>().RemoveRange(form.Locales);
                }

                //_dbContext.Update(form);
                await _dbContext.SaveChangesAsync(cancellationToken);

                form = await _dbContext.Forms
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

                if (request.Items?.Any() ?? false)
                {
                    var formItemEntities = request.Items
                        .Select(x => _mapper.Map<FormItem>(x))
                        .ToList();

                    foreach (var formItemEntity in formItemEntities)
                    {
                        //formItemEntity.Id = Guid.NewGuid();
                        //formItemEntity.FormId = form.Id;

                        if (formItemEntity.Options.Any())
                        {
                            foreach (var formItemOption in formItemEntity.Options)
                            {
                                //formItemOption.Id = Guid.NewGuid();
                                formItemOption.FormItemId = formItemEntity.Id;

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

                                foreach (var formItemOptioinLocaled in formItemOption.Locales)
                                {
                                    formItemOptioinLocaled.FormItemOptionId = formItemOption.Id;

                                    _dbContext.Entry(formItemOptioinLocaled).State = EntityState.Added;
                                }

                                _dbContext.Entry(formItemOption).State = EntityState.Added;
                            }
                        }

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

                        foreach (var formItemLocaled in formItemEntity.Locales)
                        {
                            formItemLocaled.FormItemId = formItemEntity.Id;

                            _dbContext.Entry(formItemLocaled).State = EntityState.Added;
                        }

                        form.Items.Add(formItemEntity);

                        _dbContext.Entry(formItemEntity).State = EntityState.Added;
                    }
                }

                if (request.Locales?.Any() ?? false)
                {
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
                        formLocaledEntity.FormId = form.Id;
                        form.Locales.Add(formLocaledEntity);

                        _dbContext.Entry(formLocaledEntity).State = EntityState.Added;
                    }
                }

                _dbContext.Update(form);
                await _dbContext.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await transaction.RollbackAsync(cancellationToken);
            }
        }

        var reloadedForm = await _mediator.Send(new GetFormByIdQuery(request.Id), cancellationToken);

        return reloadedForm;
    }

    private readonly AppDbContext _dbContext;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
}