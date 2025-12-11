

namespace Watchables
{
  /// <summary> 
  /// Base interface for types which wrap an underlying value of type <typeparamref name="T"/>. Value can be retrieved through <see cref="Value"/>. 
  /// </summary>
  public interface IWrapper<out T>
  {

    /// <summary> The underlying value. </summary>
    T Value { get; }

  }
}