using FormBuilder.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormBuilder.Data.EntityTypeConfigurations;

public class FormItemLocaledEntityTypeConfiguration : IEntityTypeConfiguration<FormItemLocaled>
{
    public void Configure(EntityTypeBuilder<FormItemLocaled> builder)
    {
        builder.HasKey(x => new {x.FormItemId, x.LanguageId});

        builder.Property(x => x.FormItemId)
            .IsRequired()
            .HasConversion<string>();
        builder.Property(x => x.LanguageId)
            .IsRequired()
            .HasConversion<string>();
        builder.Property(x => x.Label)
            .IsRequired()
            .HasMaxLength(1000);
        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasMaxLength(1000);
        builder.Property(x => x.Placeholder)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.HasOne(x => x.FormItem)
            .WithMany(x => x.Locales)
            .HasForeignKey(x => x.FormItemId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Language)
            .WithMany()
            .HasForeignKey(x => x.LanguageId);
        
        builder.Navigation(x => x.Language)
            .AutoInclude();
    }
}