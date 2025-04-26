using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vitrina.Domain.Project.Page;
using Vitrina.Domain.Project.Page.Blocks;

namespace Vitrina.Infrastructure.DataAccess.ModelConfigurations;

public class ProjectPageConfiguration : IEntityTypeConfiguration<ProjectPage>
{
    public void Configure(EntityTypeBuilder<ProjectPage> builder)
    {
        builder.ToTable("ProjectPages");
        ConfigureСascadingDeletionChildElements(builder, page => page.BlocksWithTextAndImages);
        ConfigureСascadingDeletionChildElements(builder, page => page.BlocksWithTextsAndImages);
        ConfigureСascadingDeletionChildElements(builder, page => page.CodeBlocks);
        ConfigureСascadingDeletionChildElements(builder, page => page.CommandBlocks);
        ConfigureСascadingDeletionChildElements(builder, page => page.HorizontalDividerBlocks);
        ConfigureСascadingDeletionChildElements(builder, page => page.ImageBlocks);
        ConfigureСascadingDeletionChildElements(builder, page => page.ImageCarouselBlocks);
        ConfigureСascadingDeletionChildElements(builder, page => page.TextBlocks);
        ConfigureСascadingDeletionChildElements(builder, page => page.VideoBlocks);
        builder
            .HasMany(page => page.Editors)
            .WithOne(editor => editor.Page)
            .HasForeignKey(editor => editor.PageId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigureСascadingDeletionChildElements<TChildElement>(
        EntityTypeBuilder<ProjectPage> builder,
        Expression<Func<ProjectPage, IEnumerable<TChildElement>?>>? navigationExpression = null)
        where TChildElement : NumberedBlockBase
    {
        builder
            .HasMany(navigationExpression)
            .WithOne(block => block.ProjectPage)
            .HasForeignKey(block => block.ProjectPageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
