using System;

namespace Watchables
{
  /// <summary>
  /// Read-only wrapper for <see cref="IWatchable{T}"/> instances which has identical value, lifetime, and change propagation.
  /// </summary>
  public sealed class ReadOnlyWatchable<T> : IWatchable<T>
  {

    private IWatchable<T> _baseValue;

    /// <summary> Always <see langword="false"/> -- Read-only implementation which cannot be owned. </summary>
    public bool IsOwned => false;

    /// <inheritdoc/>
    public bool IsDestroyed => _baseValue == null || _baseValue.IsDestroyed;

    /// <inheritdoc/>
    public T Value => _baseValue != null ? _baseValue.Value : default;

    /// <inheritdoc/>
    public event Action Changed;
    /// <inheritdoc/>
    public event Action<IWatchable> Destroyed;

    public static implicit operator T(ReadOnlyWatchable<T> watchable) => watchable.Value;

    public ReadOnlyWatchable(IWatchable<T> baseValue)
    {
      _baseValue = baseValue;
      _baseValue.Changed += OnChanged;
      _baseValue.Destroyed += OnDestroyed;
    }

    /// <summary> Unused -- Read-only implementation which only propagates state and value from a contained <see cref="IWatchable{T}"/>. </summary>
    /// <returns> <see langword="false"/> </returns>
    public bool ClearOwner(object owner) => false;
    /// <summary> Unused -- Read-only implementation which only propagates state and value from a contained <see cref="IWatchable{T}"/>. </summary>
    /// <returns> <see langword="false"/> </returns>
    public bool Destroy(object owner = null) => false;
    /// <summary> Unused -- Read-only implementation which only propagates state and value from a contained <see cref="IWatchable{T}"/>. </summary>
    /// <returns> <see langword="false"/> </returns>
    public bool SetOwner(object owner) => false;
    /// <summary> Unused -- Read-only implementation which only propagates state and value from a contained <see cref="IWatchable{T}"/>. </summary>
    /// <returns> <see langword="false"/> </returns>
    public bool CompareToOwner(object owner) => false;

    /// <summary> Returns a string representation of the underlying value. </summary>
    public override string ToString() => _baseValue.ToString();

    private void OnChanged() => Changed?.Invoke();
    
    private void OnDestroyed(IWatchable sender)
    {
      if(sender != _baseValue) { return; }
      _baseValue.Changed -= OnChanged;
      _baseValue.Destroyed -= OnDestroyed;
      _baseValue = null;
      Changed = null;
      Destroyed?.Invoke(this);
      Destroyed = null;
    }

  }
}
