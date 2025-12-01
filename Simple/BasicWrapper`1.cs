

namespace Watchables
{
  public sealed class BasicWrapper<T> : IValueWrapper<T>, ISealable
  {

    private T _value;
    private object _owner;

    public bool IsSealed { get; private set; }
    public bool IsOwned { get; private set; }

    public BasicWrapper()
    {

    }

    public BasicWrapper(T value)
    {
      _value = value;
    }

    public static implicit operator T(BasicWrapper<T> wrapper) => wrapper.ToValue();

    public bool SetValue(T value)
    {
      if(IsSealed) { return false; }
      _value = value;
      return true;
    }

    public bool SetValue(T value, object owner)
    {
      if(!CompareToOwner(owner)) { return false; }
      _value = value;
      return true;
    }

    public bool SetOwner(object owner)
    {
      if(_owner != null || owner == null) { return false; }
      _owner = owner;
      return true;
    }

    public bool ClearOwner(object owner)
    {
      if(_owner != null && _owner != owner) { return false; }
      _owner = null;
      return true;
    }

    public bool CompareToOwner(object owner) => IsOwned && _owner == owner;

    public bool SetSealed(bool sealedState, object owner = null)
    {
      if(IsOwned && !CompareToOwner(owner)) { return false; }
      IsSealed = sealedState;
      return true;
    }

    public T ToValue() => _value;

  }
}