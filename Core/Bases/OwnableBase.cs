

namespace Watchables
{
  /// <summary>
  /// Base class for <see cref="IOwnable"/>, providing common implementation of ownership semantics.
  /// </summary>
  public abstract class OwnableBase : IOwnable
  {

    protected object _owner;
    /// <inheritdoc/>
    public bool IsOwned => _owner != null;

    /// <inheritdoc/>
    public virtual bool SetOwner(object owner)
    {
      if(IsOwned || owner == null) { return false; }
      _owner = owner;
      return true;
    }

    /// <inheritdoc/>
    public virtual bool ClearOwner(object owner)
    {
      if(IsOwned && !CompareToOwner(owner)) { return false; }
      _owner = null;
      return true;
    }

    /// <inheritdoc/>
    public virtual bool CompareToOwner(object owner) => IsOwned && _owner == owner;

  }

}