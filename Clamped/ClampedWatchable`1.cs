

using System;

namespace Watchables
{
  public abstract class ClampedWatchable<T> : SealableNestedBase<T> where T : unmanaged
  {

    private IValueWrapper<T> _minimum;
    private IValueWrapper<T> _maximum;

    public IValueWrapper<T> Minimum => _minimum;
    public IValueWrapper<T> Maximum => _maximum;
    /// <summary> Whether the value is frozen. This also prevents clamping. </summary>
    public bool IsFrozen { get; private set; }

    public ClampedWatchable(IValueWrapper<T> minimum, IValueWrapper<T> maximum)
    {
      SetBound(ref _minimum, minimum);
      SetBound(ref _maximum, maximum);
    }

    public bool SetMinimum(IValueWrapper<T> minimum, object owner = null)
    {
      return MutationGuard(() => SetBound(ref _minimum, minimum), owner);
    }

    public bool SetMaximum(IValueWrapper<T> maximum, object owner = null)
    {
      return MutationGuard(() => SetBound(ref _maximum, maximum), owner);
    }

    public bool SetFrozen(bool frozenSate, object owner = null)
    {
      if(IsDestroyed || (IsOwned && !CompareToOwner(owner))) { return false; }
      IsFrozen = frozenSate;
      return true;
    }

    public bool Set(T value, object owner = null)
    {
      if(IsDestroyed || (IsFrozen && !CompareToOwner(owner))) { return false; }
      _value = value;
      Evaluate();
      return true;
    }

    public bool Add(T value, object owner = null)
    {
      if(IsDestroyed || (IsFrozen && !CompareToOwner(owner))){ return false; }
      _value = Add(_value, value);
      Evaluate();
      return true;
    }

    public bool Subtract(T value, object owner = null)
    {
      if(IsDestroyed || (IsFrozen && !CompareToOwner(owner))) { return false; }
      _value = Subtract(_value, value);
      Evaluate();
      return true;
    }

    public bool Sub(T value, object owner = null) => Subtract(value, owner);

    protected sealed override bool CheckFatalDestruction(IWatchable dependency) => true;

    protected override void Evaluate()
    {
      if(IsFrozen) { return; }
      _value = Clamp(_value, Minimum.ToValue(), Maximum.ToValue());
    }

    protected override void OnDestroyed(IWatchable self)
    {
      ClearBound(ref _minimum);
      ClearBound(ref _maximum);
    }

    protected abstract T Clamp(T input, T min, T max);
    protected abstract T Add(T input, T value);
    protected abstract T Subtract(T input, T value);

    private void SetBound(ref IValueWrapper<T> field, IValueWrapper<T> value)
    {
      ClearBound(ref field);
      if(value is IWatchable)
      {
        Register(value as IWatchable);
      }
      field = value;
      Evaluate();
    }

    private void ClearBound(ref IValueWrapper<T> field)
    {
      if(field == null) { return; }
      if(field is IWatchable)
      {
        Unregister(field as IWatchable);
      }
      field = null;
    }

  }
}
