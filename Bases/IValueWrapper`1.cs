

namespace Watchables
{
  /// <summary> 
  /// Base interface for Types which wrap an underlying value of Type <typeparamref name="T"/>. Value can be retrieved through <see cref="ToValue"/>. 
  /// </summary>
  public interface IValueWrapper<T>
  {

    /// <summary> Retrieve the underlying value of Type <typeparamref name="T"/>. </summary>
    T ToValue();

  }
}