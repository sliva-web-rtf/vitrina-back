using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain.Project;
using Vitrina.Domain.Project.Teammate;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations;

/// <summary>
/// Role configuration.
/// </summary>
internal class RoleConfiguration : IEntityTypeConfiguration<ProjectRole>
{
    public void Configure(EntityTypeBuilder<ProjectRole> builder)
    {
        builder.HasIndex(r => r.Name).IsUnique();
        builder.HasMany(r => r.Users).WithMany(u => u.Roles);
    }
}
