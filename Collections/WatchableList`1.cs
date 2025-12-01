using System;
using System.Collections.Generic;


namespace Watchables
{
  public sealed class WatchableList<T> : WatchableCollection<T>, IWatchableList<T>
  {

    private readonly List<T> _list = new();
    private Comparison<T> _sorter;

    protected override ICollection<T> Collection => _list;

    public bool IsAutoSorting { get; private set; }

    public T this[int index] => _list[index];

    public T this[int index, object owner]
    {
      get => this[index];
      set => Set(index, value, owner);
    }

    public bool AutoSort(Comparison<T> comparison, object owner = null)
    {
      return MutationGuard(() => SetSorter(comparison, true), owner);
    }

    public bool ClearAutoSort(object owner = null)
    {
      return MutationGuard(() => SetSorter(null, false), owner);
    }

    public int IndexOf(T item) => _list.IndexOf(item);
    public int IndexOf(T item, int index) => _list.IndexOf(item, index);
    public int IndexOf(T item, int index, int count) => _list.IndexOf(item, index, count);
    public int LastIndexOf(T item) => _list.LastIndexOf(item);
    public int LastIndexOf(T item, int index) => _list.LastIndexOf(item, index);
    public int LastIndexOf(T item, int index, int count) => _list.LastIndexOf(item, index, count);
    public bool Exists(Predicate<T> match) => _list.Exists(match);
    public bool TrueForAll(Predicate<T> match) => _list.TrueForAll(match);
    public T Find(Predicate<T> match) => _list.Find(match);
    public T FindLast(Predicate<T> match) => _list.FindLast(match);
    public List<T> FindAll(Predicate<T> match) => _list.FindAll(match);
    public int FindIndex(Predicate<T> match) => _list.FindIndex(match);
    public int FindIndex(int index, Predicate<T> match) => _list.FindIndex(index, match);
    public int FindIndex(int index, int count, Predicate<T> match) => _list.FindIndex(index, count, match);
    public int FindLastIndex(Predicate<T> match) => _list.FindLastIndex(match);
    public int FindLastIndex(int index, Predicate<T> match) => _list.FindLastIndex(index, match);
    public int FindLastIndex(int index, int count, Predicate<T> match) => _list.FindLastIndex(index, count, match);

    public bool Set(int index, T item, object owner = null)
    {
      if(index < 0) { return false; }
      if(index >= Count)
      {
        return Add(item, owner);
      }
      return MutationGuard(() => _list[index] = item, owner);
    }

    public bool Insert(int index, T item, object owner = null)
    {
      if(index < 0) { return false; }
      if(index >= Count)
      {
        return Add(item, owner);
      }
      return MutationGuard(() => _list.Insert(index, item), owner);
    }

    public bool InsertRange(int index, IEnumerable<T> items, object owner = null)
    {
      if(index < 0) { return false; }
      if(index >= Count)
      {
        return AddRange(items, owner);
      }
      return MutationGuard(() => _list.InsertRange(index, items), owner);
    }

    public bool RemoveAt(int index, object owner = null)
    {
      if(index < 0 || index >= Count) { return false; }
      return MutationGuard(() => _list.RemoveAt(index), owner);
    }

    public bool RemoveRange(int index, int count, object owner = null)
    {
      if(index < 0 || index + count >= Count) { return false; }
      return MutationGuard(() => _list.RemoveRange(index, count), owner);
    }

    public bool Sort(object owner = null)
    {
      if(IsAutoSorting) { return false; }
      return MutationGuard(_list.Sort, owner);
    }

    public bool Sort(Comparison<T> comparison, object owner = null)
    {
      if(IsAutoSorting) { return false; }
      return MutationGuard(() => _list.Sort(comparison), owner);
    }

    public bool Sort(IComparer<T> comparer, object owner = null)
    {
      if(IsAutoSorting) { return false; }
      return MutationGuard(() => _list.Sort(comparer), owner);
    }

    public bool Sort(int index, int count, IComparer<T> comparer, object owner = null)
    {
      if(IsAutoSorting || index < 0 || index + count >= Count) { return false; }
      return MutationGuard(() => _list.Sort(index, count, comparer), owner);
    }

    public bool Reverse(object owner = null)
    {
      if(IsAutoSorting) { return false; }
      return MutationGuard(_list.Reverse, owner);
    }

    public bool Reverse(int index, int count, object owner = null)
    {
      if(IsAutoSorting || index < 0 || index + count >= Count) { return false; }
      return MutationGuard(() => _list.Reverse(index, count), owner);
    }

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