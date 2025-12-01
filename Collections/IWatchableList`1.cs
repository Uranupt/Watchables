using System;
using System.Collections.Generic;


namespace Watchables
{ 
  public interface IWatchableList<T> : IWatchable
  { 

    T this[int index] { get; }

    bool IsAutoSorting { get; }

    bool AutoSort(Comparison<T> comparison, object owner = null);
    bool ClearAutoSort(object owner = null);

    int IndexOf(T item);
    int IndexOf(T item, int index);
    int IndexOf(T item, int index, int count);
    int LastIndexOf(T item);
    int LastIndexOf(T item, int index);
    int LastIndexOf(T item, int index, int count);

    bool Exists(Predicate<T> match);
    bool TrueForAll(Predicate<T> match);
    T Find(Predicate<T> match);
    T FindLast(Predicate<T> match);
    List<T> FindAll(Predicate<T> match);
    int FindIndex(Predicate<T> match);
    int FindIndex(int index, Predicate<T> match);
    int FindIndex(int index, int count, Predicate<T> match);
    int FindLastIndex(Predicate<T> match);
    int FindLastIndex(int index, Predicate<T> match);
    int FindLastIndex(int index, int count, Predicate<T> match);

    bool Sort(object owner = null);
    bool Sort(Comparison<T> comparison, object owner = null);
    bool Sort(IComparer<T> comparer, object owner = null);
    bool Sort(int index, int count, IComparer<T> comparer, object owner = null);
    bool Reverse(object owner = null);
    bool Reverse(int index, int count, object owner = null);

  }
}