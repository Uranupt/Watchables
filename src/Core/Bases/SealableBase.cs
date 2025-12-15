

namespace Watchables
{
  /// <summary>
  /// Extension of <see cref="OwnableBase"/> which also implements <see cref="ISealable"/>, providing common implementation.
  /// </summary>
  public abstract class SealableBase : OwnableBase, ISealable
  {

    /// <inheritdoc/>
    public bool IsSealed { get; protected set; }

    /// <inheritdoc/>
    public virtual bool SetSealed(bool sealedState, object owner = null)
    {
      if(IsOwned && !CompareToOwner(owner)) { return false; }
      IsSealed = sealedState;
      return true;
    }

  }
}