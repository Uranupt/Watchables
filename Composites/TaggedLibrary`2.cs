using System;
using System.Collections.Generic;
using System.Reflection;


namespace Watchables
{
  public class TaggedCompositeLibrary<TTag, TValue> : OwnableBase, IEnforceNumeric<TValue>
    where TTag : Tag<TTag>
    where TValue : unmanaged
  {

    private readonly CompositeLibrary<TValue> _inner;

    public NumericComposite<TValue> this[TTag tag] => Get(tag);

    public TaggedCompositeLibrary(object owner = null)
    {
      NumericUtility.ValidateType(typeof(TValue));
      _inner = new CompositeLibrary<TValue>();
      _inner.SetOwner(this);
      if(owner != null)
      {
        SetOwner(owner);
      }
    }

    public NumericComposite<TValue> Get(TTag tag) => _inner.Get(tag.Name);
    public void AddStep(TTag tag, NumericCompositePart<TValue> step) => _inner.AddStep(tag.Name, step);
    public void AddSteps(TTag tag, IEnumerable<NumericCompositePart<TValue>> steps) => _inner.AddSteps(tag.Name, steps);
    public void RemoveStep(TTag tag, NumericCompositePart<TValue> step) => _inner.RemoveStep(tag.Name, step);
    public void RemoveSteps(TTag tag, IEnumerable<NumericCompositePart<TValue>> steps) => _inner.RemoveSteps(tag.Name, steps);

    public bool Clear(object owner = null)
    {
      if(IsOwned && !CompareToOwner(owner)) { return false; }
      return _inner.Clear(this);
    }

  }
}