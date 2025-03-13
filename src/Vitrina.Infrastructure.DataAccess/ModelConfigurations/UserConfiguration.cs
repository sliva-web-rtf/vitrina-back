using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain.User;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(user => user.EducationCourse)
            .IsRequired(false);
        builder
            .Property(u => u.EmailConfirmed)
            .HasDefaultValue("false");
        builder
            .Property(u => u.PhoneNumberConfirmed)
            .HasDefaultValue("false");
        builder
            .Property(u => u.TwoFactorEnabled)
            .HasDefaultValue("false");
        builder
            .Property(u => u.LockoutEnabled)
            .HasDefaultValue("false");
        builder
            .Property(u => u.AccessFailedCount)
            .HasDefaultValue("5");
        builder
            .Property(user => user.RegistrationStatus)
            .HasDefaultValue(RegistrationStatusEnum.NotRegistered);
        builder
            .Property(u => u.RolesInTeam)
            .HasColumnType("text[]");
    }
}
