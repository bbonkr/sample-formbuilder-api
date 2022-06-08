namespace FormBuilder.Domains.Forms.Models;

public class FormModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public long ResultsCount { get; set; }

    public IList<FormItemModel> Items { get; set; } = new List<FormItemModel>();
}
