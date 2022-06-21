namespace FormBuilder.Entities;

public class ResultItemValue
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ResultItemId { get; set; }

    public string Value { get; set; }

    public virtual ResultItem ResultItem { get; set; }
}