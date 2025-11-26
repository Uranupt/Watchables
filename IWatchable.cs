using System;


namespace Watchables
{
  /// <summary>
  /// Base interface for the Watchable system of subscribable classes. 
  /// Provides the core <see cref="Changed"/> and <see cref="Destroyed"/> events, as well as ownership methods.
  /// </summary>
  public interface IWatchable
  {

    /// <summary>  Whether this instance currently has a defined owner. </summary>
    bool IsOwned { get; }
    /// <summary> Whether this instance has already been destroyed. </summary>
    bool IsDestroyed { get; }
    /// <summary> The event fired when a change occurs. </summary>
    event Action Changed;
    /// <summary> The event fired when this is instance is destroyed. </summary>
    event Action<IWatchable> Destroyed;

    /// <summary> Attempts to clear all values and listeners and mark as destroyed. If an owner exists, it is required. </summary>
    bool Destroy(object owner = null);

    /// <summary> Attempts to set the this instance's owner. Only succeeds if there is no current owner. </summary>
    bool SetOwner(object owner);

    /// <summary> Attempts to clear the this instance's owner. Requires the current owner to be passed. </summary>
    bool ClearOwner(object owner);

    /// <summary> Compares the given object to the instance's owner. Always returns false if there is no owner. </summary>
    bool CompareToOwner(object owner);

  }
}
