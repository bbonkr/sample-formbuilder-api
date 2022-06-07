﻿namespace FormBuilder.Entities;

public class Form
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    [Obsolete("Replace to Items field")]
    public string Content { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public virtual IList<FormItem> Items { get; set; } = new List<FormItem>();

    public virtual IList<Result> Results { get; set; } = new List<Result>();
}

