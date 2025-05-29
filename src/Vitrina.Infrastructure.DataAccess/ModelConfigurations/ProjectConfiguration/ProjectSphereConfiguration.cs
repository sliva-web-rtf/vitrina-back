using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain.Project;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations.ProjectConfiguration;

public class ProjectSphereConfiguration : IEntityTypeConfiguration<ProjectSphere>
{
    public void Configure(EntityTypeBuilder<ProjectSphere> builder) =>
        builder.HasIndex(sphere => sphere.Name).IsUnique();
}
