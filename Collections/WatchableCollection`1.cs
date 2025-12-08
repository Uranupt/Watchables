using System.Collections;
using System.Collections.Generic;


namespace Watchables
{
  /// <summary>
  /// Generic base class extension of <see cref="WatchableCollection"/> for directly mutable implementations. Implements <see cref="IEnumerable{T}"/>
  /// </summary>
  public abstract class WatchableCollection<T> : WatchableCollection, IEnumerable<T>
  {

    /// <summary> The <see cref="ICollection{T}"/> instance this wraps and operates on. </summary>
    protected abstract ICollection<T> Collection { get; }

    ///<inheritdoc/>
    public override int Count => Collection.Count;

    /// <summary> Determines whether the collection contains the provided value. </summary>
    public bool Contains(T item) => Collection.Contains(item);

    /// <summary> Attempts to add the provided value to the collection. </summary>
    /// <param name="owner"> The current owner of the collection, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool Add(T item, object owner = null)
    {
      return MutationGuard(() => Collection.Add(item), owner);
    }

    /// <summary> Attempts to add the provided range of values to the collection. </summary>
    /// <param name="owner"> The current owner of the collection, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool AddRange(IEnumerable<T> items, object owner = null)
    {
      return MutationGuard(
        () => { foreach(T item in items) { Collection.Add(item); } }, 
        owner
      );
    }

    /// <summary> Attempts to remove the provided value from the collection. </summary>
    /// <param name="owner"> The current owner of the collection, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool Remove(T item, object owner = null)
    {
      return MutationGuard(() => Collection.Remove(item), owner);
    }

    /// <summary> Attempts to remove the provided range of values from the collection. </summary>
    /// <param name="owner"> The current owner of the collection, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool RemoveRange(IEnumerable<T> items, object owner = null)
    {
      return MutationGuard(
        () => { foreach(T item in items) { Collection.Remove(item); } }, 
        owner
      );
    }

    /// <summary> Attempts to clear collection of all values. </summary>
    /// <param name="owner"> The current owner of the collection, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool Clear(object owner = null)
    {
      return MutationGuard(ClearValue, owner);
    }

    ///<inheritdoc/>
    public IEnumerator<T> GetEnumerator() => Collection.GetEnumerator();

    ///<inheritdoc/>
    protected override void ClearValue() => Collection.Clear();
    ///<inheritdoc/>
    protected override IEnumerator GetNonGenericEnumerator() => GetEnumerator();

  }
}