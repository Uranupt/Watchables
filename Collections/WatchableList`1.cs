using System;
using System.Collections.Generic;


namespace Watchables
{
  /// <summary>
  /// An implementation of <see cref="WatchableCollection{T}"/> which represents a <see cref="List{T}"/>.
  /// Provides mirrors of many of the querying and mutation methods in <see cref="List{T}"/>.
  /// </summary>
  public sealed class WatchableList<T> : WatchableCollection<T>, IWatchableList<T>
  {

    private readonly List<T> _list = new();
    private Comparison<T> _sorter;

    /// <inheritdoc/>
    protected override ICollection<T> Collection => _list;

    /// <inheritdoc/>
    public bool IsAutoSorting { get; private set; }

    /// <inheritdoc/>
    public T this[int index] => _list[index];

    /// <summary> Attempts to retrieve or set the item at the given index. </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state when setting. </param>
    public T this[int index, object owner]
    {
      get => this[index];
      set => Set(index, value, owner);
    }

    /// <inheritdoc/>
    public bool AutoSort(Comparison<T> comparison, object owner = null)
    {
      return MutationGuard(() => SetSorter(comparison, true), owner);
    }

    /// <inheritdoc/>
    public bool ClearAutoSort(object owner = null)
    {
      return MutationGuard(() => SetSorter(null, false), owner);
    }

    /// <inheritdoc/>
    public int IndexOf(T item) => _list.IndexOf(item);

    /// <inheritdoc/>
    public int IndexOf(T item, int index) => _list.IndexOf(item, index);

    /// <inheritdoc/>
    public int IndexOf(T item, int index, int count) => _list.IndexOf(item, index, count);

    /// <inheritdoc/>
    public int LastIndexOf(T item) => _list.LastIndexOf(item);

    /// <inheritdoc/>
    public int LastIndexOf(T item, int index) => _list.LastIndexOf(item, index);

    /// <inheritdoc/>
    public int LastIndexOf(T item, int index, int count) => _list.LastIndexOf(item, index, count);

    /// <inheritdoc/>
    public bool Exists(Predicate<T> match) => _list.Exists(match);

    /// <inheritdoc/>
    public bool TrueForAll(Predicate<T> match) => _list.TrueForAll(match);

    /// <inheritdoc/>
    public T Find(Predicate<T> match) => _list.Find(match);

    /// <inheritdoc/>
    public T FindLast(Predicate<T> match) => _list.FindLast(match);

    /// <inheritdoc/>
    public List<T> FindAll(Predicate<T> match) => _list.FindAll(match);

    /// <inheritdoc/>
    public int FindIndex(Predicate<T> match) => _list.FindIndex(match);

    /// <inheritdoc/>
    public int FindIndex(int index, Predicate<T> match) => _list.FindIndex(index, match);

    /// <inheritdoc/>
    public int FindIndex(int index, int count, Predicate<T> match) => _list.FindIndex(index, count, match);

    /// <inheritdoc/>
    public int FindLastIndex(Predicate<T> match) => _list.FindLastIndex(match);

    /// <inheritdoc/>
    public int FindLastIndex(int index, Predicate<T> match) => _list.FindLastIndex(index, match);

    /// <inheritdoc/>
    public int FindLastIndex(int index, int count, Predicate<T> match) => _list.FindLastIndex(index, count, match);

    /// <summary> Attempts to set the <paramref name="item"/> at the provided <paramref name="index"/>. </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool Set(int index, T item, object owner = null)
    {
      if(index < 0) { return false; }
      if(index >= Count)
      {
        return Add(item, owner);
      }
      return MutationGuard(() => _list[index] = item, owner);
    }

    /// <summary> Attempts to insert an <paramref name="item"/> at the provided <paramref name="index"/>. </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool Insert(int index, T item, object owner = null)
    {
      if(index < 0) { return false; }
      if(index >= Count)
      {
        return Add(item, owner);
      }
      return MutationGuard(() => _list.Insert(index, item), owner);
    }

    /// <summary> Attempts to insert a range of <paramref name="items"/> at the provided <paramref name="index"/>. </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool InsertRange(int index, IEnumerable<T> items, object owner = null)
    {
      if(index < 0) { return false; }
      if(index >= Count)
      {
        return AddRange(items, owner);
      }
      return MutationGuard(() => _list.InsertRange(index, items), owner);
    }


    /// <summary> Attempts to remove the item at the provided <paramref name="index"/>. </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool RemoveAt(int index, object owner = null)
    {
      if(index < 0 || index >= Count) { return false; }
      return MutationGuard(() => _list.RemoveAt(index), owner);
    }

    /// <summary> Attempts to remove a range of items starting at the provided <paramref name="index"/>. </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool RemoveRange(int index, int count, object owner = null)
    {
      if(index < 0 || index + count >= Count) { return false; }
      return MutationGuard(() => _list.RemoveRange(index, count), owner);
    }

    /// <inheritdoc/>
    public bool Sort(object owner = null)
    {
      if(IsAutoSorting) { return false; }
      return MutationGuard(_list.Sort, owner);
    }

    /// <inheritdoc/>
    public bool Sort(Comparison<T> comparison, object owner = null)
    {
      if(IsAutoSorting) { return false; }
      return MutationGuard(() => _list.Sort(comparison), owner);
    }

    /// <inheritdoc/>
    public bool Sort(IComparer<T> comparer, object owner = null)
    {
      if(IsAutoSorting) { return false; }
      return MutationGuard(() => _list.Sort(comparer), owner);
    }

    /// <inheritdoc/>
    public bool Sort(int index, int count, IComparer<T> comparer, object owner = null)
    {
      if(IsAutoSorting || index < 0 || index + count >= Count) { return false; }
      return MutationGuard(() => _list.Sort(index, count, comparer), owner);
    }

    /// <inheritdoc/>
    public bool Reverse(object owner = null)
    {
      if(IsAutoSorting) { return false; }
      return MutationGuard(_list.Reverse, owner);
    }

    /// <inheritdoc/>
    public bool Reverse(int index, int count, object owner = null)
    {
      if(IsAutoSorting || index < 0 || index + count >= Count) { return false; }
      return MutationGuard(() => _list.Reverse(index, count), owner);
    }

    /// <inheritdoc/>
    protected override void BeforeChanged()
    {
      if(IsAutoSorting)
      {
        _list.Sort(_sorter);
      }
    }

    private void SetSorter(Comparison<T> comparison, bool useSort)
    {
      _sorter = comparison;
      IsAutoSorting = useSort;
    }

  }
}