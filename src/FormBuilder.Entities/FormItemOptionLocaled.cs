namespace FormBuilder.Entities;

public class FormItemOptionLocaled
{
    public Guid FormItemOptionId { get; set; }
    
    public Guid LanguageId { get; set; }
    
    public string Text { get; set; } = string.Empty;
    
    public virtual FormItemOption FormItemOption { get; set; }
    
    public virtual Language Language { get; set; }
}