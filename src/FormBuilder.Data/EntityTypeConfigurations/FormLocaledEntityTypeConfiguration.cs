using FormBuilder.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormBuilder.Data.EntityTypeConfigurations;

public class FormLocaledEntityTypeConfiguration : IEntityTypeConfiguration<FormLocaled>
{
    public void Configure(EntityTypeBuilder<FormLocaled> builder)
    {
        builder.HasKey(x => new {x.FormId, x.LanguageId});

        builder.Property(x => x.FormId)
            .IsRequired()
            .HasConversion<string>();
        builder.Property(x => x.LanguageId)
            .IsRequired()
            .HasConversion<string>();
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(1000);

        builder.HasOne(x => x.Form)
            .WithMany(x => x.Locales)
            .HasForeignKey(x => x.FormId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Language)
            .WithMany()
            .HasForeignKey(x => x.LanguageId);
    }
}