

namespace Watchables
{
  public abstract class Operation<T> : NestedWatchable<T> where T : unmanaged
  {

    protected IValueWrapper<T> _base;
    protected IValueWrapper<T> _operand;
    protected readonly OperationType _operation;

    protected Operation(OperationType op, IValueWrapper<T> baseValue, IValueWrapper<T> operand = null) : base()
    {
      _operation = op;
      _base = baseValue;
      _operand = operand;
      if(_base is IWatchable)
      {
        Register(_base as IWatchable);
      }
      if(_operand != null && _operand is IWatchable)
      {
        Register(_operand as IWatchable);
      }
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
      if(_operand != null && _operand is IWatchable)
      {
        Unregister(_operand as IWatchable);
      }
      _base = null;
      _operand = null;
    }

  }
}