using System;
using System.Collections;
using System.Collections.Generic;


namespace Watchables
{
  public sealed class WatchableHashSet<T> : WatchableCollection<T>, IWatchableHashSet<T>
  {

    private readonly HashSet<T> _hashSet = new();

    protected override ICollection<T> Collection => _hashSet;

    public bool IsProperSubsetOf(IEnumerable<T> other) => _hashSet.IsProperSubsetOf(other);
    public bool IsProperSupersetOf(IEnumerable<T> other) => _hashSet.IsProperSupersetOf(other);
    public bool IsSubsetOf(IEnumerable<T> other) => _hashSet.IsSubsetOf(other);
    public bool IsSupersetOf(IEnumerable<T> other) => _hashSet.IsSupersetOf(other);
    public bool Overlaps(IEnumerable<T> other) => _hashSet.Overlaps(other);
    public bool SetEquals(IEnumerable<T> other) => _hashSet.SetEquals(other);

    public bool ExceptWith(IEnumerable<T> other, object owner = null)
    {
      return MutationGuard(() => _hashSet.ExceptWith(other), owner);
    }

    public bool IntersectWith(IEnumerable<T> other, object owner = null)
    {
      return MutationGuard(() => _hashSet.IntersectWith(other), owner);
    }

    public bool SymmetricExceptWith(IEnumerable<T> other, object owner = null)
    {
      return MutationGuard(() => _hashSet.SymmetricExceptWith(other), owner);
    }

    public bool UnionWith(IEnumerable<T> other, object owner = null)
    {
      return MutationGuard(() => _hashSet.UnionWith(other), owner);
    }

    public bool RemoveWhere(Predicate<T> match, object owner = null)
    {
      return MutationGuard(() => _hashSet.RemoveWhere(match), owner);
    }

  }
}