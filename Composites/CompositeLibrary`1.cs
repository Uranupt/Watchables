using System.Collections.Generic;


namespace Watchables
{
  public class CompositeLibrary<T> : OwnableBase, IEnforceNumeric<T> where T : unmanaged
  {

    protected readonly Dictionary<string, CompositeWatchable<T>> _collection = new();

    public CompositeWatchable<T> this[string name] => Get(name);

    public CompositeLibrary(object owner = null)
    {
      NumericUtility.ValidateType(typeof(T));
      if(owner != null)
      {
        SetOwner(owner);
      }
    }

    public CompositeWatchable<T> Get(string name)
    {
      if(!_collection.TryGetValue(name, out CompositeWatchable<T> value))
      {
        value = new CompositeWatchable<T>();
        value.SetOwner(_collection);
        _collection[name] = value;
      }
      return value;
    }

    public void AddPart(string name, CompositePart<T> part) => Get(name).AddPart(part);
    public void AddParts(string name, IEnumerable<CompositePart<T>> parts) => Get(name).AddParts(parts);
    public void RemovePart(string name, CompositePart<T> part) => Get(name).RemovePart(part);
    public void RemoveParts(string name, IEnumerable<CompositePart<T>> parts) => Get(name).RemoveParts(parts);

    public bool Clear(object owner = null)
    {
      if(IsOwned && !CompareToOwner(owner)) { return false; }
      foreach(CompositeWatchable<T> child in _collection.Values)
      {
        child.Destroy(_collection);
      }
      _collection.Clear();
      return true;
    }

  }
}