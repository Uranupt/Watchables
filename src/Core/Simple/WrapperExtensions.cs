

namespace Watchables
{
  /// <summary>
  /// Extension methods for quickly providing an <see cref="IWrapper{T}"/> for a value.
  /// </summary>
  public static class WrapperExtensions
  {

    /// <summary> Creates a new <see cref="ReadOnlyWrapper{T}"/> containing the <paramref name="value"/>. </summary>
    public static ReadOnlyWrapper<T> Wrap<T>(this T value) => new ReadOnlyWrapper<T>(value);

    /// <summary> 
    /// Creates a new <see cref="BasicWrapper{T}"/> containing the <paramref name="value"/>. 
    /// Can automatically assign the <paramref name="owner"/> as well.
    /// </summary>
    public static BasicWrapper<T> WrapMutable<T>(this T value, object owner = null)
    {
      BasicWrapper<T> resl = new(value);
      if(owner != null)
      {
        resl.SetOwner(owner);
      }
      return resl;
    }

  }
}