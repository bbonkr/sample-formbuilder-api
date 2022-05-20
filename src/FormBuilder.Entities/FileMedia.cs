namespace FormBuilder.Entities;

public class FileMedia
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string ContainerName { get; set; }

    public string? Extension { get; set; }

    public long Size { get; set; } = 0;

    public string ContentType { get; set; } = "application/octet-stream";

    /// <summary>
    /// How to access from external space
    /// </summary>
    public string Uri { get; set; }

    /// <summary>
    /// How to access from internal space
    /// </summary>
    public string Path { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}