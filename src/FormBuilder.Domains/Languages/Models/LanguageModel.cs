namespace FormBuilder.Domains.Languages.Models;

public class LanguageModel
{
    public Guid Id { get; set; }
    
    public string Code { get; set; }
    
    public  string Name { get; set; }
    
    public  int Ordinal { get; set; }
}