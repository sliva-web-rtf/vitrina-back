using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain.Project;
using Vitrina.Domain.Project.Teammate;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations;

public class TeammateConfiguration : IEntityTypeConfiguration<Teammate>
{
    public void Configure(EntityTypeBuilder<Teammate> builder)
    {
        builder
            .HasOne(t => t.User)
            .WithMany(u => u.PositionsInTeams)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(t => t.Project)
            .WithMany(p => p.Users)
            .HasForeignKey(t => t.ProjectId);
    }
}
