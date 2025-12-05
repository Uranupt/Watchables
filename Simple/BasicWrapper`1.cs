

namespace Watchables
{
  public sealed class BasicWrapper<T> : SealableBase, IValueWrapper<T>
  {

    private T _value;

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

    public T ToValue() => _value;

  }
}