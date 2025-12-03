using System;


namespace Watchables
{
  public sealed class CompositeStep<T> : OwnableBase, IValueWrapper<T> where T : unmanaged
  {

    public event Action<CompositeStep<T>> Removed;
    public CompositeOperation Operation { get; private set; }
    public IValueWrapper<T> Value { get; private set; }
    public StepPriority Priority { get; private set; }
    public bool IsValid => Value != null;

    internal CompositeStep(CompositeOperation op, IValueWrapper<T> value, StepPriority priority)
    {
      Operation = op;
      Value = value;
      Priority = priority;
      if(Value is IWatchable)
      {
        (Value as IWatchable).Destroyed += OnValueDestroyed;
      }
    }

    public static implicit operator T(CompositeStep<T> step) => step.ToValue();

    public static CompositeStep<T> Force(IValueWrapper<T> value, object owner = null, StepPriority priority = StepPriority.None) 
      => new(CompositeOperation.Force, value, owner, priority);

    public static CompositeStep<T> SetFinal(IValueWrapper<T> value, object owner = null, StepPriority priority = StepPriority.None) 
      => new(CompositeOperation.SetFinal, value, owner, priority);

    public static CompositeStep<T> SetBase(IValueWrapper<T> value, object owner = null, StepPriority priority = StepPriority.None) 
      => new(CompositeOperation.SetBase, value, owner, priority);

    public static CompositeStep<T> Add(IValueWrapper<T> value, object owner = null, StepPriority priority = StepPriority.None) 
      => new(CompositeOperation.Add, value, owner, priority);

    public static CompositeStep<T> Subtract(IValueWrapper<T> value, object owner = null, StepPriority priority = StepPriority.None) 
      => new(CompositeOperation.Subtract, value, owner, priority);

    public static CompositeStep<T> Multiply(IValueWrapper<T> value, object owner = null, StepPriority priority = StepPriority.None) 
      => new(CompositeOperation.Multiply, value, owner, priority);

    public static CompositeStep<T> Divide(IValueWrapper<T> value, object owner = null, StepPriority priority = StepPriority.None) 
      => new(CompositeOperation.Divide, value, owner, priority);

    public static CompositeStep<T> Minimum(IValueWrapper<T> value, object owner = null, StepPriority priority = StepPriority.None) 
      => new(CompositeOperation.Minimum, value, owner, priority);

    public static CompositeStep<T> Maximum(IValueWrapper<T> value, object owner = null, StepPriority priority = StepPriority.None) 
      => new(CompositeOperation.Maximum, value, owner, priority);

    public override bool SetOwner(object owner)
    {
      if(!IsValid) { return false; }
      return base.SetOwner(owner);
    }

    public override bool ClearOwner(object owner)
    {
      if(!IsValid) { return false; }
      return base.ClearOwner(owner);
    }

    public bool Remove(object owner = null)
    {
      if(IsOwned && !CompareToOwner(owner)) { return false; }
      Removed?.Invoke(this);
      return true;
    }

    public T ToValue() => Value.ToValue();

    private void OnValueDestroyed(IWatchable value)
    {
      value.Destroyed -= OnValueDestroyed;
      Remove(_owner);
      Value = null;
    }

  }
}