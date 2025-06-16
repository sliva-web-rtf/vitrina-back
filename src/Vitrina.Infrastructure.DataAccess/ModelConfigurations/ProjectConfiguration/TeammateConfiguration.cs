using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain.Project.Teammate;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations.ProjectConfiguration;

public class TeammateConfiguration : IEntityTypeConfiguration<Teammate>
{
    public void Configure(EntityTypeBuilder<Teammate> builder) =>
        builder.HasOne(teammate => teammate.User)
            .WithMany()
            .HasForeignKey(teammate => teammate.UserId)
            .OnDelete(DeleteBehavior.Cascade);
}
