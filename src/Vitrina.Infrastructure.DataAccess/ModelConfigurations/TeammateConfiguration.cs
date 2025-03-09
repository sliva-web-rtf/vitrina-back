using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain.Project;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations;

public class TeammateConfiguration : IEntityTypeConfiguration<Teammate>
{
    public void Configure(EntityTypeBuilder<Teammate> builder)
    {
        builder
            .HasOne(t => t.User) // Один пользователь
            .WithMany() // может быть связан с несколькими Teammates
            .HasForeignKey(t => t.UserId) // Внешний ключ
            .OnDelete(DeleteBehavior.Cascade); // Каскадное удаление
    }
}
