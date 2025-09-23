using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain.Project.Page;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations.ProjectConfiguration.PageConfiguration;

public class AdminViewConfiguration : IEntityTypeConfiguration<ModeratorView>
{
    public void Configure(EntityTypeBuilder<ModeratorView> builder)
    {
        builder
            .HasOne(view => view.Page)
            .WithMany()
            .HasForeignKey(view => view.PageId);
        builder
            .HasOne(view => view.Administrator)
            .WithMany()
            .HasForeignKey(view => view.AdministratorId);
    }
}
