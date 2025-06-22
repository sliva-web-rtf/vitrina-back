using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Vitrina.UseCases.ProjectPage.Dto.BasicContentUnits;
using Vitrina.UseCases.ProjectPage.Dto.Blocks;

namespace Vitrina.Web.Infrastructure.Startup.Swagger;

public class AdditionalSchemasDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var additionalTypes = new[]
        {
            typeof(ImageUnitDto),
            typeof(UnitWithImageAndTextDto),
            typeof(BlockWithTextAndImageDto),
            typeof(BlockWithTextsAndImagesDto),
            typeof(CarouselImagesDto),
            typeof(CodeBlockDto),
            typeof(HorizontalDividerDto),
            typeof(ImageBlockDto),
            typeof(TextBlockDto),
            typeof(VideoBlockDto),
        };

        foreach (var type in additionalTypes)
        {
            context.SchemaGenerator.GenerateSchema(type, context.SchemaRepository);
        }
    }
}



