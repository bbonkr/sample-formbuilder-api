using FormBuilder.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormBuilder.Data.EntityTypeConfigurations;

public class ResultItemValueEntityTypeConfiguration : IEntityTypeConfiguration<ResultItemValue>
{
    public void Configure(EntityTypeBuilder<ResultItemValue> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasConversion<string>()
            .ValueGeneratedOnAdd();
        builder.Property(x => x.ResultItemId)
            .IsRequired()
            .HasConversion<string>();
        builder.Property(x => x.Value)
            .IsRequired();

        builder.HasOne(x => x.ResultItem)
            .WithMany(x => x.Values)
            .HasForeignKey(x => x.ResultItemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}