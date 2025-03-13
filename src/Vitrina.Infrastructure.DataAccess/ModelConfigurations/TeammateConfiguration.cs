using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain.Project;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations;

public class TeammateConfiguration : IEntityTypeConfiguration<Teammate>
{
    public void Configure(EntityTypeBuilder<Teammate> builder)
    {
        builder
            .HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(t => t.Project)
            .WithMany(p => p.Users)
            .HasForeignKey(t => t.ProjectId);
    }
}
