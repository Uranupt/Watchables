using System.Collections.Generic;


namespace Watchables
{
  /// <summary>
  /// Common interface for <see cref="WatchableCollection"/> implementations which wrap a <see cref="HashSet{T}"/>.
  /// Provides mirrors of many of the querying methods in <see cref="HashSet{T}"/>
  /// </summary>
  public interface IWatchableHashSet<T> : IWatchable
  {

    /// <inheritdoc cref="HashSet{T}.IsProperSubsetOf(IEnumerable{T})"/>
    bool IsProperSubsetOf(IEnumerable<T> other);

    /// <inheritdoc cref="HashSet{T}.IsProperSupersetOf(IEnumerable{T})"/>
    bool IsProperSupersetOf(IEnumerable<T> other);

    /// <inheritdoc cref="HashSet{T}.IsSubsetOf(IEnumerable{T})"/>
    bool IsSubsetOf(IEnumerable<T> other);

    /// <inheritdoc cref="HashSet{T}.IsSupersetOf(IEnumerable{T})"/>
    bool IsSupersetOf(IEnumerable<T> other);

    /// <inheritdoc cref="HashSet{T}.Overlaps(IEnumerable{T})"/>
    bool Overlaps(IEnumerable<T> other);

    /// <inheritdoc cref="HashSet{T}.SetEquals(IEnumerable{T})"/>
    bool SetEquals(IEnumerable<T> other);

  }
}