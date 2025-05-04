using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain.Project;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations;

/// <summary>
///     Configuration of project.
/// </summary>
internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasIndex(p => p.Name);
        builder.HasIndex(p => p.Client);
        builder.HasIndex(p => p.Semester);
        builder.HasIndex(p => p.Period);
        builder.HasMany(p => p.Contents).WithOne(c => c.Project);
        builder.HasMany(p => p.Tags).WithMany(t => t.Projects);
    }
}
