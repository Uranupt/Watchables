using System.Collections.Generic;


namespace Watchables
{
  /// <summary>
  /// Common interface for <see cref="WatchableCollection"/> implementations which wrap a <see cref="Dictionary{TKey, TValue}"/>.
  /// Provides mirrors of the Key and Value querying methods in <see cref="Dictionary{TKey, TValue}"/>.
  /// </summary>
  public interface IWatchableDictionary<TKey, TValue> : IWatchable
  {

    /// <summary> Attempts to retrieve the item with the provided <paramref name="key"/>. </summary>
    TValue this[TKey key] { get; }

    /// <summary> A read-only collection of the keys contained in the <see cref="Dictionary{TKey, TValue}"/>. </summary>
    IReadOnlyCollection<TKey> Keys { get; }

    /// <summary> A read-only collection of the values contained in the <see cref="Dictionary{TKey, TValue}"/>. </summary>
    IReadOnlyCollection<TValue> Values { get; }

    /// <inheritdoc cref="Dictionary{TKey, TValue}.ContainsKey(TKey)"/>
    bool ContainsKey(TKey key);

    /// <inheritdoc cref="Dictionary{TKey, TValue}.ContainsValue(TValue)"/>
    bool ContainsValue(TValue value);

    /// <inheritdoc cref="Dictionary{TKey, TValue}.TryGetValue(TKey, out TValue)"/>
    bool TryGetValue(TKey key, out TValue value);

  }
}