using System.Collections.Generic;


namespace Watchables
{
  public class TaggedCompositeLibrary<TTag, TValue> : OwnableBase, IEnforceNumeric<TValue>
    where TTag : Tag<TTag>
    where TValue : unmanaged
  {

    private readonly CompositeLibrary<TValue> _inner;

    public CompositeWatchable<TValue> this[TTag tag] => Get(tag);

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

    public CompositeWatchable<TValue> Get(TTag tag) => _inner.Get(tag.Name);
    public void AddPart(TTag tag, CompositePart<TValue> part) => _inner.AddPart(tag.Name, part);
    public void AddParts(TTag tag, IEnumerable<CompositePart<TValue>> parts) => _inner.AddParts(tag.Name, parts);
    public void RemovePart(TTag tag, CompositePart<TValue> part) => _inner.RemovePart(tag.Name, part);
    public void RemoveParts(TTag tag, IEnumerable<CompositePart<TValue>> parts) => _inner.RemoveParts(tag.Name, parts);

    public bool Clear(object owner = null)
    {
      if(IsOwned && !CompareToOwner(owner)) { return false; }
      return _inner.Clear(this);
    }

  }
}