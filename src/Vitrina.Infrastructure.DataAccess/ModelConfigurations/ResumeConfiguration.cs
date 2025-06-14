using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain.Project;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations;

/// <summary>
/// Configuration of Resume entity.
/// </summary>
internal class ResumeConfiguration : IEntityTypeConfiguration<Resume>
{
    public void Configure(EntityTypeBuilder<Resume> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasOne(resume => resume.User)
            .WithOne()
            .HasForeignKey<Resume>(resume => resume.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(r => r.FileName)
            .IsRequired();

        builder.HasIndex(r => r.UserId)
            .IsUnique();
    }
}
