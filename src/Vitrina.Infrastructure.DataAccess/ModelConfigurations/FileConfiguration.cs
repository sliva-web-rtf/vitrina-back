using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = Vitrina.Domain.File;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations;

public class FileConfiguration : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<File> builder) =>
        builder.HasOne(file => file.Creator)
            .WithMany()
            .HasForeignKey(file => file.CreatorId)
            .OnDelete(DeleteBehavior.NoAction);
}
