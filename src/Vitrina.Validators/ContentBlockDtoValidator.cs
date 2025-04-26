using FluentValidation;
using Newtonsoft.Json;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.Project.Page;
using Vitrina.UseCases.ProjectPage.Dto.Blocks;
using Vitrina.UseCases.ProjectPages;
using Vitrina.UseCases.ProjectPages.Blocks;

namespace Vitrina.Validators;

public class ContentBlockDtoValidator : AbstractValidator<ContentBlockDto>
{
    private static readonly Dictionary<ContentTypeEnum, Type> DictionaryContentTypeMatching = new()
    {
        { ContentTypeEnum.BlockWithTextAndImage, typeof(BlockWithTextAndImageDto) },
        { ContentTypeEnum.BlockWithTextsAndImages, typeof(BlockWithTextsAndImagesDto) },
        { ContentTypeEnum.ImageCarouselBlock, typeof(CarouselImagesDto) },
        { ContentTypeEnum.CodeBlock, typeof(CodeBlockDto) },
        { ContentTypeEnum.CommandBlock, typeof(CommandBlockDto) },
        { ContentTypeEnum.HorizontalDividerBlock, typeof(HorizontalDividerDto) },
        { ContentTypeEnum.ImageBlock, typeof(ImageBlockDto) },
        { ContentTypeEnum.TextBlock, typeof(TextBlockDto) },
        { ContentTypeEnum.VideoBlock, typeof(VideoBlockDto) },
    };

    public ContentBlockDtoValidator()
    {
        RuleFor(contentBlock => contentBlock.Content)
            .NotEmpty()
            .WithMessage("The value of the Content field should not be empty");
        RuleFor(contentBlock => contentBlock)
            .Must(TryDeserializeContentBlock)
            .WithMessage(content => $"The value of the {nameof(content.Content)} property is invalid:{Environment.NewLine}{content.Content}");
    }

    private bool TryDeserializeContentBlock(ContentBlockDto block)
    {
        if (DictionaryContentTypeMatching.TryGetValue(block.ContentType, out var type))
        {
            try
            {
                JsonConvert.DeserializeObject(block.Content, type);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        throw new DomainException($"For the type of content {block.ContentType} the check is not indicated.");
    }
}
