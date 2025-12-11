using System.Collections.Generic;


namespace Watchables
{
  /// <summary>
  /// A wrapped <see cref="Dictionary{TKey, TValue}"/> with keys of type <typeparamref name="TKey"/> and values of type
  /// <typeparamref name="TComp"/>, where <typeparamref name="TComp"/> is derived from <see cref="CompositeBase{TPart}"/>.
  /// <br/> Provides access to, and lazy instantiation of, <typeparamref name="TComp"/> instances. Instances may not be manually added or removed
  /// except with <see cref="Clear"/>.
  /// </summary>
  /// <typeparam name="TKey"> The type of key used. </typeparam>
  /// <typeparam name="TComp"> The contained composite type. </typeparam>
  /// <typeparam name="TPart"> The parts the composite type is composed of. </typeparam>
  public class CompositeLibrary<TKey, TComp, TPart> : OwnableBase
    where TComp : CompositeBase<TPart>, new()
    where TPart : CompositePartBase<TPart>
  {

    /// <summary> The wrapped collection of <typeparamref name="TComp"/> instances. </summary>
    protected readonly Dictionary<TKey, TComp> _collection = new();

    public CompositeLibrary(object owner = null)
    {
      if(owner != null)
      {
        SetOwner(owner);
      }
    }

    /// <inheritdoc cref="Get"/>
    public TComp this[TKey key] => Get(key);

    /// <summary>
    /// Retrieves the <typeparamref name="TComp"/> instance with the given <paramref name="key"/>. Will create a new one if one does not exist.
    /// </summary>
    public TComp Get(TKey key)
    {
      if(!_collection.TryGetValue(key, out TComp value))
      {
        value = new TComp();
        value.SetOwner(_collection);
        _collection[key] = value;
      }
      return value;
    }

    /// <summary> Add the provided <paramref name="part"/> to the <typeparamref name="TComp"/> with the given <paramref name="key"/>. </summary>
    public void AddPart(TKey key, TPart part) => Get(key).AddPart(part);
    /// <summary> Add the provided <paramref name="parts"/> to the <typeparamref name="TComp"/> with the given <paramref name="key"/>. </summary>
    public void AddParts(TKey key, IEnumerable<TPart> parts) => Get(key).AddParts(parts);
    /// <summary> Remove the provided <paramref name="part"/> from the <typeparamref name="TComp"/> with the given <paramref name="key"/>. </summary>
    public void RemovePart(TKey key, TPart part) => Get(key).RemovePart(part);
    /// <summary> Remove the provided <paramref name="parts"/> from the <typeparamref name="TComp"/> with the given <paramref name="key"/>. </summary>
    public void RemoveParts(TKey key, IEnumerable<TPart> parts) => Get(key).RemoveParts(parts);

    /// <summary>
    /// Attempts to destroy all contained <typeparamref name="TComp"/> instances and clear the collection.
    /// </summary>
    /// <param name="owner"> The instance's current owner </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool Clear(object owner = null)
    {
      if(IsOwned && !CompareToOwner(owner)) { return false; }
      foreach(TComp child in _collection.Values)
      {
        child.Destroy(_collection);
      }
      _collection.Clear();
      return true;
    }

  }
}
