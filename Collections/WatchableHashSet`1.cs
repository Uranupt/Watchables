using System;
using System.Collections;
using System.Collections.Generic;


namespace Watchables
{
  /// <summary>
  /// An implementation of <see cref="WatchableCollection{T}"/> which represents a <see cref="HashSet{T}"/>.
  /// Provides mirros of many of the querying and mutation methods in <see cref="HashSet{T}"/>.
  /// </summary>
  public sealed class WatchableHashSet<T> : WatchableCollection<T>, IWatchableHashSet<T>
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

    /// <summary> 
    /// Attempts to remove all items from the <see cref="HashSet{T}"/> which appear in the <paramref name="other"/> collection. 
    /// </summary>
    /// <returns> Whether the operation was allowed. </returns>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    public bool ExceptWith(IEnumerable<T> other, object owner = null)
    {
      return MutationGuard(() => _hashSet.ExceptWith(other), owner);
    }

    /// <summary>
    /// Attempts to remove all items from the <see cref="HashSet{T}"/> which do not also appear in the <paramref name="other"/> collection.
    /// </summary>
    /// <returns> Whether the operation was allowed. </returns>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    public bool IntersectWith(IEnumerable<T> other, object owner = null)
    {
      return MutationGuard(() => _hashSet.IntersectWith(other), owner);
    }

    /// <summary>
    /// Attempts to set the contents of the <see cref="HashSet{T}"/> to contain only elements which currently appear in the
    /// <see cref="HashSet{T}"/> or in the <paramref name="other"/> collection, but which do not appear in both.
    /// </summary>
    /// <returns> Whether the operation was allowed. </returns>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    public bool SymmetricExceptWith(IEnumerable<T> other, object owner = null)
    {
      return MutationGuard(() => _hashSet.SymmetricExceptWith(other), owner);
    }

    /// <summary>
    /// Attempts to set the contents of the <see cref="HashSet{T}"/> to contain elements which currently appear in the <see cref="HashSet{T}"/>,
    /// the <paramref name="other"/> collection, or both.
    /// </summary>
    /// <returns> Whether the operation was allowed. </returns>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    public bool UnionWith(IEnumerable<T> other, object owner = null)
    {
      return MutationGuard(() => _hashSet.UnionWith(other), owner);
    }

    /// <summary> 
    /// Attempts to remove all elements from the <see cref="HashSet{T}"/> which <paramref name="match"/> the given <see cref="Predicate{T}"/>.
    /// </summary>
    /// <returns> Whether the operation was allowed. </returns>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    public bool RemoveWhere(Predicate<T> match, object owner = null)
    {
      return MutationGuard(() => _hashSet.RemoveWhere(match), owner);
    }

  }
}