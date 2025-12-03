using System;


namespace Watchables
{
  public sealed class ClampedWatchable<T> : BoundedWatchable<T> where T : unmanaged
  {

    /// <summary> Whether the value is frozen. This also prevents clamping. </summary>
    public bool IsFrozen { get; private set; }

    public ClampedWatchable(IValueWrapper<T> minimum, IValueWrapper<T> maximum) : base(minimum, maximum)
    {

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
      _value = _value.Add(value);
      Evaluate();
      return true;
    }

    public bool Subtract(T value, object owner = null)
    {
      if(IsDestroyed || (IsFrozen && !CompareToOwner(owner))) { return false; }
      _value = _value.Subtract(value);
      Evaluate();
      return true;
    }

    public bool Sub(T value, object owner = null) => Subtract(value, owner);

    protected sealed override bool CheckFatalDestruction(IWatchable dependency) => true;

    protected override void Evaluate()
    {
      if(IsFrozen) { return; }
      _value = _value.Minimum(Minimum.ToValue()).Maximum(Maximum.ToValue());
    }

  }
}
