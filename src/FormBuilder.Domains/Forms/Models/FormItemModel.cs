using FormBuilder.Entities;

namespace FormBuilder.Domains.Forms.Models;

public class FormItemModel
{
    public Guid? Id { get; set; }

    public Guid? FormId { get; set; }

    public ElementTypes ElementType { get; set; } = ElementTypes.SingleLineText;

    public string Name { get; set; } = string.Empty;

    public string Label { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Placeholder { get; set; } = string.Empty;

    public bool IsRequired { get; set; } = false;

    public int Ordinal { get; set; } = 1;

    public IEnumerable<FormItemOptionModel> Options { get; set; } = new List<FormItemOptionModel>();

    public IEnumerable<FormItemLocaledModel> Locales { get; set; } = new List<FormItemLocaledModel>();
}

public class FormItemLocaledModel
{
    public Guid? FormId { get; set; }
    
    public Guid? LanguageId { get; set; }
    
    public string? LanguageCode { get; set; }
    
    public string Label { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Placeholder { get; set; } = string.Empty;
}

public class FormTempModel
{
    public string Id { get; set; }

    public IEnumerable<FormItemTempModel>? Items { get; set; }
}

public class FormItemTempModel
{
    public string Id { get; set; }

    public Guid? FormId { get; set; }

    public string ElementType { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Label { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Placeholder { get; set; } = string.Empty;

    public bool IsRequired { get; set; } = false;

    public int Ordinal { get; set; } = 1;

    [Obsolete("Replace to OptionItems field")]
    public string Options { get; set; } = string.Empty;

    public virtual IList<FormItemOptionModel>? OptionItems { get; set; } = new List<FormItemOptionModel>();
}