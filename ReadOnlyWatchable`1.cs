using System;

namespace Watchables
{
  /// <summary>
  /// Read-only wrapper for <see cref="IWatchable{T}"/> instances.
  /// </summary>
  public sealed class ReadOnlyWatchable<T> : IWatchable<T>
  {

    private readonly IWatchable<T> _baseValue;

    public bool IsOwned => _baseValue.IsOwned;
    public bool IsDestroyed => _baseValue.IsDestroyed;

    public event Action Changed;
    public event Action<IWatchable> Destroyed;

    public static implicit operator T(ReadOnlyWatchable<T> watchable) => watchable.ToValue();

    public ReadOnlyWatchable(IWatchable<T> baseValue)
    {
      _baseValue = baseValue;
      _baseValue.Changed += OnChanged;
      _baseValue.Destroyed += OnDestroyed;
    }

    /// <summary> Unused -- Read-only implementation </summary>
    public bool ClearOwner(object owner) => false;
    /// <summary> Unused -- Read-only implementation </summary>
    public bool Destroy(object owner = null) => false;
    /// <summary> Unused -- Read-only implementation </summary>
    public bool SetOwner(object owner) => false;

    public T ToValue() => _baseValue.ToValue();
    /// <summary> Returns a string representation of the underlying value. </summary>
    public override string ToString() => _baseValue.ToString();

    private void OnChanged() => Changed?.Invoke();
    
    private void OnDestroyed(IWatchable sender)
    {
      if(sender != _baseValue) { return; }
      _baseValue.Changed -= OnChanged;
      _baseValue.Destroyed -= OnDestroyed;
      Changed = null;
      Destroyed?.Invoke(this);
      Destroyed = null;
    }

  }
}
