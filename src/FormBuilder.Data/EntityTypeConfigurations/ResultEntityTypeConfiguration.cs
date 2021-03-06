using FormBuilder.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormBuilder.Data.EntityTypeConfigurations;

public class ResultEntityTypeConfiguration : IEntityTypeConfiguration<Result>
{
    public void Configure(EntityTypeBuilder<Result> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired()
            .HasConversion<string>()
            .ValueGeneratedOnAdd();
        builder.Property(x => x.FormId)
            .IsRequired()
            .HasConversion<string>();
        //builder.Property(x => x.Content)
        //    .IsRequired(true);
        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(x => x.Form)
            .WithMany(x => x.Results)
            .HasForeignKey(x => x.FormId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
