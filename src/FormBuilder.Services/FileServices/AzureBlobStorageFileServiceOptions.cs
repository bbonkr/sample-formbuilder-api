namespace FormBuilder.Services.FileServices;

public class AzureBlobStorageFileServiceOptions
{
    public const string Name = "AzureBlobStorage";
    
    public string ConnectionString { get; set; }
}