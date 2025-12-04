using System;
using System.Collections.Generic;


namespace Watchables
{
  public sealed class FlagsComposite<T> : NestedWatchable<T> where T : struct, Enum
  {

    private readonly List<FlagsCompositePart<T>> _parts = new();

    public FlagsComposite()
    {

    }
    
    public void AddPart(FlagsCompositePart<T> part)
    {
      MutationGuard(
        () =>
        {
          AddPartPrivate(part);
          Sort();
        }
      );
    }

    public void RemovePart(FlagsCompositePart<T> part)
    {
      MutationGuard(
        () =>
        {
          RemovePartPrivate(part);
          Sort();
        }
      );
    }

    protected override bool CheckFatalDestruction(IWatchable dependency) => false;

    protected override void Evaluate()
    {
      ulong val = 0;
      foreach(FlagsCompositePart<T> part in _parts)
      {
        val = part.SetTo ? val |= part.ToValue().ToULong() : val &= ~part.ToValue().ToULong();
      }
      _value = val.ToEnum<T>();
    }

    protected override void OnDestroyed(IWatchable self)
    {
      while(_parts.Count > 0)
      {
        RemovePartPrivate(_parts[0]);
      }
    }

    protected override void OnNonFatalDestruction(IWatchable dependency)
    {
      for(int i = 0; i < _parts.Count; i++)
      {
        if(_parts[i].Value == dependency)
        {
          RemovePartPrivate(_parts[i]);
          return;
        }
      }
    }

    private void AddPartPrivate(FlagsCompositePart<T> part)
    {
      if(_parts.Contains(part)) { return; }
      _parts.Add(part);
      if(part.Value is IWatchable)
      {
        Register(part as IWatchable);
      }
      part.Removed += RemovePartPrivate;
    }

    private void RemovePartPrivate(FlagsCompositePart<T> part)
    {
      if(!_parts.Remove(part)) { return; }
      if(part.Value is IWatchable)
      {
        Unregister(part as IWatchable);
      }
      part.Removed -= RemovePartPrivate;
    }

    private void Sort()
    {
      _parts.Sort(
        (x, y) =>
        {
          int resl = x.Priority.CompareTo(y.Priority);
          return resl != 0 ? resl : y.SetTo.CompareTo(x.SetTo);
        }
      );
    }

  }
}
