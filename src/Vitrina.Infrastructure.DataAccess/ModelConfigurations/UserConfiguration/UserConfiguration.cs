using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain.User;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations.UserConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(user => user.EmailConfirmed)
            .HasDefaultValue("false");
        builder
            .Property(user => user.PhoneNumberConfirmed)
            .HasDefaultValue("false");
        builder
            .Property(user => user.TwoFactorEnabled)
            .HasDefaultValue("false");
        builder
            .Property(user => user.LockoutEnabled)
            .HasDefaultValue("false");
        builder
            .Property(user => user.AccessFailedCount)
            .HasDefaultValue("5");
        builder
            .Property(user => user.RegistrationStatus)
            .HasDefaultValue(RegistrationStatusEnum.NotRegistered);
        builder
            .HasMany(user => user.EditingRights)
            .WithOne(editor => editor.User)
            .HasForeignKey(editor => editor.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasIndex(user => user.NormalizedEmail)
            .IsUnique();
    }
}
