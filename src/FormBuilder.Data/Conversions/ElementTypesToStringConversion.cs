using System;
using FormBuilder.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FormBuilder.Data.Conversions
{
    public class ElementTypesToStringConversion : ValueConverter<ElementTypes, string>
    {
        public ElementTypesToStringConversion()
            : base(
                 x => x.ToString(),
                 x => Enum.Parse<ElementTypes>(x)
                 )
        {
        }
    }
}

