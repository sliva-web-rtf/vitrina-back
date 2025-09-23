using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain.Project.Page;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations.ProjectConfiguration.PageConfiguration;

public class VerificationResultConfiguration : IEntityTypeConfiguration<VerificationResult>
{
    public void Configure(EntityTypeBuilder<VerificationResult> builder)
    {
        builder
            .HasOne(result => result.Page)
            .WithOne()
            .HasForeignKey<VerificationResult>(result => result.PageId);
        builder
            .HasIndex(result => result.PageId)
            .IsUnique();
    }
}
