using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain.User;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations;

public abstract class UserWithRoleConfigurationBase<TUser> : IEntityTypeConfiguration<TUser>
    where TUser : UserWithRoleBase
{
    public virtual void Configure(EntityTypeBuilder<TUser> builder)
    {
        builder.ToTable(nameof(TUser));
        builder
            .HasOne(userWithRole => userWithRole.User)
            .WithOne()
            .HasForeignKey<Student>(student => student.UserId);
    }
}
