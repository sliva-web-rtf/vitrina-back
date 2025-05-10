using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain.Project;
using Vitrina.Domain.Project.Page;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations;

/// <summary>
///     Configuration of project.
/// </summary>
internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasIndex(project => project.Name);
        builder.HasIndex(project => project.Client);
        builder.HasIndex(project => project.Semester);
        builder.HasOne(project => project.Page).WithOne(c => c.Project);
        builder.HasMany(project => project.Tags).WithMany(t => t.Projects);
        builder
            .HasOne(project => project.Page)
            .WithOne(page => page.Project)
            .HasForeignKey<ProjectPage>(projectPage => projectPage.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
