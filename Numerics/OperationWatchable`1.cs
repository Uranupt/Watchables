

namespace Watchables
{
  public sealed class OperationWatchable<T> : NestedWatchable<T>, IEnforceNumeric<T> where T : unmanaged
  {

    private IValueWrapper<T> _base;
    private IValueWrapper<T> _operand;
    internal readonly NumericOperation _operation;

    internal OperationWatchable(NumericOperation op, IValueWrapper<T> baseValue, IValueWrapper<T> operand = null) : base()
    {
      _operation = op;
      _base = baseValue;
      _operand = operand ?? new ReadOnlyWrapper<T>(default);
      if(_base is IWatchable)
      {
        Register(_base as IWatchable);
      }
      if(_operand != null && _operand is IWatchable)
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

  public static class OperationWatchable
  {

    public static OperationWatchable<ushort> New(UnsignedOperation operation, IValueWrapper<ushort> value, IValueWrapper<ushort> operand)
      => new OperationWatchable<ushort>(operation.Operation, value, operand);
    public static OperationWatchable<uint> New(UnsignedOperation operation, IValueWrapper<uint> value, IValueWrapper<uint> operand)
      => new OperationWatchable<uint>(operation.Operation, value, operand);
    public static OperationWatchable<ulong> New(UnsignedOperation operation, IValueWrapper<ulong> value, IValueWrapper<ulong> operand)
      => new OperationWatchable<ulong>(operation.Operation, value, operand);
    public static OperationWatchable<short> New(SignedOperation operation, IValueWrapper<short> value, IValueWrapper<short> operand)
      => new OperationWatchable<short>(operation.Operation, value, operand);
    public static OperationWatchable<int> New(SignedOperation operation, IValueWrapper<int> value, IValueWrapper<int> operand)
      => new OperationWatchable<int>(operation.Operation, value, operand);
    public static OperationWatchable<long> New(SignedOperation operation, IValueWrapper<long> value, IValueWrapper<long> operand)
      => new OperationWatchable<long>(operation.Operation, value, operand);
    public static OperationWatchable<decimal> New(RealOperation operation, IValueWrapper<decimal> value, IValueWrapper<decimal> operand)
      => new OperationWatchable<decimal>(operation.Operation, value, operand);
    public static OperationWatchable<float> New(RealOperation operation, IValueWrapper<float> value, IValueWrapper<float> operand)
     => new OperationWatchable<float>(operation.Operation, value, operand);
    public static OperationWatchable<double> New(RealOperation operation, IValueWrapper<double> value, IValueWrapper<double> operand)
      => new OperationWatchable<double>(operation.Operation, value, operand);

  }
}