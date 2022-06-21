using FormBuilder.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormBuilder.Data.EntityTypeConfigurations;

public class ResultItemEntityTypeConfiguration : IEntityTypeConfiguration<ResultItem>
{
    public void Configure(EntityTypeBuilder<ResultItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasConversion<string>();
        builder.Property(x => x.ResultId)
            .IsRequired()
            .HasConversion<string>();
        builder.Property(x => x.FormItemId)
            .IsRequired()
            .HasConversion<string>();

        builder.HasOne(x => x.FormItem)
            .WithMany()
            .HasForeignKey(x => x.FormItemId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.Values)
            .WithOne(x => x.ResultItem)
            .HasForeignKey(x => x.ResultItemId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
