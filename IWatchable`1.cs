

namespace Watchables
{
  /// <summary>
  /// Generic extension interface of <see cref="IWatchable"/>
  /// </summary>
  public interface IWatchable<T> : IWatchable
  {

    /// <summary> Retrieve the underlying value of Type <typeparamref name="T"/>. </summary>
    T ToValue();

  }
}