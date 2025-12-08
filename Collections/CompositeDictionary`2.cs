using System.Collections.Generic;


namespace Watchables
{
  /// <summary>
  /// An implementation of <see cref="CompositeCollection{T}"/> which represents a <see cref="Dictionary{TKey, TValue}"/>
  /// constructed from multiple <see cref="IEnumerable{T}"/> sources. Provides mirrors of many of the querying methods in
  /// <see cref="Dictionary{TKey, TValue}"/>.
  /// </summary>
  public sealed class CompositeDictionary<TKey, TValue> : CompositeCollection<KeyValuePair<TKey, TValue>>, IWatchableDictionary<TKey, TValue>
  {

    private readonly Dictionary<TKey, TValue> _dictionary = new();

    /// <inheritdoc/>
    protected override ICollection<KeyValuePair<TKey, TValue>> Collection => _dictionary;

    /// <inheritdoc/>
    public TValue this[TKey key] => _dictionary[key];

    /// <inheritdoc/>
    public IReadOnlyCollection<TKey> Keys => _dictionary.Keys;

    /// <inheritdoc/>
    public IReadOnlyCollection<TValue> Values => _dictionary.Values;

    /// <inheritdoc/>
    public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);
    /// <inheritdoc/>
    public bool ContainsValue(TValue value) => _dictionary.ContainsValue(value);

    /// <inheritdoc/>
    public bool TryGetValue(TKey key, out TValue value)
    {
      if(IsDestroyed)
      {
        value = default;
        return false; 
      }
      return _dictionary.TryGetValue(key, out value);
    }

  }
}