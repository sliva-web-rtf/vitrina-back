using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Vitrina.Web.Infrastructure.Behaviors;

public class SensitiveDataContractResolver : DefaultContractResolver
{
    private static readonly DataType[] TypesToMask = new[]
    {
        DataType.Password,
        DataType.EmailAddress,
        DataType.PhoneNumber
    };

    protected override JsonProperty CreateProperty(
        MemberInfo member, MemberSerialization memberSerialization)
    {
        var property = base.CreateProperty(member, memberSerialization);

        var dataTypeAttrs = member.GetCustomAttributes<DataTypeAttribute>().ToList();

        var hasSensitiveType = dataTypeAttrs
            .Any(attr => TypesToMask.Contains(attr.DataType));

        if (hasSensitiveType)
        {
            var originalProvider = property.ValueProvider;
            property.ValueProvider = new MaskingValueProvider(originalProvider);
        }

        return property;
    }

    private class MaskingValueProvider(IValueProvider originalProvider) : IValueProvider
    {
        private const string Mask = "***";

        public object GetValue(object target)
        {
            var value = originalProvider.GetValue(target);
            if (value != null && !string.IsNullOrEmpty(value.ToString()))
            {
                return Mask;
            }

            return value;
        }

        public void SetValue(object target, object value)
        {
            originalProvider.SetValue(target, value);
        }
    }
}
