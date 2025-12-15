

namespace Watchables
{
  /// <summary>
  /// An read-only implementation of <see cref="IWrapper{T}"/>.
  /// </summary>
  public sealed class ReadOnlyWrapper<T> : IWrapper<T>
  {

    /// <inheritdoc/>
    public T Value { get; private set; }

    public ReadOnlyWrapper(T value)
    {
      Value = value;
    }

    public static implicit operator T(ReadOnlyWrapper<T> wrapper) => wrapper.Value;

    /// <summary> Returns a string representation of the underlying value. </summary>
    public override string ToString() => Value.ToString();

  }
}