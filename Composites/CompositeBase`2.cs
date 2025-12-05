using System.Collections.Generic;


namespace Watchables
{
  public abstract class CompositeBase<TValue, TPart> : NestedWatchable<TValue> where TPart : CompositePartBase<TValue, TPart>
  {

    protected readonly List<TPart> _parts = new();

    public void AddPart(TPart part)
    {
      MutationGuard(
        () =>
        {
          AddPartPrivate(part);
          Sort();
        }
      );
    }

    public void AddParts(IEnumerable<TPart> parts)
    {
      MutationGuard(
        () =>
        {
          foreach(TPart part in parts)
          {
            AddPartPrivate(part);
          }
          Sort();
        }
      );
    }

    public void RemovePart(TPart part)
    {
      MutationGuard(
        () =>
        {
          RemovePartPrivate(part);
          Sort();
        }
      );
    }

    public void RemoveParts(IEnumerable<TPart> parts)
    {
      MutationGuard(
        () =>
        {
          foreach(TPart part in parts)
          {
            RemovePartPrivate(part);
          }
          Sort();
        }
      );
    }

    protected override bool CheckFatalDestruction(IWatchable dependency) => false;

    protected override void OnDestroyed(IWatchable self)
    {
      while(_parts.Count > 0)
      {
        RemovePartPrivate(_parts[0]);
      }
    }

    protected override void OnNonFatalDestruction(IWatchable dependency)
    {
      for(int i = 0; i < _parts.Count; i++)
      {
        if(_parts[i].Value == dependency)
        {
          RemovePartPrivate(_parts[i]);
          return;
        }
      }
    }

    protected abstract void Sort();

    private void AddPartPrivate(TPart part)
    {
      if(_parts.Contains(part)) { return; }
      _parts.Add(part);
      if(part.Value is IWatchable)
      {
        Register(part.Value as IWatchable);
      }
      part.Removed += RemovePartPrivate;
    }

    private void RemovePartPrivate(TPart part)
    {
      part.Removed -= RemovePartPrivate;
      if(!_parts.Remove(part)) { return; }
      if(part.Value is IWatchable)
      {
        Unregister(part.Value as IWatchable);
      }
    }

  }
}
