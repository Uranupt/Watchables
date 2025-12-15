using System.Collections.Generic;


namespace Watchables
{
  /// <summary>
  /// The base class for the <see cref="IWatchable"/> half of the Composite-Part pair pattern of Watchables.
  /// Provides common interface and implementation for adding and removing <typeparamref name="TPart"/> instances.
  /// </summary>
  /// <typeparam name="TPart"> The type of part used to compose. </typeparam>
  public abstract class CompositeBase<TPart> : WatchableBase where TPart : CompositePartBase<TPart>
  {

    /// <summary> All of the current <typeparamref name="TPart"/>s composing this instance. </summary>
    protected readonly List<TPart> _parts = new();

    /// <summary> Add a new <typeparamref name="TPart"/> to the <see cref="CompositeBase{TPart}"/>. </summary>
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

    /// <summary> Add a range of new <typeparamref name="TPart"/>s to the <see cref="CompositeBase{TPart}"/>. </summary>
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

    /// <summary> Remove the <typeparamref name="TPart"/> from the <see cref="CompositeBase{TPart}"/>. </summary>
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

    /// <summary> Remove a range of <typeparamref name="TPart"/>s from the <see cref="CompositeBase{TPart}"/>. </summary>
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

    /// <inheritdoc/>
    protected override void BeforeDestroyed()
    {
      while(_parts.Count > 0)
      {
        RemovePartPrivate(_parts[0]);
      }
    }

    /// <summary>
    /// This method defines the sorting behavior of <see cref="_parts"/> when <typeparamref name="TPart"/>s are
    /// added or removed.
    /// </summary>
    protected abstract void Sort();

    /// <summary>
    /// Override this method to define the behavior when a new <paramref name="part"/> is added.
    /// </summary>
    protected virtual void OnPartAdded(TPart part)
    {

    }

    /// <summary>
    /// Override this method to define the behavior when a <paramref name="part"/> is removed.
    /// </summary>
    protected virtual void OnPartRemoved(TPart part)
    {

    }

    private void AddPartPrivate(TPart part)
    {
      if(_parts.Contains(part)) { return; }
      _parts.Add(part);
      OnPartAdded(part);
      part.Removed += RemovePartPrivate;
    }

    private void RemovePartPrivate(TPart part)
    {
      part.Removed -= RemovePartPrivate;
      if(!_parts.Remove(part)) { return; }
      OnPartRemoved(part);
    }

  }
}
