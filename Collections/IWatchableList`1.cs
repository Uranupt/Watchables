using System;
using System.Collections.Generic;


namespace Watchables
{
  /// <summary>
  /// Common interface for <see cref="WatchableCollection"/> implementations which wrap a <see cref="List{T}"/>.
  /// Provides mirrors for many of the querying methods in <see cref="List{T}"/>
  /// </summary>
  public interface IWatchableList<T> : IWatchable
  { 

    /// <summary> Attempts to retrieve the item at the provided <paramref name="index"/>. </summary>
    T this[int index] { get; }

    /// <summary> Whether the list is being automatically sorted when a change occurs. </summary>
    bool IsAutoSorting { get; }

    /// <summary> Attempts to set the <see cref="Comparison{T}"/> used to automatically sort the list. </summary>
    /// <param name="comparison"> The comparison to sort the list with. </param>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    bool AutoSort(Comparison<T> comparison, object owner = null);

    /// <summary> Attempts to clear any currently used auto sorting by a <see cref="Comparison{T}"/>. </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    bool ClearAutoSort(object owner = null);

    /// <inheritdoc cref="List{T}.IndexOf(T)"/>
    int IndexOf(T item);

    /// <inheritdoc cref="List{T}.IndexOf(T, int)"/>
    int IndexOf(T item, int index);

    /// <inheritdoc cref="List{T}.IndexOf(T, int, int)"/>
    int IndexOf(T item, int index, int count);

    /// <inheritdoc cref="List{T}.LastIndexOf(T)"/>
    int LastIndexOf(T item);

    /// <inheritdoc cref="List{T}.LastIndexOf(T, int)"/>
    int LastIndexOf(T item, int index);

    /// <inheritdoc cref="List{T}.LastIndexOf(T, int, int)"/>
    int LastIndexOf(T item, int index, int count);

    /// <inheritdoc cref="List{T}.Exists(Predicate{T})"/>
    bool Exists(Predicate<T> match);

    /// <inheritdoc cref="List{T}.TrueForAll(Predicate{T})"/>
    bool TrueForAll(Predicate<T> match);

    /// <inheritdoc cref="List{T}.Find(Predicate{T})"/>
    T Find(Predicate<T> match);

    /// <inheritdoc cref="List{T}.FindLast(Predicate{T})"/>
    T FindLast(Predicate<T> match);

    /// <inheritdoc cref="List{T}.FindAll(Predicate{T})"/>
    List<T> FindAll(Predicate<T> match);

    /// <inheritdoc cref="List{T}.FindIndex(Predicate{T})"/>
    int FindIndex(Predicate<T> match);

    /// <inheritdoc cref="List{T}.FindIndex(int, Predicate{T})"/>
    int FindIndex(int index, Predicate<T> match);

    /// <inheritdoc cref="List{T}.FindIndex(int, int, Predicate{T})"/>
    int FindIndex(int index, int count, Predicate<T> match);

    /// <inheritdoc cref="List{T}.FindLastIndex(Predicate{T})"/>
    int FindLastIndex(Predicate<T> match);

    /// <inheritdoc cref="List{T}.FindLastIndex(int, Predicate{T})"/>
    int FindLastIndex(int index, Predicate<T> match);

    /// <inheritdoc cref="List{T}.FindLastIndex(int, int, Predicate{T})"/>
    int FindLastIndex(int index, int count, Predicate<T> match);

    /// <summary> 
    /// Attemtps to sort the list using the default sorter. Will fail if <see cref="IsAutoSorting"/> is true.
    /// </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    bool Sort(object owner = null);

    /// <summary> 
    /// Attemtps to sort the list using the provided <see cref="Comparison{T}"/>. Will fail if <see cref="IsAutoSorting"/> is true.
    /// </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    bool Sort(Comparison<T> comparison, object owner = null);

    /// <summary> 
    /// Attemtps to sort the list using the provided <see cref="IComparer{T}"/>. Will fail if <see cref="IsAutoSorting"/> is true.
    /// </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    bool Sort(IComparer<T> comparer, object owner = null);

    /// <summary> 
    /// Attemtps to sort the list within the given range using the provided <see cref="IComparer{T}"/>.
    /// Will fail if <see cref="IsAutoSorting"/> is true.
    /// </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    bool Sort(int index, int count, IComparer<T> comparer, object owner = null);

    /// <summary> 
    /// Attemtps to reverse order of the list. Will fail if <see cref="IsAutoSorting"/> is true.
    /// </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    bool Reverse(object owner = null);

    /// <summary> 
    /// Attemtps to reverse order of the list within a given range. Will fail if <see cref="IsAutoSorting"/> is true.
    /// </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    bool Reverse(int index, int count, object owner = null);

  }
}