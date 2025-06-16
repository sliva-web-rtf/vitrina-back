using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations;

/// <summary>
///     Configuration of Image entity.
/// </summary>
internal class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.HasKey(image => image.Id);

        builder.Property(image => image.Id)
            .IsRequired();

        builder.HasOne(image => image.File)
            .WithOne()
            .HasForeignKey<Image>(resume => resume.FileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
