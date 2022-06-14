namespace FormBuilder.Entities;

public class FormItemOption
{
    public Guid Id { get; set; }

    public Guid FormItemId { get; set; }

    public virtual FormItem? FormItem { get; set; }

    public string Value { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;

    public int Ordinal { get; set; } = 1;

    public IList<FormItemOptionLocaled> Locales { get; set; } = new List<FormItemOptionLocaled>();
}