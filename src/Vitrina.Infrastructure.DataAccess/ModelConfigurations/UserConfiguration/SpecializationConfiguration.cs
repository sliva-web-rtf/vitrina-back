using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain.User;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations;

public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
{
    public void Configure(EntityTypeBuilder<Specialization> builder) =>
        builder
            .HasIndex(s => s.Name)
            .IsUnique();
}
