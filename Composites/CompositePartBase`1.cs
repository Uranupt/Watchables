using System;


namespace Watchables
{
  /// <summary>
  /// The base class for the Part half of the Composite-Part pair pattern of Watchables. Provides common interface and implementation for
  /// sorting priority and removal.
  /// </summary>
  /// <typeparam name="TSelf"> The self-referential type </typeparam>
  public abstract class CompositePartBase<TSelf> : OwnableBase where TSelf : CompositePartBase<TSelf>
  {

    /// <summary> The event raised when the part requests to be removed. </summary>
    public event Action<TSelf> Removed;

    /// <summary> The sorting priority of the part. </summary>
    public CompositePartPriority Priority { get; protected set; }

    protected CompositePartBase(CompositePartPriority priority)
    {
      Priority = priority;
    }

    /// <summary>
    /// Attempts to raise the <see cref="Removed"/> event, instructing any containing composites to remove this part.
    /// </summary>
    /// <param name="owner"> The current owner of the instance, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool Remove(object owner = null)
    {
      if(IsOwned && !CompareToOwner(owner)) { return false; }
      InvokeRemoved();
      return true;
    }

    protected void InvokeRemoved()
    {
      Removed?.Invoke((TSelf)this);
    }

  }
}
