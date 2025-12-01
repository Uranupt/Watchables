using System.Collections.Generic;


namespace Watchables
{
  public sealed class CompositeHashSet<T> : CompositeCollection<T>, IWatchableHashSet<T>
  {

    private readonly HashSet<T> _hashSet = new();

    protected override ICollection<T> Collection => _hashSet;

    public bool IsProperSubsetOf(IEnumerable<T> other) => _hashSet.IsProperSubsetOf(other);
    public bool IsProperSupersetOf(IEnumerable<T> other) => _hashSet.IsProperSupersetOf(other);
    public bool IsSubsetOf(IEnumerable<T> other) => _hashSet.IsSubsetOf(other);
    public bool IsSupersetOf(IEnumerable<T> other) => _hashSet.IsSupersetOf(other);
    public bool Overlaps(IEnumerable<T> other) => _hashSet.Overlaps(other);
    public bool SetEquals(IEnumerable<T> other) => _hashSet.SetEquals(other);

  }
}