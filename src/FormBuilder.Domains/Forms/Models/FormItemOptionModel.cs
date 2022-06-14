﻿using FormBuilder.Entities;

namespace FormBuilder.Domains.Forms.Models;

public class FormItemOptionModel
{
    public Guid? Id { get; set; }

    public Guid? FormItemId { get; set; }

    public string Value { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;

    public int Ordinal { get; set; } = 1;

    public IEnumerable<FormItemOptionLocaled> Locales { get; set; } = new List<FormItemOptionLocaled>();
}

public class FormItemOptionLocaledModel
{
    public Guid? FormItemOptionId { get; set; }

    public Guid? LanguageId { get; set; }

    public string LanguageCode { get; set; }

    public string Text { get; set; }
}