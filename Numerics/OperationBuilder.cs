using System;


namespace Watchables
{
  public static class OperationBuilder
  {

    public static OperationStep<ushort> Step(UnsignedOperation operation, IValueWrapper<ushort> operand, bool required = false)
      => new OperationStep<ushort>(operation, operand, required);
    public static OperationStep<uint> Step(UnsignedOperation operation, IValueWrapper<uint> operand, bool required = false)
      => new OperationStep<uint>(operation, operand, required);
    public static OperationStep<ulong> Step(UnsignedOperation operation, IValueWrapper<ulong> operand, bool required = false)
      => new OperationStep<ulong>(operation, operand, required);
    public static OperationStep<short> Step(IntegerOperation operation, IValueWrapper<short> operand, bool required = false)
      => new OperationStep<short>(operation, operand, required);
    public static OperationStep<int> Step(IntegerOperation operation, IValueWrapper<int> operand, bool required = false)
      => new OperationStep<int>(operation, operand, required);
    public static OperationStep<long> Step(IntegerOperation operation, IValueWrapper<long> operand, bool required = false)
      => new OperationStep<long>(operation, operand, required);
    public static OperationStep<decimal> Step(NumericOperation operation, IValueWrapper<decimal> operand, bool required = false)
      => new OperationStep<decimal>(operation, operand, required);
    public static OperationStep<float> Step(NumericOperation operation, IValueWrapper<float> operand, bool required = false)
      => new OperationStep<float>(operation, operand, required);
    public static OperationStep<double> Step(NumericOperation operation, IValueWrapper<double> operand, bool required = false)
     => new OperationStep<double>(operation, operand, required);

    public static OperationWatchable<ushort> Watchable(UnsignedOperation operation, IValueWrapper<ushort> value, IValueWrapper<ushort> operand)
      => new OperationWatchable<ushort>(operation, value, operand);
    public static OperationWatchable<uint> Watchable(UnsignedOperation operation, IValueWrapper<uint> value, IValueWrapper<uint> operand)
      => new OperationWatchable<uint>(operation, value, operand);
    public static OperationWatchable<ulong> Watchable(UnsignedOperation operation, IValueWrapper<ulong> value, IValueWrapper<ulong> operand)
      => new OperationWatchable<ulong>(operation, value, operand);
    public static OperationWatchable<short> Watchable(IntegerOperation operation, IValueWrapper<short> value, IValueWrapper<short> operand)
      => new OperationWatchable<short>(operation, value, operand);
    public static OperationWatchable<int> Watchable(IntegerOperation operation, IValueWrapper<int> value, IValueWrapper<int> operand)
      => new OperationWatchable<int>(operation, value, operand);
    public static OperationWatchable<long> Watchable(IntegerOperation operation, IValueWrapper<long> value, IValueWrapper<long> operand)
      => new OperationWatchable<long>(operation, value, operand);
    public static OperationWatchable<decimal> Watchable(NumericOperation operation, IValueWrapper<decimal> value, IValueWrapper<decimal> operand)
      => new OperationWatchable<decimal>(operation, value, operand);
    public static OperationWatchable<float> Watchable(NumericOperation operation, IValueWrapper<float> value, IValueWrapper<float> operand)
     => new OperationWatchable<float>(operation, value, operand);
    public static OperationWatchable<double> Watchable(NumericOperation operation, IValueWrapper<double> value, IValueWrapper<double> operand)
      => new OperationWatchable<double>(operation, value, operand);

    public static OperationChain<T> Clamp<T>(IValueWrapper<T> value, IValueWrapper<T> min, IValueWrapper<T> max) where T : unmanaged
    {
      return new OperationChain<T>(value)
        .Then(UnsignedOperation.Minimum, min, true)
        .Then(UnsignedOperation.Maximum, max, true);
    }

    public static OperationChain<decimal> RatioScale(IValueWrapper<decimal> value, IValueWrapper<decimal> ante, IValueWrapper<decimal> cons,
      bool baseOne = false, bool normalize = false)
    {
      OperationChain<decimal> resl = new OperationChain<decimal>(ante).Then(NumericOperation.Divide, cons, true);
      if(normalize)
      {
        resl.Then(NumericOperation.Minimum, 0m.Wrap())
          .Then(NumericOperation.Maximum, 1m.Wrap());
      }
      if(baseOne)
      {
        resl.Then(NumericOperation.Add, 1m.Wrap());
      }
      return resl.Then(NumericOperation.Multiply, value, true);
    }

    public static OperationChain<float> RatioScale(IValueWrapper<float> value, IValueWrapper<float> ante, IValueWrapper<float> cons,
      bool baseOne = false, bool normalize = false)
    {
      OperationChain<float> resl = new OperationChain<float>(ante).Then(NumericOperation.Divide, cons, true);
      if(normalize)
      {
        resl.Then(NumericOperation.Minimum, 0f.Wrap())
          .Then(NumericOperation.Maximum, 1f.Wrap());
      }
      if(baseOne)
      {
        resl.Then(NumericOperation.Add, 1f.Wrap());
      }
      return resl.Then(NumericOperation.Multiply, value, true);
    }

    public static OperationChain<double> RatioScale(IValueWrapper<double> value, IValueWrapper<double> ante, IValueWrapper<double> cons,
      bool baseOne = false, bool normalize = false)
    {
      OperationChain<double> resl = new OperationChain<double>(ante).Then(NumericOperation.Divide, cons, true);
      if(normalize)
      {
        resl.Then(NumericOperation.Minimum, 0d.Wrap())
          .Then(NumericOperation.Maximum, 1d.Wrap());
      }
      if(baseOne)
      {
        resl.Then(NumericOperation.Add, 1d.Wrap());
      }
      return resl.Then(NumericOperation.Multiply, value, true);
    }

    internal static OperationStep<T> Step<T>(NumericOperation operation, IValueWrapper<T> operands, bool required = false) where T : unmanaged
    {
      throw new ArgumentException($"Invalid OperationStep type: {typeof(T).Name}. Only numeric types are allowed.");
    }

  }
}
