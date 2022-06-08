namespace FormBuilder.Domains.Forms.Models;

public class FormItemOptionModel
{
    public Guid Id { get; set; }

    public Guid FormItemId { get; set; }

    public string Value { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;

    public int Ordinal { get; set; } = 1;
}