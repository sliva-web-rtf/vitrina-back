using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations;

public class ProjectThematicsConfiguration : IEntityTypeConfiguration<ProjectThematics>
{
    public void Configure(EntityTypeBuilder<ProjectThematics> builder) =>
        builder
            .HasIndex(thematics => thematics.Name)
            .IsUnique();
}
