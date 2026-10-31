using Microsoft.AspNetCore.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ApexCharts.Internal;

/// <summary>
/// Internal use
/// </summary>
/// <remarks>
/// Makes deep copies of option objects, so that a copy can be changed without changing the original.
/// The chart services use it to give every DI scope (every Blazor Server circuit) its own copy of the
/// global options configured with <see cref="ApexChartsExtensions.AddApexCharts"/>. It is internal, and
/// the MAUI package reaches it through InternalsVisibleTo (both packages ship from the same release).
/// </remarks>
internal static class OptionsCopier
{
    private static readonly Assembly modelAssembly = typeof(ApexChartBaseOptions).Assembly;
    private static readonly MethodInfo memberwiseClone = typeof(object).GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic);

    // The DynamicDependency attributes on DeepCopy keep the List<T> and Dictionary<TKey, TValue>
    // constructors and the Dictionary<TKey, TValue>.Comparer property when the app is trimmed,
    // because the collection copies reach them through reflection.

    /// <summary>
    /// Returns a deep copy of <paramref name="source"/>, or null when it is null.
    /// </summary>
    /// <remarks>
    /// Option objects are copied member by member, including private fields, and so are the lists,
    /// dictionaries and arrays they hold. An option object is an instance of a class of this library, of a
    /// class derived from one (for example your own subclass of <see cref="ApexChartBaseOptions"/> or of
    /// <see cref="Theme"/>), or of your own implementation of <see cref="IApexChartBaseOptions"/>, also when
    /// that class implements <see cref="IDisposable"/> or <see cref="IAsyncDisposable"/>. The library's own
    /// components, chart services and disposable resources, and classes derived from them, are not options.
    /// Strings, value types, delegates, components and any other object (for example the items bound to a
    /// series) are immutable or not owned by the options, and are kept as the same reference. An object
    /// referenced twice in the source is copied once, so shared references and cycles are preserved.
    /// </remarks>
    /// <typeparam name="T">The type of the options</typeparam>
    /// <param name="source">The options to copy</param>
    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicConstructors, typeof(List<>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicProperties, typeof(Dictionary<,>))]
    public static T DeepCopy<T>(T source) where T : class
    {
        if (source == null) { return null; }
        return (T)Copy(source, new Dictionary<object, object>(ReferenceEqualityComparer.Instance));
    }

    private static object Copy(object source, Dictionary<object, object> copies)
    {
        if (source == null) { return null; }

        var type = source.GetType();
        if (!IsCopied(type)) { return source; }
        if (copies.TryGetValue(source, out var existing)) { return existing; }

        if (type.IsArray)
        {
            // Clone keeps the exact array type without creating one through reflection.
            var arrayCopy = (Array)((Array)source).Clone();
            copies[source] = arrayCopy;
            var elementType = type.GetElementType();
            if (elementType.IsValueType || elementType == typeof(string)) { return arrayCopy; }
            for (var i = 0; i < arrayCopy.Length; i++)
            {
                arrayCopy.SetValue(Copy(arrayCopy.GetValue(i), copies), i);
            }
            return arrayCopy;
        }

        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
        {
            var list = (IList)source;
            var listCopy = (IList)Activator.CreateInstance(type);
            copies[source] = listCopy;
            foreach (var item in list)
            {
                listCopy.Add(Copy(item, copies));
            }
            return listCopy;
        }

        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
        {
            var dictionary = (IDictionary)source;
            // Keep a custom comparer (for example one that ignores case). Fall back to the default
            // comparer if the property cannot be read.
            var comparer = type.GetProperty(nameof(Dictionary<object, object>.Comparer))?.GetValue(source);
            var dictionaryCopy = (IDictionary)(comparer == null ? Activator.CreateInstance(type) : Activator.CreateInstance(type, comparer));
            copies[source] = dictionaryCopy;
            foreach (DictionaryEntry entry in dictionary)
            {
                dictionaryCopy.Add(entry.Key, Copy(entry.Value, copies));
            }
            return dictionaryCopy;
        }

        var copy = memberwiseClone.Invoke(source, null);
        copies[source] = copy;
        for (var current = type; current != null && current != typeof(object); current = current.BaseType)
        {
            foreach (var field in current.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
            {
                if (field.FieldType.IsValueType) { continue; }
                var value = field.GetValue(copy);
                if (value != null)
                {
                    field.SetValue(copy, Copy(value, copies));
                }
            }
        }
        return copy;
    }

    private static bool IsCopied(Type type)
    {
        if (type.IsValueType || type == typeof(string) || typeof(Delegate).IsAssignableFrom(type)) { return false; }
        if (type.IsArray) { return type.GetArrayRank() == 1; }
        if (type.IsGenericType)
        {
            var definition = type.GetGenericTypeDefinition();
            if (definition == typeof(List<>) || definition == typeof(Dictionary<,>)) { return true; }
        }

        return IsOptionType(type);
    }

    /// <summary>
    /// An implementation of <see cref="IApexChartBaseOptions"/>, or a class of this library that is not a
    /// component, a chart service or a disposable resource, or a class derived from one (declared in any
    /// assembly). A consumer may configure the global options with a subclass, and sharing that object between
    /// scopes would bring back the leak these copies exist to prevent.
    /// </summary>
    /// <remarks>
    /// Only the library class decides whether a type is excluded, never what a subclass adds: a consumer's
    /// subclass of <see cref="ApexChartBaseOptions"/> or <see cref="Theme"/> that also implements
    /// <see cref="IDisposable"/> is still an option object and is still copied.
    /// </remarks>
    private static bool IsOptionType(Type type)
    {
        if (typeof(IApexChartBaseOptions).IsAssignableFrom(type)) { return true; }
        for (var current = type; current != null && current != typeof(object); current = current.BaseType)
        {
            if (current.Assembly == modelAssembly) { return !IsResource(current); }
        }
        return false;
    }

    /// <summary>
    /// The library classes that are not options: components, chart services and disposable resources.
    /// </summary>
    private static bool IsResource(Type libraryType)
    {
        return typeof(IComponent).IsAssignableFrom(libraryType)
            || typeof(IApexChartService).IsAssignableFrom(libraryType)
            || typeof(IDisposable).IsAssignableFrom(libraryType)
            || typeof(IAsyncDisposable).IsAssignableFrom(libraryType);
    }
}
