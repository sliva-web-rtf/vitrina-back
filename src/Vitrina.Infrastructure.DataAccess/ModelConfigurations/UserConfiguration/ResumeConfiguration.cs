using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain.User;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations.UserConfiguration;

/// <summary>
///     Configuration of Resume entity.
/// </summary>
internal class ResumeConfiguration : IEntityTypeConfiguration<Resume>
{
    public void Configure(EntityTypeBuilder<Resume> builder)
    {
        builder.HasKey(resume => resume.Id);

        builder.HasOne(resume => resume.User)
            .WithOne()
            .HasForeignKey<Resume>(resume => resume.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(resume => resume.File)
            .WithOne()
            .HasForeignKey<Resume>(resume => resume.FileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(resume => resume.UserId)
            .IsUnique();
    }
}
