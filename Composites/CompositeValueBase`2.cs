using System;


namespace Watchables
{ 
  public abstract class CompositeValueBase<TValue, TSelf> : OwnableBase, IValueWrapper<TValue> where TSelf : CompositeValueBase<TValue, TSelf>
  {

    public event Action<TSelf> Removed;
    public IValueWrapper<TValue> Value { get; protected set;  }
    public CompositePartPriority Priority { get; protected set;  }
    public bool IsValid => Value != null;

    protected CompositeValueBase(IValueWrapper<TValue> value, CompositePartPriority priority)
    {
      Value = value;
      Priority = priority;
      if(value is IWatchable watchable)
      {
        watchable.Destroyed += OnValueDestroyed;
      }
    }

    public static implicit operator TValue(CompositeValueBase<TValue, TSelf> cvb) => cvb.ToValue();

    public TValue ToValue() => IsValid ? Value.ToValue() : default;

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
      Removed?.Invoke((TSelf)this);
      return true;
    }

    protected void InvokeRemoved()
    {
      Removed?.Invoke((TSelf)this);
    }

    private void OnValueDestroyed(IWatchable value)
    {
      value.Destroyed -= OnValueDestroyed;
      Remove(_owner);
      Value = null;
    }

  }
}