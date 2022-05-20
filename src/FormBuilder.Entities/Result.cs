namespace FormBuilder.Entities;

public class Result
{
    public Guid Id { get; set; }

    public Guid FormId { get; set; }

    public string Content { get; set; }

    public Form Form { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}