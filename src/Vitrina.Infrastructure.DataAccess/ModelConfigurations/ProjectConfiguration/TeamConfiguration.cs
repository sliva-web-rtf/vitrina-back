using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain.Project;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations.ProjectConfiguration;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder
            .HasOne(team => team.Project)
            .WithOne(team => team.Team)
            .HasForeignKey<Project>(team => team.TeamId)
            .OnDelete(DeleteBehavior.NoAction);
        builder
            .HasMany(team => team.TeamMembers)
            .WithOne(teammate => teammate.Team)
            .HasForeignKey(teammate => teammate.TeamId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
