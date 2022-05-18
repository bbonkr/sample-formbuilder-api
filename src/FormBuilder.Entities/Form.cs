namespace FormBuilder.Entities;

public class Form
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public virtual IList<Result> Results { get; set; }
}