using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain.Project.Page.BasicContentUnits;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations.PageConfiguration.BasicContentUnitsConfiguration;

public class ImageUnitConfiguration : IEntityTypeConfiguration<UnitWithImageBase>
{
    public virtual void Configure(EntityTypeBuilder<UnitWithImageBase> builder)
    {
        builder
            .HasOne(unit => unit.Image)
            .WithMany()
            .HasForeignKey(file => file.ImageFileId)
            .OnDelete(DeleteBehavior.Restrict);

        throw new NotImplementedException();
    }
}
