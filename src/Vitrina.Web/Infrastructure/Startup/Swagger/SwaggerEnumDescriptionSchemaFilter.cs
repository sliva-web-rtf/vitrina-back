﻿using System.ComponentModel;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

// module: swagger

namespace Vitrina.Web.Infrastructure.Startup.Swagger;

/// <summary>
///     A filter that adds descriptions of enums depending on their name.
///     It also adds additional descriptions if the enum elements have the attribute
///     <see cref="DescriptionAttribute" />.
/// </summary>
internal class EnumDescriptionSchemaFilter : ISchemaFilter
{
    private const string LineBreakSeparator = "<br />";

    /// <inheritdoc />
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var type = context.Type;
        if (!type.IsEnum)
        {
            return;
        }

        if (!string.IsNullOrWhiteSpace(schema.Description))
        {
            schema.Description += LineBreakSeparator;
        }

        var enumDescriptions = new List<string>();
        var enumValues = Enum.GetValues(type);

        foreach (var enumValue in enumValues)
        {
            var enumValueName = Enum.GetName(type, enumValue)!;
            var enumDescriptionFromAttribute = GetEnumDescriptionFromAttribute(type, enumValueName);
            var enumUnderlyingTypeValue = Convert.ChangeType(enumValue, type.GetEnumUnderlyingType());

            var enumDescription = string.IsNullOrWhiteSpace(enumDescriptionFromAttribute)
                ? $"{enumUnderlyingTypeValue} = {enumValueName}"
                : $"{enumUnderlyingTypeValue} = {enumValueName} ({enumDescriptionFromAttribute})";

            enumDescriptions.Add(enumDescription);
        }

        schema.Description += string.Join(LineBreakSeparator, enumDescriptions);
    }

    private static string GetEnumDescriptionFromAttribute(Type type, string enumValueName)
    {
        var typeMemberInfo = type.GetMember(enumValueName);
        var typeAttributes = typeMemberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        if (typeAttributes.Length > 0)
        {
            return (typeAttributes[0] as DescriptionAttribute)?.Description ?? string.Empty;
        }

        return string.Empty;
    }
}
