//-----------------------------------------------------------------------
// <copyright file="CollectionExtensions.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>Extensions for working with text</summary>
public static class CollectionExtensions
{
    /// <summary>Creates a dictionary from an enumeration of KeyValuePairs</summary>
    /// <typeparam name="TKey">Type of the keys</typeparam>
    /// <typeparam name="TValue">Type of the values</typeparam>
    /// <param name="sequence">Sequence of KeyValuePairs</param>
    /// <returns>The created dictionary</returns>
    /// <exception cref="System.ArgumentNullException">One or more keys are null</exception>
    /// <exception cref="System.ArgumentException">The sequence contains duplicate keys</exception>
    public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> sequence)
    {
        return sequence.ToDictionary(false);
    }
    
    /// <summary>Creates a dictionary from an enumeration of KeyValuePairs</summary>
    /// <typeparam name="TKey">Type of the keys</typeparam>
    /// <typeparam name="TValue">Type of the values</typeparam>
    /// <param name="sequence">Sequence of KeyValuePairs</param>
    /// <param name="ignoreDuplicateKeys">Whether to ignore duplicate keys (the first value for a given key will be used)</param>
    /// <returns>The created dictionary</returns>
    /// <exception cref="System.ArgumentNullException">One or more keys are null</exception>
    /// <exception cref="System.ArgumentException">The sequence contains duplicate keys</exception>
    public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> sequence, bool ignoreDuplicateKeys)
    {
        return
            (ignoreDuplicateKeys ? sequence.Distinct(new KeyValuePairKeyComparer<TKey, TValue>()) : sequence)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }

    /// <summary>Returns distinct elements from a sequence by using a specified predicate to compare values.</summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">The sequence to remove duplicate elements from.</param>
    /// <param name="predicate">Predicate to compare values</param>
    /// <returns>A sequence that contains distinct elements from the source sequence.</returns>
    /// <exception cref="System.ArgumentNullException">source is null.</exception>
    public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, bool> predicate)
    {
        return source.Distinct(new PredicateComparer<TSource>(predicate));
    }

    /// <summary>Predicate EqualityComparer</summary>
    /// <typeparam name="T">Type of values to compare</typeparam>
    public class PredicateComparer<T> : EqualityComparer<T>
    {
        /// <summary>Equality comparer predicate</summary>
        public readonly Func<T, T, bool> Predicate;

        /// <summary>Initializes a new instance of the PredicateComparer class</summary>
        /// <param name="predicate">Predicate used to compare values</param>
        public PredicateComparer(Func<T, T, bool> predicate)
        {
            this.Predicate = predicate;
        }

        /// <summary>determines whether two objects of type T are equal.</summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>true if the specified objects are equal; otherwise, false.</returns>
        public override bool Equals(T x, T y)
        {
            return this.Predicate(x, y);
        }

        /// <summary>Serves as a hash function for the specified object</summary>
        /// <param name="obj">The object for which to get a hash code.</param>
        /// <returns>A hash code for the specified object.</returns>
        /// <exception cref="System.ArgumentNullException">The type of obj is a reference type and obj is null.</exception>
        public override int GetHashCode(T obj)
        {
            if (typeof(T).IsByRef && obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            return obj.GetHashCode();
        }
    }

    /// <summary>Compares KeyValuePair keys</summary>
    /// <typeparam name="TKey">Type of keys</typeparam>
    /// <typeparam name="TValue">Type of values</typeparam>
    public class KeyValuePairKeyComparer<TKey, TValue> : PredicateComparer<KeyValuePair<TKey, TValue>>
    {
        /// <summary>Initializes a new instance of the KeyValuePairKeyComparer class</summary>
        public KeyValuePairKeyComparer() : base((a, b) => a.Key.Equals(b.Key)) { }
    }
}