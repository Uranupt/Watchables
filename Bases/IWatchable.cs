using System;


namespace Watchables
{
  /// <summary>
  /// Base interface for the Watchable system of subscribable classes. 
  /// Provides the core <see cref="Changed"/> and <see cref="Destroyed"/> events.
  /// Inherits <see cref="IOwnable"/>.
  /// </summary>
  public interface IWatchable : IOwnable
  {

    /// <summary> Whether this instance has already been destroyed. </summary>
    bool IsDestroyed { get; }
    /// <summary> The event fired when a change occurs. </summary>
    event Action Changed;
    /// <summary> The event fired when this instance is destroyed. </summary>
    event Action<IWatchable> Destroyed;

    /// <summary> Attempts to clear all values and listeners and mark as destroyed. </summary>
    bool Destroy(object owner = null);

  }
}
