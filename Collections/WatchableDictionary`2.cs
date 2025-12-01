using System.Collections.Generic;


namespace Watchables
{
  public sealed class WatchableDictionary<TKey, TValue> : WatchableCollection<KeyValuePair<TKey, TValue>>, IWatchableDictionary<TKey, TValue>
  {

    private readonly Dictionary<TKey, TValue> _dictionary = new();

    protected override ICollection<KeyValuePair<TKey, TValue>> Collection => _dictionary;

    public TValue this[TKey key] => _dictionary[key];

    public TValue this[TKey key, object owner]
    {
      get => this[key];
      set => Set(key, value, owner);
    }

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

    public bool Add(TKey key, TValue value, object owner = null)
    {
      return Add(new KeyValuePair<TKey, TValue>(key, value), owner);
    }

    public bool Remove(TKey key, object owner = null)
    {
      return MutationGuard(() => _dictionary.Remove(key), owner);
    }

    public bool TryAdd(TKey key, TValue value, object owner = null)
    {
      if(ContainsKey(key)) { return false; }
      return Add(key, value, owner);
    }

    public bool Set(TKey key, TValue value, object owner = null)
    {
      return MutationGuard(() => _dictionary[key] = value, owner);
    }

  }
}