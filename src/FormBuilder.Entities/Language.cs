namespace FormBuilder.Entities;

public class Language
{
    public Guid Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public int Ordinal { get; set; } = 1;
}