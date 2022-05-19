namespace FormBuilderApp.Infrastructure.Options;

public class CorsOptions
{
    public const string Name = "Cors";

    public string Origins { get; set; } = string.Empty;

    public IEnumerable<string>? GetAllowOrigins()
    {
        if (string.IsNullOrWhiteSpace(Origins))
        {
            return null;
        }
        
        return Origins.Split(';', StringSplitOptions.RemoveEmptyEntries);
    }
}