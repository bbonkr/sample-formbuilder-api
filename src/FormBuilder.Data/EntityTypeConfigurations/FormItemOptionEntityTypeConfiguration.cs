using FormBuilder.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormBuilder.Data.EntityTypeConfigurations;

public class FormItemOptionEntityTypeConfiguration : IEntityTypeConfiguration<FormItemOption>
{
    public void Configure(EntityTypeBuilder<FormItemOption> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasConversion<string>();
        builder.Property(x => x.FormItemId)
            .IsRequired()
            .HasConversion<string>();
        builder.Property(x => x.Value)
            .IsRequired()
            .HasMaxLength(1000);
        builder.Property(x => x.Text)
            .IsRequired()
            .HasMaxLength(1000);
        builder.Property(x => x.Ordinal)
            .IsRequired()
            .HasDefaultValue(1);
    }
}