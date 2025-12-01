

namespace Watchables
{
  public abstract class SealableBase : OwnableBase, ISealable
  {

    public bool IsSealed { get; protected set; }

    public virtual bool SetSealed(bool sealedState, object owner = null)
    {
      if(IsOwned && !CompareToOwner(owner)) { return false; }
      IsSealed = sealedState;
      return true;
    }

    protected bool CanEdit(object owner = null) => !IsSealed || CompareToOwner(owner);

  }
}