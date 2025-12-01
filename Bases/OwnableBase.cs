

namespace Watchables
{

  public abstract class OwnableBase : IOwnable
  {

    protected object _owner;
    public bool IsOwned => _owner != null;

    public virtual bool SetOwner(object owner)
    {
      if(IsOwned || owner == null) { return false; }
      _owner = owner;
      return true;
    }

    public virtual bool ClearOwner(object owner)
    {
      if(IsOwned && !CompareToOwner(owner)) { return false; }
      _owner = null;
      return true;
    }

    public virtual bool CompareToOwner(object owner) => IsOwned && _owner == owner;

  }

}