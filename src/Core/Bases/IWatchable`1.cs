

namespace Watchables
{
  /// <summary>
  /// Base generic interface for the Watchable system which combines <see cref="IWatchable"/> and <see cref="IWrapper{T}"/>.
  /// </summary>
  public interface IWatchable<out T> : IWatchable, IWrapper<T>
  {


  }
}