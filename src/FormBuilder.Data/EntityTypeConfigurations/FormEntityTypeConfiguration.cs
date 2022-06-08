using FormBuilder.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormBuilder.Data.EntityTypeConfigurations;

public class FormEntityTypeConfiguration : IEntityTypeConfiguration<Form>
{
    public void Configure(EntityTypeBuilder<Form> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasConversion<string>()
            .ValueGeneratedOnAdd();
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(1000);
        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.Results)
            .WithOne(x => x.Form)
            .HasForeignKey(x => x.FormId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Items)
            .WithOne(x => x.Form)
            .HasForeignKey(x => x.FormId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
