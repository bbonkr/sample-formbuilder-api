namespace FormBuilder.Entities;

public class FormItemLocaled
{
    public  Guid FormItemId { get; set; }
    
    public Guid LanguageId { get; set; }
    
    public string Label { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Placeholder { get; set; } = string.Empty;
    
    public  virtual  FormItem FormItem { get; set; }
    
    public virtual  Language Language { get; set; }
}