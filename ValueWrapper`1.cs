

namespace Watchables
{
  public sealed class ValueWrapper<T> : IValueWrapper<T>
  {

    private readonly T _value;

    public ValueWrapper(T value)
    {
      _value = value;
    }

    public static operator T(ValueWrapper<T> wrapper) => wrapper.ToValue();
    public static operator ValueWrapper<T>(T value) => new ValueWrapper<T>(value);

    public T ToValue() => _value;
    /// <summary> Returns a string representation of the underlying value. </summary>
    public override string ToString() => _value.ToString();

  }
}