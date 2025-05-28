using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain.Project.Teammate;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations.ProjectConfiguration;

/// <summary>
///     Role configuration.
/// </summary>
internal class RoleConfiguration : IEntityTypeConfiguration<ProjectRole>
{
    public void Configure(EntityTypeBuilder<ProjectRole> builder) => builder.HasIndex(role => role.Name).IsUnique();
}
