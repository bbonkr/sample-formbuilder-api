namespace FormBuilder.Services.FileServices.Models;

public class FileModel
{
    public string Name { get; set; }

    public string Extension { get; set; }

    public long Size { get; set; }

    public string ContentType { get; set; }

    public string ContainerName { get; set; }

    public string Uri { get; set; }

    public IEnumerable<byte> FileContent { get; set; } = null;
}