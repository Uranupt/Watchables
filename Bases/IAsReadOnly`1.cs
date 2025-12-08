

namespace Watchables
{
  /// <summary>
  /// Extension of <see cref="IWatchable{T}"/> providing the <see cref="AsReadOnly"/> method to attain a read-only view of the instance
  /// with an identical value, lifetime, and change propagation.
  /// </summary>
  public interface IAsReadOnly<T> : IWatchable<T>
  {

    /// <summary> Returns a read-only view of this instance. </summary>
    ReadOnlyWatchable<T> AsReadOnly();

  }
}
