using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain.Project;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations;

/// <summary>
/// Role configuration.
/// </summary>
internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasIndex(r => r.Name).IsUnique();
        builder.HasMany(r => r.Users).WithMany(u => u.Roles);
    }
}
