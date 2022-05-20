namespace FormBuilder.Services.FileServices.Models;

public class FileModel
{
    public string Name { get; set; }

    public string Extension { get; set; }

    public long Size { get; set; }

    public string ContentType { get; set; }

    public string ContainerName { get; set; }

    public string Uri { get; set; }

    public byte[]? FileContent { get; set; } = null;

    public Stream Stream { get; set; } = null;
}