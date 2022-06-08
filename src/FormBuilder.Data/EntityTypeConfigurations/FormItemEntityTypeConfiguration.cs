using FormBuilder.Data.Conversions;
using FormBuilder.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormBuilder.Data.EntityTypeConfigurations;

public class FormItemEntityTypeConfiguration : IEntityTypeConfiguration<FormItem>
{
    public void Configure(EntityTypeBuilder<FormItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasConversion<string>()
            .ValueGeneratedOnAdd();
        builder.Property(x => x.FormId)
            .IsRequired()
            .HasConversion<string>();
        builder.Property(x => x.ElementType)
            .IsRequired()
            .HasConversion<ElementTypesToStringConversion>();
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.Label)
            .IsRequired()
            .HasMaxLength(1000);
        builder.Property(x => x.Description)
            .IsRequired(false);
        builder.Property(x => x.Placeholder)
            .IsRequired(false);
        builder.Property(x => x.IsRequired)
            .IsRequired()
            .HasDefaultValue(false);
        builder.Property(x => x.Ordinal)
            .IsRequired()
            .HasDefaultValue(1);

        builder.HasMany(x => x.Options)
            .WithOne(x => x.FormItem)
            .HasForeignKey(x => x.FormItemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
