using System.Collections;
using System.Collections.Generic;


namespace Watchables
{
  public abstract class WatchableCollection<T> : WatchableCollection, IEnumerable<T>
  {

    protected abstract ICollection<T> Collection { get; }

    public override int Count => Collection.Count;

    public bool Contains(T item) => Collection.Contains(item);

    public bool Add(T item, object owner = null)
    {
      return MutationGuard(() => Collection.Add(item), owner);
    }

    public bool AddRange(IEnumerable<T> items, object owner = null)
    {
      return MutationGuard(
        () => { foreach(T item in items) { Collection.Add(item); } }, 
        owner
      );
    }

    public bool Remove(T item, object owner = null)
    {
      return MutationGuard(() => Collection.Remove(item), owner);
    }

    public bool RemoveRange(IEnumerable<T> items, object owner = null)
    {
      return MutationGuard(
        () => { foreach(T item in items) { Collection.Remove(item); } }, 
        owner
      );
    }

    public bool Clear(object owner = null)
    {
      return MutationGuard(ClearValue, owner);
    }

    public IEnumerator<T> GetEnumerator() => Collection.GetEnumerator();

    protected override void ClearValue() => Collection.Clear();
    protected override IEnumerator GetNonGenericEnumerator() => GetEnumerator();

  }
}