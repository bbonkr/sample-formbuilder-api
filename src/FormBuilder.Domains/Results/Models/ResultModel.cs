using FormBuilder.Domains.Forms.Models;

namespace FormBuilder.Domains.Results.Models;

public class ResultModel
{
    public Guid Id { get; set; }

    public Guid FormId { get; set; }

    [Obsolete]
    public string Content { get; set; }

    public FormModel Form { get; set; }

    public IEnumerable<ResultItemModel> Items { get; set; } = Enumerable.Empty<ResultItemModel>();
}

public class ResultItemModel
{
    public Guid Id { get; set; }

    public Guid FormItemId { get; set; }

    public FormItemModel FormItem { get; set; }

    public IEnumerable<ResultItemValueModel> Values { get; set; } = Enumerable.Empty<ResultItemValueModel>();
}

public class ResultItemValueModel
{
    public Guid Id { get; set; }

    public string Value { get; set; }
}