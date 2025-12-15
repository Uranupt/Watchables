using System.Collections.Generic;


namespace Watchables
{
  /// <summary>
  /// An implementation of <see cref="CompositeCollection{T}"/> which represents a <see cref="HashSet{T}"/> constructed from
  /// multiple <see cref="IEnumerable{T}"/> sources. Provides mirrors of many of the querying methods in <see cref="HashSet{T}"/>
  /// </summary>
  public sealed class CompositeHashSet<T> : CompositeCollection<T>, IWatchableHashSet<T>
  {

    private readonly HashSet<T> _hashSet = new();

    /// <inheritdoc/>
    protected override ICollection<T> Collection => _hashSet;

    /// <inheritdoc/>
    public bool IsProperSubsetOf(IEnumerable<T> other) => _hashSet.IsProperSubsetOf(other);

    /// <inheritdoc/>
    public bool IsProperSupersetOf(IEnumerable<T> other) => _hashSet.IsProperSupersetOf(other);

    /// <inheritdoc/>
    public bool IsSubsetOf(IEnumerable<T> other) => _hashSet.IsSubsetOf(other);

    /// <inheritdoc/>
    public bool IsSupersetOf(IEnumerable<T> other) => _hashSet.IsSupersetOf(other);

    /// <inheritdoc/>
    public bool Overlaps(IEnumerable<T> other) => _hashSet.Overlaps(other);

    /// <inheritdoc/>
    public bool SetEquals(IEnumerable<T> other) => _hashSet.SetEquals(other);

  }
}