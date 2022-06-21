namespace FormBuilder.Entities;

public class ResultItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ResultId { get; set; }

    public Guid FormItemId { get; set; }

    public virtual Result Result { get; set; }

    public virtual FormItem FormItem { get; set; }

    public virtual IList<ResultItemValue> Values { get; set; }
}
