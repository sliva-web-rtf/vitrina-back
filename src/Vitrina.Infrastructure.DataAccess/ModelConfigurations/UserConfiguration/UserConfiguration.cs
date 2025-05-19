using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain.User;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
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
            .HasMany(user => user.EditingRights)
            .WithOne(editor => editor.User)
            .HasForeignKey(editor => editor.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasIndex(user => user.Email)
            .IsUnique();
    }
}
