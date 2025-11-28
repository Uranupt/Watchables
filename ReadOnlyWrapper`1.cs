

namespace Watchables
{
  public sealed class ReadOnlyWrapper<T> : IValueWrapper<T>
  {

    private readonly T _value;

    public ReadOnlyWrapper(T value)
    {
      _value = value;
    }

    public static implicit operator T(ReadOnlyWrapper<T> wrapper) => wrapper.ToValue();
    public static implicit operator ReadOnlyWrapper<T>(T value) => new ReadOnlyWrapper<T>(value);

    public T ToValue() => _value;
    /// <summary> Returns a string representation of the underlying value. </summary>
    public override string ToString() => _value.ToString();

  }
}