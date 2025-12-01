using System.Collections;
using System.Collections.Generic;


namespace Watchables
{
  public abstract class CompositeCollection<T> : WatchableCollection, IEnumerable<T>
  {

    private readonly HashSet<IEnumerable<T>> _sources = new();
    private readonly HashSet<IWatchable> _requiredSources = new();

    protected abstract ICollection<T> Collection { get; }

    public override int Count => Collection.Count;

    public bool Contains(T item) => Collection.Contains(item);

    public bool Add(IEnumerable<T> source, bool required, object owner = null)
    {
      return MutationGuard(() => AddSourcePrivate(source, required), owner);
    }

    public bool Remove(IEnumerable<T> source, object owner = null)
    {
      return MutationGuard(() => RemoveSourcePrivate(source), owner);
    }

    public bool Clear(object owner = null)
    {
      return MutationGuard(ClearValue, owner);
    }

    public IEnumerator<T> GetEnumerator() => Collection.GetEnumerator();

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

    protected override IEnumerator GetNonGenericEnumerator() => GetEnumerator();

    protected void Evaluate()
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
      Evaluate();
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