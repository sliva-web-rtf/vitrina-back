using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain.Project;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations;

public class TeammateConfiguration : IEntityTypeConfiguration<Teammate>
{
    public void Configure(EntityTypeBuilder<Teammate> builder)
    {
        builder
            .HasOne(teammate => teammate.Student)
            .WithMany(user => user.PositionsInTeams)
            .HasForeignKey(t => t.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
