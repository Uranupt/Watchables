using System;


namespace Watchables
{
  public sealed class CompositeStep<T> : IValueWrapper<T>, IOwnable where T : unmanaged
  {

    private object _owner;

    public event Action<CompositeStep<T>> Removed;
    public CompositeOperation Operation { get; private set; }
    public IValueWrapper<T> Value { get; private set; }
    public bool IsOwned => _ownwer != null;
    public bool IsValid => Value != null;

    private CompositeStep(CompositeOperation op, IValueWrapper<T> value, object owner)
    {
      Operation = op;
      Value = value;
      _owner = owner;
      if(Value is IWatchable)
      {
        (Value as IWatchable).Destroyed += OnValueDestroyed;
      }
    }

    public static implicit operator T(CompositeStep<T> step) => step.ToValue();

    public static CompositeStep<T> Force(IValueWrapper<T> value, object owner = null) => new(CompositeOperation.Force, value, owner);
    public static CompositeStep<T> SetFinal(IValueWrapper<T> value, object owner = null) => new(CompositeOperation.SetFinal, value, owner);
    public static CompositeStep<T> SetBase(IValueWrapper<T> value, object owner = null) => new(CompositeOperation.SetBase, value, owner);
    public static CompositeStep<T> Add(IValueWrapper<T> value, object owner = null) => new(CompositeOperation.Add, value, owner);
    public static CompositeStep<T> Subtract(IValueWrapper<T> value, object owner = null) => new(CompositeOperation.Subtract, value, owner);
    public static CompositeStep<T> Multiply(IValueWrapper<T> value, object owner = null) => new(CompositeOperation.Multiply, value, owner);
    public static CompositeStep<T> Divide(IValueWrapper<T> value, object owner = null) => new(CompositeOperation.Divide, value, owner);
    public static CompositeStep<T> Minimum(IValueWrapper<T> value, object owner = null) => new(CompositeOperation.Minimum, value, owner);
    public static CompositeStep<T> Maximum(IValueWrapper<T> value, object owner = null) => new(CompositeOperation.Maximum, value, owner);

    public bool SetOwner(object owner)
    {

    }

    public bool ClearOwner(object owner)
    {

    }

    public bool CompareToOwner(object owner)
    {

    }

    public bool Remove(object owner = null)
    {
      if(IsOwned && !CompareToOwner(owner)) { return false; }
      Removed.Invoke(this);
      return true;
    }

    public T ToValue() => Value;

    private void OnValueDestroyed(IWatchable value)
    {
      value.Destroyed -= OnValueDestroyed;
      Remove(_owner);
      Value = null;
    }

  }
}