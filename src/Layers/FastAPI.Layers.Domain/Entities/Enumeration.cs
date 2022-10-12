namespace FastAPI.Layers.Domain.Entities;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

/// <summary>
/// Enumeration class.
/// </summary>
public abstract class Enumeration : IComparable
{
    private static readonly ConcurrentDictionary<Type, IEnumerable<object>> EnumCache = new ();

    /// <summary>
    /// Initializes a new instance of the <see cref="Enumeration"/> class.
    /// </summary>
    /// <param name="value">Enumeration value.</param>
    /// <param name="name">Enumeration name.</param>
    protected Enumeration(int value, string name)
    {
        this.Value = value;
        this.Name = name;
    }

    /// <summary>
    /// Gets enumeration value.
    /// </summary>
    public int Value { get; }

    /// <summary>
    /// Gets enumeration name.
    /// </summary>
    public string Name { get; }

    public static bool operator ==(Enumeration? left, Enumeration? right)
    {
        return EqualityComparer<Enumeration>.Default.Equals(left, right);
    }

    public static bool operator !=(Enumeration? left, Enumeration? right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Gets an enumerable list of all elements in the enumeration.
    /// </summary>
    /// <typeparam name="T">Type of enumeration.</typeparam>
    /// <returns>Returns an enumerable list of all elements in the enumeration.</returns>
    public static IEnumerable<T> GetAll<T>()
        where T : Enumeration
    {
        var type = typeof(T);

        var values = EnumCache.GetOrAdd(type, _ => type
            .GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Select(f => f.GetValue(null))
            .Cast<T>());

        return values.Cast<T>();
    }

    /// <summary>
    /// Gets enumeration object by specified enumeration type and value.
    /// </summary>
    /// <typeparam name="T">Enumeration type.</typeparam>
    /// <param name="value">Enumeration value.</param>
    /// <returns>Returns enumeration object by specified enumeration type and value.</returns>
    public static T FromValue<T>(int value)
        where T : Enumeration
        => Parse<T, int>(value, "value", item => item.Value == value);

    /// <summary>
    /// Gets enumeration object by specified enumeration type and name.
    /// </summary>
    /// <typeparam name="T">Enumeration type.</typeparam>
    /// <param name="name">Enumeration name.</param>
    /// <returns>Returns enumeration object by specified enumeration type and name.</returns>
    public static T FromName<T>(string name)
        where T : Enumeration
        => Parse<T, string>(name, "name", item => item.Name == name);

    /// <summary>
    /// Gets enumeration name by specified enumeration type and value.
    /// </summary>
    /// <typeparam name="T">Enumeration type.</typeparam>
    /// <param name="value">Enumeration value.</param>
    /// <returns>Returns enumeration name by specified enumeration type and value.</returns>
    public static string NameFromValue<T>(int value)
        where T : Enumeration
        => FromValue<T>(value).Name;

    /// <summary>
    /// Indicates if enumeration has specific value.
    /// </summary>
    /// <typeparam name="T">Enumeration type.</typeparam>
    /// <param name="value">Enumeration value.</param>
    /// <returns>Returns if enumeration has specific value.</returns>
    public static bool HasValue<T>(int value)
        where T : Enumeration
    {
        try
        {
            FromValue<T>(value);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <inheritdoc/>
    public override string ToString()
        => this.Name;

    /// <summary>
    /// Get enumeration item value.
    /// </summary>
    /// <returns>Returns enumeration item value.</returns>
    public int ToValue()
        => this.Value;

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        if (obj is not Enumeration otherValue)
        {
            return false;
        }

        var typeMatches = this.GetType() == obj.GetType();
        var valueMatches = this.Value.Equals(otherValue.Value);

        return typeMatches && valueMatches;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
        => (this.GetType().ToString() + this.Value).GetHashCode();

    /// <summary>
    /// Compare two enumeration objects.
    /// </summary>
    /// <param name="other">Compare against this object.</param>
    /// <returns>
    ///     Return Value – Description
    ///     Less than zero – This instance is less than value.
    ///     Zero – This instance is equal to value.
    ///     Greater than zero – This instance is greater than value.
    /// </returns>
    public int CompareTo(object? other)
        => this.Value.CompareTo(((Enumeration)other!).Value);

    /// <summary>
    /// Try parse string value and get an Enumeration instance.
    /// </summary>
    /// <typeparam name="TEnumeration">Enumeration type.</typeparam>
    /// <param name="valueOrName">String value.</param>
    /// <param name="enumeration">Parsed enumeration value.</param>
    /// <returns>True if value is parsed successfully.</returns>
    public static bool TryParse<TEnumeration>(
        string valueOrName,
        out TEnumeration enumeration)
            where TEnumeration : Enumeration
                => TryParse(item => item.Name == valueOrName, out enumeration!) || 
                   (int.TryParse(valueOrName, out var value) && TryParse(item => item.Value == value, out enumeration!));

    private static T Parse<T, TValue>(TValue value, string description, Func<T, bool> predicate)
        where T : Enumeration
    {
        var matchingItem = GetAll<T>().FirstOrDefault(predicate);

        if (matchingItem is null)
        {
            throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");
        }

        return matchingItem;
    }

    private static bool TryParse<TEnumeration>(
        Func<TEnumeration, bool> predicate,
        out TEnumeration? enumeration)
            where TEnumeration : Enumeration
    {
        enumeration = GetAll<TEnumeration>().FirstOrDefault(predicate);
        return enumeration != null;
    }
}
