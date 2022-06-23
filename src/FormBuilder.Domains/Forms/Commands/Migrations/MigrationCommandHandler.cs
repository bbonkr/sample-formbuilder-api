using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using FormBuilder.Data;
using FormBuilder.Domains.Forms.Models;
using FormBuilder.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FormBuilder.Domains.Forms.Commands.Migrations;

public class MigrationCommandHandler : IRequestHandler<MigrationCommand, Unit>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public MigrationCommandHandler(AppDbContext dbContext, IMapper mapper, ILogger<MigrationCommandHandler> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public Task<Unit> Handle(MigrationCommand request, CancellationToken cancellationToken = default)
    {
        //var jsonSerializeOptions = new JsonSerializerOptions()
        //{
        //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        //    IgnoreReadOnlyFields = true,
        //    AllowTrailingCommas = true,
        //};
        //jsonSerializeOptions.Converters.Add(new NumberToGuidConverter());

        //var ids = _dbContext.Forms.Select(x => x.Id).ToList();

        //var migrated = 0;

        //using (var transaction = _dbContext.Database.BeginTransaction())
        //{
        //    try
        //    {
        //        foreach (var id in ids)
        //        {
        //            var form = await _dbContext.Forms
        //                .Where(x => x.Id == id)
        //                .AsNoTracking()
        //                .FirstOrDefaultAsync(cancellationToken);

        //            if (form == null)
        //            {
        //                continue;
        //            }

        //            if (form.Items.Any())
        //            {
        //                continue;
        //            }

        //            if (!string.IsNullOrWhiteSpace(form.Content))
        //            {
        //                var formTemp = JsonSerializer.Deserialize<FormTempModel>(form.Content, jsonSerializeOptions);

        //                if (formTemp != null)
        //                {

        //                    var formItems = formTemp.Items.Select((formItemTemp, index) =>
        //                    {
        //                        var formItemId = Guid.NewGuid();

        //                        formItemTemp.Id = formItemId.ToString();
        //                        formItemTemp.FormId = id;
        //                        formItemTemp.ElementType = MayBeThisElementType(formItemTemp.ElementType);

        //                        var formItemModel = _mapper.Map<FormItemModel>(formItemTemp);

        //                        var formItemEntry = _mapper.Map<FormItem>(formItemModel);

        //                        var formItem = new FormItem
        //                        {
        //                            Id = formItemId,
        //                            FormId = id,
        //                            ElementType = ParseToElementType(formItemTemp.ElementType),
        //                            Name = formItemTemp.Name,
        //                            Description = formItemTemp.Description,
        //                            Label = formItemTemp.Label,
        //                            IsRequired = formItemTemp.IsRequired,
        //                            Placeholder = formItemTemp.Placeholder,
        //                            Ordinal = index + 1,
        //                        };

        //                        if (!string.IsNullOrWhiteSpace(formItemTemp.Options))
        //                        {
        //                            var optionsItemsSource = formItemTemp.Options.Split(';', StringSplitOptions.RemoveEmptyEntries);

        //                            if (optionsItemsSource.Length > 0)
        //                            {
        //                                var formItemOptions = optionsItemsSource.Select((x, index) => new FormItemOption
        //                                {
        //                                    Id = Guid.NewGuid(),
        //                                    FormItemId = formItemId,
        //                                    Value = x,
        //                                    Text = x,
        //                                    Ordinal = index + 1,
        //                                }).ToList();

        //                                foreach (var option in formItemOptions)
        //                                {
        //                                    formItem.Options.Add(option);
        //                                }
        //                            }
        //                        }

        //                        return formItem;
        //                    });

        //                    foreach (var item in formItems)
        //                    {
        //                        _dbContext.FormItems.Add(item);
        //                        //form.Items.Add(item);
        //                    }
        //                }
        //            }

        //            //_dbContext.Update(form);

        //            await _dbContext.SaveChangesAsync(cancellationToken);

        //            migrated++;
        //        }

        //        if (migrated > 0)
        //        {
        //            await transaction.CommitAsync(cancellationToken);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, ex.Message);

        //        await transaction.RollbackAsync(cancellationToken);

        //        throw;
        //    }
        //}

        //return Unit.Value;

        throw new NotImplementedException();
    }

    private ElementTypes ParseToElementType(string value)
    {
        var currentElementType = MayBeThisElementType(value);
        var elementType = ElementTypes.SingleLineText;

        if (!Enum.TryParse(value, out elementType))
        {
            elementType = ElementTypes.SingleLineText;
        }

        return elementType;
    }

    public string MayBeThisElementType(string value) => value switch
    {
        "single-text-input" => ElementTypes.SingleLineText.ToString(),
        "date" => ElementTypes.Date.ToString(),
        "datetime" => ElementTypes.DateTime.ToString(),
        "time" => ElementTypes.Time.ToString(),
        "email" => ElementTypes.Email.ToString(),
        "number-int" => ElementTypes.NumberInteger.ToString(),
        "number-float" => ElementTypes.NumberFloat.ToString(),
        "select" => ElementTypes.Select.ToString(),
        "multi-text-input" => ElementTypes.MultiLineText.ToString(),
        "checkbox" => ElementTypes.Checkbox.ToString(),
        "radio" => ElementTypes.Radio.ToString(),
        "file" => ElementTypes.File.ToString(),
        _ => value,
    };
}

class NumberToGuidConverter : JsonConverter<Guid>
{
    public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString() ?? string.Empty;

        Guid parsed;
        if (!Guid.TryParse(value, out parsed))
        {
            return Guid.NewGuid();
        }

        return parsed;
    }

    public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}

