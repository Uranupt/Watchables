

namespace Watchables
{ 
  public static class Operations
  {

    #region Watchables

    public static OperationWatchable<ushort> NewWatchable(UnsignedOperation operation, IValueWrapper<ushort> value, IValueWrapper<ushort> operand)
      => new OperationWatchable<ushort>(operation.Operation, value, operand);
    public static OperationWatchable<uint> NewWatchable(UnsignedOperation operation, IValueWrapper<uint> value, IValueWrapper<uint> operand)
      => new OperationWatchable<uint>(operation.Operation, value, operand);
    public static OperationWatchable<ulong> NewWatchable(UnsignedOperation operation, IValueWrapper<ulong> value, IValueWrapper<ulong> operand)
      => new OperationWatchable<ulong>(operation.Operation, value, operand);
    public static OperationWatchable<short> NewWatchable(SignedOperation operation, IValueWrapper<short> value, IValueWrapper<short> operand)
      => new OperationWatchable<short>(operation.Operation, value, operand);
    public static OperationWatchable<int> NewWatchable(SignedOperation operation, IValueWrapper<int> value, IValueWrapper<int> operand)
      => new OperationWatchable<int>(operation.Operation, value, operand);
    public static OperationWatchable<long> NewWatchable(SignedOperation operation, IValueWrapper<long> value, IValueWrapper<long> operand)
      => new OperationWatchable<long>(operation.Operation, value, operand);
    public static OperationWatchable<decimal> NewWatchable(RealOperation operation, IValueWrapper<decimal> value, IValueWrapper<decimal> operand)
      => new OperationWatchable<decimal>(operation.Operation, value, operand);
    public static OperationWatchable<float> NewWatchable(RealOperation operation, IValueWrapper<float> value, IValueWrapper<float> operand)
     => new OperationWatchable<float>(operation.Operation, value, operand);
    public static OperationWatchable<double> NewWatchable(RealOperation operation, IValueWrapper<double> value, IValueWrapper<double> operand)
      => new OperationWatchable<double>(operation.Operation, value, operand);

    #endregion

    #region Steps

    public static OperationStep<ushort> NewStep(UnsignedOperation operation, IValueWrapper<ushort> operand, bool required = false)
      => new OperationStep<ushort>(operation.Operation, operand, required);
    public static OperationStep<uint> NewStep(UnsignedOperation operation, IValueWrapper<uint> operand, bool required = false)
      => new OperationStep<uint>(operation.Operation, operand, required);
    public static OperationStep<ulong> NewStep(UnsignedOperation operation, IValueWrapper<ulong> operand, bool required = false)
      => new OperationStep<ulong>(operation.Operation, operand, required);
    public static OperationStep<short> NewStep(SignedOperation operation, IValueWrapper<short> operand, bool required = false)
      => new OperationStep<short>(operation.Operation, operand, required);
    public static OperationStep<int> NewStep(SignedOperation operation, IValueWrapper<int> operand, bool required = false)
      => new OperationStep<int>(operation.Operation, operand, required);
    public static OperationStep<long> NewStep(SignedOperation operation, IValueWrapper<long> operand, bool required = false)
      => new OperationStep<long>(operation.Operation, operand, required);
    public static OperationStep<decimal> NewStep(RealOperation operation, IValueWrapper<decimal> operand, bool required = false)
      => new OperationStep<decimal>(operation.Operation, operand, required);
    public static OperationStep<float> NewStep(RealOperation operation, IValueWrapper<float> operand, bool required = false)
      => new OperationStep<float>(operation.Operation, operand, required);
    public static OperationStep<double> NewStep(RealOperation operation, IValueWrapper<double> operand, bool required = false)
     => new OperationStep<double>(operation.Operation, operand, required);

    #endregion

    #region Chains

    public static OperationChain<ushort> NewChain(IValueWrapper<ushort> value) => new OperationChain<ushort>(value);

    public static OperationChain<ushort> NewStep(this OperationChain<ushort> chain, UnsignedOperation operation, 
      IValueWrapper<ushort> operand, bool required = false)
    {
      chain.AddStep(NewStep(operation, operand, required));
      return chain;
    }

    public static OperationChain<uint> NewChain(IValueWrapper<uint> value) => new OperationChain<uint>(value);

    public static OperationChain<uint> NewStep(this OperationChain<uint> chain, UnsignedOperation operation,
      IValueWrapper<uint> operand, bool required = false)
    {
      chain.AddStep(NewStep(operation, operand, required));
      return chain;
    }

    public static OperationChain<ulong> NewChain(IValueWrapper<ulong> value) => new OperationChain<ulong>(value);

    public static OperationChain<ulong> NewStep(this OperationChain<ulong> chain, UnsignedOperation operation,
      IValueWrapper<ulong> operand, bool required = false)
    {
      chain.AddStep(NewStep(operation, operand, required));
      return chain;
    }

    public static OperationChain<short> NewChain(IValueWrapper<short> value) => new OperationChain<short>(value);

    public static OperationChain<short> NewStep(this OperationChain<short> chain, SignedOperation operation,
      IValueWrapper<short> operand, bool required = false)
    {
      chain.AddStep(NewStep(operation, operand, required));
      return chain;
    }

    public static OperationChain<int> NewChain(IValueWrapper<int> value) => new OperationChain<int>(value);

    public static OperationChain<int> NewStep(this OperationChain<int> chain, SignedOperation operation,
      IValueWrapper<int> operand, bool required = false)
    {
      chain.AddStep(NewStep(operation, operand, required));
      return chain;
    }

    public static OperationChain<long> NewChain(IValueWrapper<long> value) => new OperationChain<long>(value);

    public static OperationChain<long> NewStep(this OperationChain<long> chain, SignedOperation operation,
      IValueWrapper<long> operand, bool required = false)
    {
      chain.AddStep(NewStep(operation, operand, required));
      return chain;
    }

    public static OperationChain<decimal> NewChain(IValueWrapper<decimal> value) => new OperationChain<decimal>(value);

    public static OperationChain<decimal> NewStep(this OperationChain<decimal> chain, RealOperation operation,
      IValueWrapper<decimal> operand, bool required = false)
    {
      chain.AddStep(NewStep(operation, operand, required));
      return chain;
    }

    public static OperationChain<float> NewChain(IValueWrapper<float> value) => new OperationChain<float>(value);

    public static OperationChain<float> NewStep(this OperationChain<float> chain, RealOperation operation,
      IValueWrapper<float> operand, bool required = false)
    {
      chain.AddStep(NewStep(operation, operand, required));
      return chain;
    }

    public static OperationChain<double> NewChain(IValueWrapper<double> value) => new OperationChain<double>(value);

    public static OperationChain<double> NewStep(this OperationChain<double> chain, RealOperation operation,
      IValueWrapper<double> operand, bool required = false)
    {
      chain.AddStep(NewStep(operation, operand, required));
      return chain;
    }

    #endregion

  }
}
