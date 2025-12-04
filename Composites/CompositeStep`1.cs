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

  public static class CompositeStep
  {

    public static CompositeStep<ushort> New(CompositeOperation operation, IValueWrapper<ushort> value, StepPriority priority = StepPriority.None)
      => new CompositeStep<ushort>(operation, value, priority);
    public static CompositeStep<uint> New(CompositeOperation operation, IValueWrapper<uint> value, StepPriority priority = StepPriority.None)
      => new CompositeStep<uint>(operation, value, priority);
    public static CompositeStep<ulong> New(CompositeOperation operation, IValueWrapper<ulong> value, StepPriority priority = StepPriority.None)
      => new CompositeStep<ulong>(operation, value, priority);
    public static CompositeStep<short> New(CompositeOperation operation, IValueWrapper<short> value, StepPriority priority = StepPriority.None)
      => new CompositeStep<short>(operation, value, priority);
    public static CompositeStep<int> New(CompositeOperation operation, IValueWrapper<int> value, StepPriority priority = StepPriority.None)
      => new CompositeStep<int>(operation, value, priority);
    public static CompositeStep<long> New(CompositeOperation operation, IValueWrapper<long> value, StepPriority priority = StepPriority.None)
      => new CompositeStep<long>(operation, value, priority);
    public static CompositeStep<decimal> New(CompositeOperation operation, IValueWrapper<decimal> value, StepPriority priority = StepPriority.None)
      => new CompositeStep<decimal>(operation, value, priority);
    public static CompositeStep<float> New(CompositeOperation operation, IValueWrapper<float> value, StepPriority priority = StepPriority.None)
      => new CompositeStep<float>(operation, value, priority);
    public static CompositeStep<double> New(CompositeOperation operation, IValueWrapper<double> value, StepPriority priority = StepPriority.None)
      => new CompositeStep<double>(operation, value, priority);

  }
}