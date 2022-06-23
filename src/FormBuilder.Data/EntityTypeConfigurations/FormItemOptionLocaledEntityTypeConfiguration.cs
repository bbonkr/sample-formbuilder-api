using FormBuilder.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormBuilder.Data.EntityTypeConfigurations;

public class FormItemOptionLocaledEntityTypeConfiguration : IEntityTypeConfiguration<FormItemOptionLocaled>
{
    public void Configure(EntityTypeBuilder<FormItemOptionLocaled> builder)
    {
        builder.HasKey(x => new { x.FormItemOptionId, x.LanguageId });

        builder.Property(x => x.FormItemOptionId)
            .IsRequired()
            .HasConversion<string>();
        builder.Property(x => x.LanguageId)
            .IsRequired()
            .HasConversion<string>();
        builder.Property(x => x.Text)
            .IsRequired()
            .HasMaxLength(1000);

        builder.HasOne(x => x.FormItemOption)
            .WithMany(x => x.Locales)
            .HasForeignKey(x => x.FormItemOptionId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Language)
            .WithMany()
            .HasForeignKey(x => x.LanguageId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Navigation(x => x.Language)
            .AutoInclude();
    }
}