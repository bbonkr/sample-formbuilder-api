using AutoMapper;
using FormBuilder.Data;
using FormBuilder.Domains.Forms.Models;
using kr.bbon.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FormBuilder.Domains.Forms.Queries.GetLocalizedFormById;

public class GetLocalizedFormByIdQueryHandler : IRequestHandler<GetLocalizedFormByIdQuery, FormModel>
{
    public GetLocalizedFormByIdQueryHandler(AppDbContext dbContext, IMapper mapper, ILogger<GetLocalizedFormByIdQueryHandler> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<FormModel> Handle(GetLocalizedFormByIdQuery request, CancellationToken cancellationToken = default)
    {
        var language = await _dbContext.Languages
            .FirstOrDefaultAsync(x => x.Code == request.LanguageCode, cancellationToken);

        if (language == null)
        {
            language = await _dbContext.Languages
            .FirstOrDefaultAsync(x => x.Code == "en", cancellationToken);
        }

        if (language == null)
        {
            throw new ApiException(System.Net.HttpStatusCode.NotFound);
        }

        var formModel = _dbContext.Forms
            .Include(x => x.Items.OrderBy(item => item.Ordinal))
                .ThenInclude(x => x.Options.OrderBy(option => option.Ordinal))
                    .ThenInclude(x => x.Locales)
                        .ThenInclude(x => x.Language)
            .Include(x => x.Items.OrderBy(item => item.Ordinal))
                .ThenInclude(x => x.Locales)
                    .ThenInclude(x => x.Language)
            .Include(x => x.Locales)
                .ThenInclude(x => x.Language)
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => _mapper.Map<FormModel>(x))
            .FirstOrDefault()
            ;

        //query.Load();

        //var form = query.FirstOrDefault();

        //var formModel = _mapper.Map<FormModel>(form);

        if (formModel == null)
        {
            throw new ApiException(System.Net.HttpStatusCode.NotFound);
        }

        // Replace localized value
        if (formModel.Locales.Any(x => x.LanguageId == language.Id))
        {
            var localizedForm = formModel.Locales.FirstOrDefault(x => x.LanguageId == language.Id);
            if (localizedForm != null)
            {
                formModel.Title = localizedForm.Title;
            }
        }

        formModel.Locales = Enumerable.Empty<FormLocaledModel>();

        foreach (var formItem in formModel.Items)
        {
            if (formItem.Locales.Any(x => x.LanguageId == language.Id))
            {
                var localizedFormItem = formItem.Locales.FirstOrDefault(x => x.LanguageId == language.Id);
                if (localizedFormItem != null)
                {
                    formItem.Label = localizedFormItem.Label;
                    formItem.Description = localizedFormItem.Description;
                    formItem.Placeholder = localizedFormItem.Placeholder;
                }

                foreach (var formItemOption in formItem.Options)
                {
                    if (formItemOption.Locales.Any(x => x.LanguageId == language.Id))
                    {
                        var localizedFormItemOption = formItemOption.Locales.FirstOrDefault(x => x.LanguageId == language.Id);

                        if (localizedFormItemOption != null)
                        {
                            formItemOption.Text = localizedFormItemOption.Text;
                        }
                    }

                    formItemOption.Locales = Enumerable.Empty<FormItemOptionLocaledModel>();
                }
            }

            formItem.Locales = Enumerable.Empty<FormItemLocaledModel>();
        }

        return formModel;
    }

    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
}