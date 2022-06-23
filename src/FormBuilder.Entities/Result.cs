namespace FormBuilder.Entities;

public class Result
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid FormId { get; set; }

    //[Obsolete]
    //public string Content { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public virtual Form Form { get; set; }

    public virtual IList<ResultItem> Items { get; set; } = new List<ResultItem>();
}
