using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain.Project.Page;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations.PageConfiguration;

public class ContentBlockConfiguration : IEntityTypeConfiguration<ContentBlock>
{
    public void Configure(EntityTypeBuilder<ContentBlock> builder)
    {
        builder.ToTable("ContentBlocks");
        builder
            .HasOne(block => block.Page)
            .WithMany(page => page.ContentBlocks)
            .HasForeignKey(block => block.PageId);
    }
}
