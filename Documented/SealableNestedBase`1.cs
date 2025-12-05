using System;


namespace Watchables
{
  /// <summary>
  /// Extension of <see cref="NestedWatchable{T}"/> which also implements <see cref="ISealable"/>.
  /// </summary>
  public abstract class SealableNestedBase<T> : NestedWatchable<T>, ISealable
  {

    /// <inheritdoc/>
    public bool IsSealed { get; private set; }

    /// <inheritdoc/>
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