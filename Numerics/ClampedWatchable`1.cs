using System;


namespace Watchables
{
  public sealed class ClampedWatchable<T> : SealableNestedBase<T>, IEnforceNumeric<T> where T : unmanaged
  {

    private OperationChain<T> _chain;
    private BasicWatchable<T> _baseValue;

    public IValueWrapper<T> Minimum { get; private set; }
    public IValueWrapper<T> Maximum { get; private set; }

    public ClampedWatchable(IValueWrapper<T> min, IValueWrapper<T> max)
    {
      NumericUtility.ValidateType(typeof(T));
      Minimum = min;
      Maximum = max;
      _baseValue = new BasicWatchable<T>();
      _chain = OperationBuilder.ClampChain(_baseValue, Minimum, Maximum);
      Register(_chain);
    }

    public bool Set(T value, object owner = null) => MutationGuard(() => _baseValue.SetValue(value), owner);

    public bool Add(T value, object owner = null) => Set(_value.Add(value), owner);

    public bool Subtract(T value, object owner = null) => Set(_value.Subtract(value), owner);

    public bool Sub(T value, object owner = null) => Subtract(value, owner);

    public bool Fill(object owner = null) => Set(Maximum.ToValue(), owner);

    public bool Empty(object owner = null) => Set(Minimum.ToValue(), owner);

    protected override bool CheckFatalDestruction(IWatchable dependency) => true;

    protected override void Evaluate() => _value = _chain;

    protected override void OnDestroyed(IWatchable self)
    {
      Unregister(_chain);
      _chain.Destroy();
      _baseValue.Destroy();
      _chain = null;
      _baseValue = null;
      Minimum = null;
      Maximum = null;
    }

  }
}
