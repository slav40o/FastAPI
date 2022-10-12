namespace FastAPI.Layers.Infrastructure.Http.Json;

using FastAPI.Layers.Domain.Entities;

using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;


/// <summary>
/// Json Converter for <see cref="Enumeration"/> class.
/// </summary>
public class EnumerationJsonConverter : JsonConverter<Enumeration>
{
    private const string NameProperty = "Name";

    /// <summary>
    /// Determines whether the specified type is of type <see cref="Enumeration"/>.
    /// </summary>
    /// <param name="objectType">The type to compare against.</param>
    /// <returns>true if the type can be converted; otherwise false.</returns>
    public override bool CanConvert(Type objectType)
        => objectType.IsSubclassOf(typeof(Enumeration));

    /// <inheritdoc/>
    public override Enumeration? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
            case JsonTokenType.Number:
                return GetEnumerationFromJson(reader.GetString(), typeToConvert);
            case JsonTokenType.Null:
                return null;
            default:
                throw new JsonException($"Unexpected token {reader.TokenType} when parsing enumeration.");
        }
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, Enumeration value, JsonSerializerOptions options)
    {
        if (value is null)
        {
            writer.WriteNull(NameProperty);
            return;
        }

        var name = value.GetType().GetProperty(NameProperty, BindingFlags.Public | BindingFlags.Instance);
        if (name is null)
        {
            throw new JsonException($"Error while writing JSON for {value}");
        }

        writer.WriteStringValue(name.GetValue(value)?.ToString());
    }

    private static Enumeration? GetEnumerationFromJson(string? nameOrValue, Type objectType)
    {
        try
        {
            object result = default!;

            var methodInfo = typeof(Enumeration).GetMethod(
                nameof(Enumeration.TryParse),
                BindingFlags.Static | BindingFlags.Public);

            if (methodInfo is null)
            {
                throw new JsonException("Serialization not supported.");
            }

            var genericMethod = methodInfo.MakeGenericMethod(objectType);

            var args = new[] { nameOrValue, result };

            genericMethod.Invoke(null, args);

            return args[1] as Enumeration;
        }
        catch (Exception)
        {
            throw new JsonException($"Error converting value '{nameOrValue}' to a enumeration,");
        }
    }
}