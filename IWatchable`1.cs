

namespace Watchables
{
  /// <summary>
  /// Base generic interface for the Watchable system which combines <see cref="IWatchable"/> and <see cref="IValueWrapper{T}"/>.
  /// </summary>
  public interface IWatchable<T> : IWatchable, IValueWrapper<T>
  {


  }
}