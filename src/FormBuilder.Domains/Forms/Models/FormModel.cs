namespace FormBuilder.Domains.Forms.Models;

public class FormModel
{
    public Guid? Id { get; set; }

    public string Title { get; set; }

    // [Obsolete]
    // public string Content { get; set; }

    public long ResultsCount { get; set; } = 0;

    public IEnumerable<FormItemModel> Items { get; set; } = new List<FormItemModel>();

    public IEnumerable<FormLocaledModel> Locales { get; set; } = new List<FormLocaledModel>();
}

public class FormLocaledModel
{
    public Guid? FormId { get; set; }

    public Guid? LanguageId { get; set; }

    public string LanguageCode { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
}