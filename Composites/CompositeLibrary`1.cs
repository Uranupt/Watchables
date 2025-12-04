using System.Collections.Generic;


namespace Watchables
{
  public class CompositeLibrary<T> : OwnableBase, IEnforceNumeric<T> where T : unmanaged
  {

    protected readonly Dictionary<string, NumericComposite<T>> _collection = new();

    public NumericComposite<T> this[string name] => Get(name);

    public CompositeLibrary(object owner = null)
    {
      NumericUtility.ValidateType(typeof(T));
      if(owner != null)
      {
        SetOwner(owner);
      }
    }

    public NumericComposite<T> Get(string name)
    {
      if(!_collection.TryGetValue(name, out NumericComposite<T> value))
      {
        value = new NumericComposite<T>();
        value.SetOwner(_collection);
        _collection[name] = value;
      }
      return value;
    }

    public void AddStep(string name, NumericCompositePart<T> step) => Get(name).AddStep(step);
    public void AddSteps(string name, IEnumerable<NumericCompositePart<T>> steps) => Get(name).AddSteps(steps);
    public void RemoveStep(string name, NumericCompositePart<T> step) => Get(name).RemoveStep(step);
    public void RemoveSteps(string name, IEnumerable<NumericCompositePart<T>> steps) => Get(name).RemoveSteps(steps);

    public bool Clear(object owner = null)
    {
      if(IsOwned && !CompareToOwner(owner)) { return false; }
      foreach(NumericComposite<T> child in _collection.Values)
      {
        child.Destroy(_collection);
      }
      _collection.Clear();
      return true;
    }

  }
}