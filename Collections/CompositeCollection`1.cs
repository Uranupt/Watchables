using System.Collections;
using System.Collections.Generic;


namespace Watchables
{
  /// <summary>
  /// Base class extension of <see cref="WatchableCollection"/> for collections built from other <see cref="IEnumerable{T}"/> instances.
  /// Implements <see cref="IEnumerable{T}"/>
  /// </summary>
  public abstract class CompositeCollection<T> : WatchableCollection, IEnumerable<T>
  {

    private readonly HashSet<IEnumerable<T>> _sources = new();
    private readonly HashSet<IWatchable> _requiredSources = new();

    /// <summary> The <see cref="ICollection{T}"/> instance this wraps and operates on. </summary>
    protected abstract ICollection<T> Collection { get; }

    /// <inheritdoc/>
    public override int Count => Collection.Count;

    /// <summary> Determines whether the collection contains the provided value. </summary>
    public bool Contains(T item) => Collection.Contains(item);

    /// <summary> Attempts to add the provided source to the collection. </summary>
    /// <param name="owner"> The current owner of the collection, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool Add(IEnumerable<T> source, bool required, object owner = null)
    {
      return MutationGuard(() => AddSourcePrivate(source, required), owner);
    }


    /// <summary> Attempts to remove the provided source from the collection. </summary>
    /// <param name="owner"> The current owner of the collection, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool Remove(IEnumerable<T> source, object owner = null)
    {
      return MutationGuard(() => RemoveSourcePrivate(source), owner);
    }

    /// <summary> Attempts to clear all sources from the collection. </summary>
    /// <param name="owner"> The current owner of the collection, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool Clear(object owner = null)
    {
      return MutationGuard(ClearValue, owner);
    }

    /// <inheritdoc/>
    public IEnumerator<T> GetEnumerator() => Collection.GetEnumerator();

    /// <inheritdoc/>
    protected override void ClearValue()
    {
      foreach(IEnumerable<T> source in _sources)
      {
        if(source is IWatchable watchable)
        {
          Unregister(watchable);
        }
      }
      Collection.Clear();
      _sources.Clear();
    }

    /// <inheritdoc/>
    protected override IEnumerator GetNonGenericEnumerator() => GetEnumerator();

    private void Evaluate()
    {
      Collection.Clear();
      foreach(IEnumerable<T> source in _sources)
      {
        foreach(T item in source)
        {
          Collection.Add(item);
        }
      }
    }

    private void AddSourcePrivate(IEnumerable<T> source, bool required)
    {
      _sources.Add(source);
      if(source is IWatchable watchable)
      {
        Register(watchable);
        if(required)
        {
          _requiredSources.Add(watchable);
        }
      }
      Evaluate();
    }

    private void RemoveSourcePrivate(IEnumerable<T> source)
    {
      _sources.Remove(source);
      if(source is IWatchable watchable)
      {
        Unregister(watchable);
        _requiredSources.Remove(watchable);
      }
      Evaluate();
    }

    private void Register(IWatchable watchable)
    {
      Unregister(watchable);
      watchable.Changed += OnSourceChanged;
      watchable.Destroyed += OnSourceDestroyed;
    }

    private void Unregister(IWatchable watchable)
    {
      watchable.Changed -= OnSourceChanged;
      watchable.Destroyed -= OnSourceDestroyed;
    }

    private void OnSourceChanged()
    {
      if(IsDestroyed) { return; }
      InvokeChanged();
    }

    private void OnSourceDestroyed(IWatchable watchable)
    {
      if(_requiredSources.Contains(watchable))
      {
        DestroyProtected();
      }
      else
      {
        RemoveSourcePrivate(watchable as IEnumerable<T>);
        InvokeChanged();
      }
    }

  }
}