using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saritasa.RedMan.Domain.Store;

namespace Saritasa.RedMan.Infrastructure.DataAccess.ModelConfigurations;

/// <summary>
/// Contains database model configuration for <see cref="Product"/>.
/// </summary>
internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasIndex(p => p.Name);
        builder.Property(e => e.CreatedAt)
            .HasColumnType("timestamp")
            .HasDefaultValueSql("now() at time zone 'UTC'");
        builder.Property(e => e.UpdatedAt)
            .HasColumnType("timestamp")
            .HasDefaultValueSql("now() at time zone 'UTC'");
    }
}
