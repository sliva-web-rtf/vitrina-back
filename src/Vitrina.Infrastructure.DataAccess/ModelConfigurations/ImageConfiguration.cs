using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain.Project;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations;

/// <summary>
/// Configuration of Image entity.
/// </summary>
internal class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
            .IsRequired();
    }
}
