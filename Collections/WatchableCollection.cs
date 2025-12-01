using System;
using System.Collections;


namespace Watchables
{
  public abstract class WatchableCollection : WatchableBase, ISealable, IEnumerable
  {

    public abstract int Count { get; }
    public bool IsSealed { get; private set; }

    public bool SetSealed(bool sealedState, object owner = null)
    {
      if(IsDestroyed || (IsOwned && !CompareToOwner(owner))) { return false; }
      IsSealed = sealedState;
      return true;
    }

    protected override bool MutationGuard(Action action, object owner = null)
    {
      if(IsSealed && !CompareToOwner(owner)) { return false; }
      return base.MutationGuard(action, owner);
    }

    protected abstract IEnumerator GetNonGenericEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetNonGenericEnumerator();

  }
}