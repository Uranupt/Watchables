using System;
using System.Collections.Generic;


namespace Watchables
{
  public class TaggedLibrary<TTag, TValue> : SealableBase
    where TTag : Tag<TTag>
    where TValue : unmanaged
  {

    private readonly CompositeLibrary<TValue> _inner;

    public CompositeWatchable<TValue> this[TTag tag] => Get(tag);

    public TaggedLibrary(CompositeLibrary<TValue> inner, object owner = null)
    {
      if(inner == null || inner.IsOwned)
      {
        throw new ArgumentException("Provided CompositeLibrary was either null or already owned.");
      }
      _inner = inner;
      _inner.SetOwner(this);
      _inner.SetSealed(true, this);
      _inner.Clear(this);
      if(owner != null)
      {
        SetOwner(owner);
      }
    }

    public static TaggedLibrary<TTag, TValue> New<T>(object owner = null) where T : CompositeLibrary<TValue>, new()
    {
      return new TaggedLibrary<TTag, TValue>(new T(), owner);
    }

    public CompositeWatchable<TValue> Get(TTag tag) => _inner.Get(tag.Name);

    public bool AddStep(TTag tag, CompositeStep<TValue> step, object owner = null)
    {
      if(!CanEdit(owner)) { return false; }
      return _inner.AddStep(tag.Name, step, this);
    }

    public bool AddSteps(TTag tag, IEnumerable<CompositeStep<TValue>> steps, object owner = null)
    {
      if(!CanEdit(owner)) { return false; }
      return _inner.AddSteps(tag.Name, steps, this);
    }

    public bool RemoveStep(TTag tag, CompositeStep<TValue> step, object owner = null)
    {
      if(!CanEdit(owner)) { return false; }
      return _inner.RemoveStep(tag.Name, step, this);
    }

    public bool RemoveSteps(TTag tag, IEnumerable<CompositeStep<TValue>> steps, object owner = null)
    {
      if(!CanEdit(owner)) { return false; }
      return _inner.RemoveSteps(tag.Name, steps, this);
    }

    public bool Clear(object owner = null)
    {
      if(IsOwned && !CompareToOwner(owner)) { return false; }
      return _inner.Clear(this);
    }

  }
}