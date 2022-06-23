namespace FormBuilder.Domains.Forms.Models;

public class FormLocaledModel
{
    public Guid? FormId { get; set; }

    public Guid? LanguageId { get; set; }

    public string LanguageCode { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
}