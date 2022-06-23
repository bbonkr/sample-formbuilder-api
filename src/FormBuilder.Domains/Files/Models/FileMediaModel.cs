namespace FormBuilder.Domains.Files.Models;

public abstract class FileMediaModel
{
    public string Name { get; set; } = string.Empty;

    public long Size { get; set; } = 0;

    public string ContentType { get; set; } = "application/octet-stream";

    public byte[]? Content { get; set; } = null;
}

public class DownloadFileMediaModel : FileMediaModel
{
    public Stream? Stream { get; set; }
}

public class UploadFileMediaModel : FileMediaModel
{
    public string Uri { get; set; } = string.Empty;

    public string UriForDeletion { get; set; } = string.Empty;
}