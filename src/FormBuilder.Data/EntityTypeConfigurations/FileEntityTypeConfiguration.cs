using Microsoft.EntityFrameworkCore;
using FormBuilder.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FormBuilder.Data.EntityTypeConfigurations;

public class FileEntityTypeConfiguration : IEntityTypeConfiguration<FileMedia>
{
    public void Configure(EntityTypeBuilder<FileMedia> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired()
            .HasConversion<string>()
            .ValueGeneratedOnAdd();
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(1000);
        builder.Property(x => x.ContainerName)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.Extension)
            .IsRequired(false)
            .HasMaxLength(100);
        builder.Property(x => x.Size)
            .IsRequired()
            .HasDefaultValue(0);
        builder.Property(x => x.ContentType)
            .IsRequired()
            .HasDefaultValue("application/octet-stream")
            .HasMaxLength(100);
        builder.Property(x => x.Uri)
            .IsRequired()
            .HasMaxLength(1000);
        builder.Property(x => x.Path)
            .IsRequired(true)
            .HasMaxLength(1000);
        builder.Property(x => x.CreatedAt)
            .IsRequired();
    }
}