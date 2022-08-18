using System.Diagnostics.CodeAnalysis;

namespace Schotejil.Clubkampioen.Data.Lenex.Xml;

public static class XmlEnumExtensions
{
    public static string ToXmlString<T>(this T value)
        where T : notnull, Enum
    {
        XmlEnumAttribute? attribute = value.GetAttributeOfType<XmlEnumAttribute>();
        if (attribute is { Name: { } })
        {
            return attribute.Name;
        }

        return value.ToString();
    }

    [return: NotNullIfNotNull("value")]
    public static T? FromXmlString<T>(string? value)
        where T : notnull, Enum
    {
        if (value is null)
        {
            return default;
        }

        T? enumValue = typeof(T).GetEnumValues()
            .Cast<T>()
            .SingleOrDefault(x => x.GetAttributeOfType<XmlEnumAttribute>() is { Name: { } } attr && attr.Name == value);

        if (enumValue is not null)
        {
            return enumValue;
        }

        if (Enum.TryParse(typeof(T), value, out object? parsed))
        {
            return (T)parsed!;
        }

        throw new InvalidOperationException($"Could not parse {value} as {typeof(T).Name}.");
    }

    public static T? GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
    {
        var type = enumVal.GetType();
        var memInfo = type.GetMember(enumVal.ToString());
        var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
        return (attributes.Length > 0) ? (T)attributes[0] : null;
    }
}