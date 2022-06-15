namespace FormBuilder.Entities;

public class FormLocaled
{
    public Guid FormId { get; set; }

    public Guid LanguageId { get; set; }

    public string Title { get; set; }
    
    public virtual Form Form { get; set; }
    
    public  virtual  Language Language { get; set; }
}