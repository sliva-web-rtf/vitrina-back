using System.Reflection;
using AutoMapper;

namespace Vitrina.UseCases.Common;

public static class AutoMapperExtensions
{
    public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(
        this IMappingExpression<TSource, TDestination> expression)
    {
        var sourceProps = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(p => p.Name)
            .ToHashSet();

        var destinationProps = typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var prop in destinationProps)
        {
            if (!sourceProps.Contains(prop.Name))
            {
                expression.ForMember(prop.Name, opt => opt.Ignore());
            }
        }

        return expression;
    }
}
