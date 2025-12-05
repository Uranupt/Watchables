

namespace Watchables
{
  public sealed class OperationWatchable<T> : NestedWatchable<T>, IEnforceNumeric<T> where T : unmanaged
  {

    private IValueWrapper<T> _base;
    private IValueWrapper<T> _operand;
    internal readonly NumericOperation _operation;

    internal OperationWatchable(NumericOperation op, IValueWrapper<T> baseValue, IValueWrapper<T> operand = null) : base()
    {
      NumericUtility.ValidateType(typeof(T));
      _operation = op;
      _base = baseValue;
      _operand = operand ?? new ReadOnlyWrapper<T>(default);
      if(_base is IWatchable)
      {
        Register(_base as IWatchable);
      }
      if(_operand is IWatchable)
      {
        Register(_operand as IWatchable);
      }
    }

    protected override void Evaluate()
    {
      _value = _base.ToValue().Operate(_operation, _operand.ToValue(), true);
    }

    protected override bool CheckFatalDestruction(IWatchable dependency)
    {
      return dependency == _base || dependency == _operand;
    }

    protected override void OnDestroyed(IWatchable self)
    {
      if(_base is IWatchable)
      {
        Unregister(_base as IWatchable);
      }
      if(_operand is IWatchable)
      {
        Unregister(_operand as IWatchable);
      }
      _base = null;
      _operand = null;
    }

  }
}