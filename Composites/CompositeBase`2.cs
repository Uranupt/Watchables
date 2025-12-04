using System.Collections.Generic;


namespace Watchables
{
  public abstract class CompositeBase<TValue, TPart> : NestedWatchable<TValue> where TPart : CompositeValueBase<TValue, TPart>
  {

    protected readonly List<TPart> _parts = new();

    public void AddPart(TPart part)
    {

    }

    public void AddParts(IEnumerable<TPart> parts)
    {

    }

    public void RemovePart(TPart part)
    {

    }

    public void RemoveParts(IEnumerable<TPart> parts)
    {

    }

    protected override bool CheckFatalDestruction(IWatchable dependency) => false;

    protected abstract void Sort();

  }
}
