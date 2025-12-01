using System;


namespace Watchables
{
  public abstract class SealableNestedBase<T> : NestedWatchable<T>, ISealable
  {

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

  }
}