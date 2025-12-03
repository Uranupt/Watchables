using System.Collections.Generic;


namespace Watchables
{
  public class CompositeLibrary<T> : SealableBase, IEnforceNumeric<T> where T : unmanaged
  {

    protected readonly Dictionary<string, CompositeWatchable<T>> _collection = new();

    public CompositeWatchable<T> this[string name] => Get(name);

    public CompositeLibrary(object owner = null)
    {
      if(owner != null)
      {
        SetOwner(owner);
      }
    }

    public CompositeWatchable<T> Get(string name)
    {
      if(!_collection.TryGetValue(name, out CompositeWatchable<T> value))
      {
        value = CreateNew();
        value.SetOwner(_collection);
        value.SetSealed(true, _collection);
        _collection[name] = value;
      }
      return value;
    }

    public bool AddStep(string name, CompositeStep<T> step, object owner = null)
    {
      if(!CanEdit(owner)) { return false; }
      return Get(name).AddStep(step, _collection);
    }

    public bool AddSteps(string name, IEnumerable<CompositeStep<T>> steps, object owner = null)
    {
      if(!CanEdit(owner)) { return false; }
      return Get(name).AddSteps(steps, _collection);
    }

    public bool RemoveStep(string name, CompositeStep<T> step, object owner = null)
    {
      if(!CanEdit(owner)) { return false; }
      return Get(name).RemoveStep(step, _collection);
    }

    public bool RemoveSteps(string name, IEnumerable<CompositeStep<T>> steps, object owner = null)
    {
      if(!CanEdit(owner)) { return false; }
      return Get(name).RemoveSteps(steps, _collection);
    }

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

    protected abstract CompositeWatchable<T> CreateNew();

  }
}