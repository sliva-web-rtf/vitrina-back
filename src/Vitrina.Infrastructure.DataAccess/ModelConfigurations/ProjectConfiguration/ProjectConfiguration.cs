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
        builder.HasIndex(project => project.PageId);
        builder
            .HasOne(project => project.Team)
            .WithOne(team => team.Project)
            .HasForeignKey<Project>(project => project.TeamId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(project => project.Page)
            .WithOne(page => page.Project)
            .HasForeignKey<ProjectPage>(projectPage => projectPage.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
