using FluentValidation;
using Newtonsoft.Json;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.Project.Page.Content;
using Vitrina.UseCases.ProjectPage.Dto;
using Vitrina.UseCases.ProjectPage.Dto.Blocks;

namespace Vitrina.UseCases.ProjectPage;

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
        { ContentTypeEnum.VideoBlock, typeof(VideoBlockDto) }
    };

    private static readonly JsonSerializerSettings SerializerSettings = new()
    {
        MissingMemberHandling = MissingMemberHandling.Error,
        Error = (_, args) => { args.ErrorContext.Handled = false; }
    };

    public ContentBlockDtoValidator()
    {
        RuleFor(contentBlock => contentBlock.Content)
            .NotNull()
            .WithMessage("The value of the Content field should not be empty");
        RuleFor(contentBlock => contentBlock)
            .Must(TryDeserializeContentBlock)
            .WithMessage(content =>
                $"The value of the {nameof(content.Content)} property is invalid:{Environment.NewLine}{content.Content}");
    }

    private bool TryDeserializeContentBlock(ContentBlockDto block)
    {
        if (DictionaryContentTypeMatching.TryGetValue(block.ContentType, out var type))
        {
            try
            {
                block.Content.ToObject(type, JsonSerializer.Create(SerializerSettings));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        throw new DomainException($"For the type of content {block.ContentType} the check is not indicated.");
    }
}
