using System;
using System.Collections.Generic;
using System.Reflection;


namespace Watchables
{
  public class TaggedLibrary<TTag, TValue> : SealableBase, IEnforceNumeric<TValue>
    where TTag : Tag<TTag>
    where TValue : unmanaged
  {

    private readonly CompositeLibrary<TValue> _inner;

    public CompositeWatchable<TValue> this[TTag tag] => Get(tag);

    internal TaggedLibrary()
    {
      _inner = new CompositeLibrary<TValue>();
      _inner.SetOwner(this);
      _inner.SetSealed(true, this);
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

  public static class TaggedLibrary
  {

    public static TaggedLibrary<TTag, ushort> UShort<TTag>() where TTag : Tag<TTag> => new TaggedLibrary<TTag, ushort>();
    public static TaggedLibrary<TTag, uint> UInt<TTag>() where TTag : Tag<TTag> => new TaggedLibrary<TTag, uint>();
    public static TaggedLibrary<TTag, ulong> ULong<TTag>() where TTag : Tag<TTag> => new TaggedLibrary<TTag, ulong>();
    public static TaggedLibrary<TTag, short> Short<TTag>() where TTag : Tag<TTag> => new TaggedLibrary<TTag, short>();
    public static TaggedLibrary<TTag, int> Int<TTag>() where TTag : Tag<TTag> => new TaggedLibrary<TTag, int>();
    public static TaggedLibrary<TTag, long> Long<TTag>() where TTag : Tag<TTag> => new TaggedLibrary<TTag, long>();
    public static TaggedLibrary<TTag, decimal> Decimal<TTag>() where TTag : Tag<TTag> => new TaggedLibrary<TTag, decimal>();
    public static TaggedLibrary<TTag, float> Float<TTag>() where TTag : Tag<TTag> => new TaggedLibrary<TTag, float>();
    public static TaggedLibrary<TTag, double> Double<TTag>() where TTag : Tag<TTag> => new TaggedLibrary<TTag, double>();

  }
}