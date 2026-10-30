using Microsoft.AspNetCore.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace ApexCharts.Internal;

/// <summary>
/// Internal use
/// </summary>
/// <remarks>
/// Makes deep copies of option objects, so that a copy can be changed without changing the original.
/// The chart services use it to give every DI scope (every Blazor Server circuit) its own copy of the
/// global options configured with <see cref="ApexChartsExtensions.AddApexCharts"/>.
/// </remarks>
public static class OptionsCopier
{
    private static readonly Assembly modelAssembly = typeof(ApexChartBaseOptions).Assembly;
    private static readonly MethodInfo memberwiseClone = typeof(object).GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic);

    /// <summary>
    /// Returns a deep copy of <paramref name="source"/>, or null when it is null.
    /// </summary>
    /// <remarks>
    /// Option classes of this library are copied member by member, including private fields, and so are
    /// the lists, dictionaries and arrays they hold. Strings, value types, delegates, components and
    /// objects of other assemblies (for example the items bound to a series) are immutable or not owned
    /// by the options, and are kept as the same reference. An object referenced twice in the source is
    /// copied once, so shared references and cycles are preserved.
    /// </remarks>
    /// <typeparam name="T">The type of the options</typeparam>
    /// <param name="source">The options to copy</param>
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
            var array = (Array)source;
            var arrayCopy = Array.CreateInstance(type.GetElementType(), array.Length);
            copies[source] = arrayCopy;
            for (var i = 0; i < array.Length; i++)
            {
                arrayCopy.SetValue(Copy(array.GetValue(i), copies), i);
            }
            return arrayCopy;
        }

        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
        {
            var list = (IList)source;
            var listCopy = (IList)Activator.CreateInstance(type, list.Count);
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
            var comparer = type.GetProperty(nameof(Dictionary<object, object>.Comparer)).GetValue(source);
            var dictionaryCopy = (IDictionary)Activator.CreateInstance(type, comparer);
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

        return type.Assembly == modelAssembly
            && !typeof(IComponent).IsAssignableFrom(type)
            && !typeof(IApexChartService).IsAssignableFrom(type)
            && !typeof(IDisposable).IsAssignableFrom(type)
            && !typeof(IAsyncDisposable).IsAssignableFrom(type);
    }
}
