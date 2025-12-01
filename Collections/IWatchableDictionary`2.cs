using System.Collections.Generic;


namespace Watchables
{
  public interface IWatchableDictionary<TKey, TValue> : IWatchable
  {

    TValue this[TKey key] { get; }

    IReadOnlyCollection<TKey> Keys { get; }
    IReadOnlyCollection<TValue> Values { get; }

    bool ContainsKey(TKey key);
    bool ContainsValue(TValue value);

    bool TryGetValue(TKey key, out TValue value);

  }
}