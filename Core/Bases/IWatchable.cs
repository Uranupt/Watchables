using System;


namespace Watchables
{
  /// <summary>
  /// Base interface for the Watchable system. Provides the core <see cref="Changed"/> and <see cref="Destroyed"/> events.
  /// </summary>
  /// <remarks> Inherits <see cref="IOwnable"/>. </remarks>
  public interface IWatchable : IOwnable
  {

    /// <summary> Whether this instance has been destroyed. </summary>
    bool IsDestroyed { get; }
    /// <summary> The event fired when a change occurs. </summary>
    event Action Changed;
    /// <summary> The event fired when this instance is destroyed. </summary>
    event Action<IWatchable> Destroyed;

    /// <summary> Attempts to clear all values and listeners and mark as destroyed. Requires the current <paramref name="owner"/> if one exists. </summary>
    /// <returns> Whether the operation was allowed. </returns>
    bool Destroy(object owner = null);

  }
}
