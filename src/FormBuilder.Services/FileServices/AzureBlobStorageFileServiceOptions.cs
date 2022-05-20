namespace FormBuilder.Services.FileServices;

public class AzureBlobStorageFileServiceOptions
{
    public const string Name = "AzureBlobStorage";
    
    public string ConnectionString { get; set; }
    
    public string AccountName { get; set; }
    
    public string AccountKey { get; set; }
}