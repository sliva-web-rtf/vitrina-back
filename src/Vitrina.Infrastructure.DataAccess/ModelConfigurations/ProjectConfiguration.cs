using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saritasa.RedMan.Domain.Project;

namespace Saritasa.RedMan.Infrastructure.DataAccess.ModelConfigurations;

/// <summary>
/// Configeration of project.
/// </summary>
internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasIndex(p => p.Name);
        builder.HasMany(p => p.Contents).WithOne(c => c.Project);
        builder.HasMany(p => p.Tags).WithMany(t => t.Projects);
        builder.HasMany(p => p.Users).WithOne(u => u.Project);
    }
}
