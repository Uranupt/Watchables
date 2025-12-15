using System.Collections.Generic;


namespace Watchables
{
  /// <summary>
  /// An implementation of <see cref="WatchableCollection{T}"/> which represents a <see cref="Dictionary{TKey, TValue}"/>.
  /// Provides mirrors of many of the querying and mutation methods in <see cref="Dictionary{TKey, TValue}"/>.
  /// </summary>
  public sealed class WatchableDictionary<TKey, TValue> : WatchableCollection<KeyValuePair<TKey, TValue>>, IWatchableDictionary<TKey, TValue>
  {

    private readonly Dictionary<TKey, TValue> _dictionary = new();

    /// <inheritdoc/>
    protected override ICollection<KeyValuePair<TKey, TValue>> Collection => _dictionary;

    /// <inheritdoc/>
    public TValue this[TKey key] => _dictionary[key];

    /// <summary> Attempts to retrieve or set the item at the given <paramref name="key"/>. </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    public TValue this[TKey key, object owner]
    {
      get => this[key];
      set => Set(key, value, owner);
    }

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

    /// <summary>
    /// Attempts to add the provided <paramref name="value"/> to the <see cref="Dictionary{TKey, TValue}"/> with 
    /// the provided <paramref name="key"/>. Will throw if the <paramref name="key"/> already exists in the <see cref="Dictionary{TKey, TValue}"/>.
    /// </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool Add(TKey key, TValue value, object owner = null)
    {
      return Add(new KeyValuePair<TKey, TValue>(key, value), owner);
    }

    /// <summary>
    /// Attempts to remove the item with the provided <paramref name="key"/> from the <see cref="Dictionary{TKey, TValue}"/>.
    /// </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. (Not if there was a value to remove, only if the attempt to remove was allowed) </returns>
    public bool Remove(TKey key, object owner = null)
    {
      return MutationGuard(() => _dictionary.Remove(key), owner);
    }


    /// <summary>
    /// Attempts to add the provided <paramref name="value"/> to the <see cref="Dictionary{TKey, TValue}"/> with 
    /// the provided <paramref name="key"/>.
    /// </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the <paramref name="key"/> did not already exist and the operation was allowed. </returns>
    public bool TryAdd(TKey key, TValue value, object owner = null)
    {
      if(ContainsKey(key)) { return false; }
      return Add(key, value, owner);
    }

    /// <summary>
    /// Attempts to set the <paramref name="value"/> in the <see cref="Dictionary{TKey, TValue}"/> at 
    /// the provided <paramref name="key"/>.
    /// </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the the operation was allowed. </returns>
    public bool Set(TKey key, TValue value, object owner = null)
    {
      return MutationGuard(() => _dictionary[key] = value, owner);
    }

  }
}