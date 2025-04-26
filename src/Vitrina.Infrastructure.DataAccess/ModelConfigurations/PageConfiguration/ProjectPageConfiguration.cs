using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain.Project.Page;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations;

public class ProjectPageConfiguration : IEntityTypeConfiguration<ProjectPage>
{
    public void Configure(EntityTypeBuilder<ProjectPage> builder)
    {
        builder.ToTable("ProjectPages");
        builder
            .HasMany(page => page.ContentBlocks)
            .WithOne(block => block.Page)
            .HasForeignKey(block => block.PageId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasMany(page => page.Editors)
            .WithOne(editor => editor.Page)
            .HasForeignKey(editor => editor.PageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
