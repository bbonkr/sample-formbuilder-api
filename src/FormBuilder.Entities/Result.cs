namespace FormBuilder.Entities;

public class Result
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid FormId { get; set; }

    public string Content { get; set; }

    public Form Form { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public virtual IList<ResultItem> Items { get; set; } = new List<ResultItem>();
}
