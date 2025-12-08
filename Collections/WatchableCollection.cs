using System;
using System.Collections;


namespace Watchables
{
  /// <summary>
  /// Base class for Watchable collections. 
  /// Derived from <see cref="WatchableBase"/>, implements <see cref="ISealable"/> and <see cref="IEnumerable"/>.
  /// </summary>
  public abstract class WatchableCollection : WatchableBase, ISealable, IEnumerable
  {

    /// <summary> The number of items in this collection. </summary>
    public abstract int Count { get; }
    /// <inheritdoc/>
    public bool IsSealed { get; private set; }

    /// <inheritdoc/>
    public bool SetSealed(bool sealedState, object owner = null)
    {
      if(IsDestroyed || (IsOwned && !CompareToOwner(owner))) { return false; }
      IsSealed = sealedState;
      return true;
    }

    /// <inheritdoc/>
    protected override bool MutationGuard(Action action, object owner = null)
    {
      if(IsSealed && !CompareToOwner(owner)) { return false; }
      return base.MutationGuard(action, owner);
    }

    /// <summary> This method provides the <see cref="IEnumerator"/> to satisfy <see cref="IEnumerable.GetEnumerator"/> </summary>
    protected abstract IEnumerator GetNonGenericEnumerator();

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => GetNonGenericEnumerator();

  }
}