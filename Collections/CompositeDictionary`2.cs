using System.Collections.Generic;


namespace Watchables
{
  public sealed class CompositeDictionary<TKey, TValue> : CompositeCollection<KeyValuePair<TKey, TValue>>, IWatchableDictionary<TKey, TValue>
  {

    private readonly Dictionary<TKey, TValue> _dictionary = new();

    protected override ICollection<KeyValuePair<TKey, TValue>> Collection => _dictionary;

    public TValue this[TKey key] => _dictionary[key];
    public IReadOnlyCollection<TKey> Keys => _dictionary.Keys;
    public IReadOnlyCollection<TValue> Values => _dictionary.Values;

    public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);
    public bool ContainsValue(TValue value) => _dictionary.ContainsValue(value);

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