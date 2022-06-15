namespace FormBuilder.Entities;

public class FormItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid FormId { get; set; }

    public virtual Form? Form { get; set; }

    public ElementTypes ElementType { get; set; } = ElementTypes.SingleLineText;

    public string Name { get; set; } = string.Empty;

    public string Label { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Placeholder { get; set; } = string.Empty;

    public bool IsRequired { get; set; } = false;

    public int Ordinal { get; set; } = 1;

    public virtual IList<FormItemOption> Options { get; set; } = new List<FormItemOption>();

    public virtual IList<FormItemLocaled> Locales { get; set; } = new List<FormItemLocaled>();
}