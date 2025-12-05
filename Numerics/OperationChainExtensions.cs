using System;


namespace Watchables
{
  public static class OperationChainExtensions
  {

    public static OperationChain<ushort> Then(this OperationChain<ushort> chain, UnsignedOperation operation, IValueWrapper<ushort> operand,
      bool required = false, object owner = null)
    {
      return chain.Then(OperationBuilder.Step(operation, operand, required), owner);
    }

    public static OperationChain<uint> Then(this OperationChain<uint> chain, UnsignedOperation operation, IValueWrapper<uint> operand,
      bool required = false, object owner = null)
    {
      return chain.Then(OperationBuilder.Step(operation, operand, required), owner);
    }

    public static OperationChain<ulong> Then(this OperationChain<ulong> chain, UnsignedOperation operation, IValueWrapper<ulong> operand,
      bool required = false, object owner = null)
    {
      return chain.Then(OperationBuilder.Step(operation, operand, required), owner);
    }

    public static OperationChain<short> Then(this OperationChain<short> chain, IntegerOperation operation, IValueWrapper<short> operand,
      bool required = false, object owner = null)
    {
      return chain.Then(OperationBuilder.Step(operation, operand, required), owner);
    }

    public static OperationChain<int> Then(this OperationChain<int> chain, IntegerOperation operation, IValueWrapper<int> operand,
      bool required = false, object owner = null)
    {
      return chain.Then(OperationBuilder.Step(operation, operand, required), owner);
    }

    public static OperationChain<long> Then(this OperationChain<long> chain, IntegerOperation operation, IValueWrapper<long> operand,
      bool required = false, object owner = null)
    {
      return chain.Then(OperationBuilder.Step(operation, operand, required), owner);
    }

    public static OperationChain<decimal> Then(this OperationChain<decimal> chain, NumericOperation operation, IValueWrapper<decimal> operand,
      bool required = false, object owner = null)
    {
      return chain.Then(OperationBuilder.Step(operation, operand, required), owner);
    }

    public static OperationChain<float> Then(this OperationChain<float> chain, NumericOperation operation, IValueWrapper<float> operand,
      bool required = false, object owner = null)
    {
      return chain.Then(OperationBuilder.Step(operation, operand, required), owner);
    }

    public static OperationChain<double> Then(this OperationChain<double> chain, NumericOperation operation, IValueWrapper<double> operand,
      bool required = false, object owner = null)
    {
      return chain.Then(OperationBuilder.Step(operation, operand, required), owner);
    }

    public static bool AddStep(this OperationChain<ushort> chain, UnsignedOperation operation, IValueWrapper<ushort> operand,
      bool required = false, object owner = null)
    {
      return chain.AddStep(OperationBuilder.Step(operation, operand, required), owner);
    }

    public static bool AddStep(this OperationChain<uint> chain, UnsignedOperation operation, IValueWrapper<uint> operand,
      bool required = false, object owner = null)
    {
      return chain.AddStep(OperationBuilder.Step(operation, operand, required), owner);
    }

    public static bool AddStep(this OperationChain<ulong> chain, UnsignedOperation operation, IValueWrapper<ulong> operand,
      bool required = false, object owner = null)
    {
      return chain.AddStep(OperationBuilder.Step(operation, operand, required), owner);
    }

    public static bool AddStep(this OperationChain<short> chain, UnsignedOperation operation, IValueWrapper<short> operand,
      bool required = false, object owner = null)
    {
      return chain.AddStep(OperationBuilder.Step(operation, operand, required), owner);
    }

    public static bool AddStep(this OperationChain<int> chain, UnsignedOperation operation, IValueWrapper<int> operand,
      bool required = false, object owner = null)
    {
      return chain.AddStep(OperationBuilder.Step(operation, operand, required), owner);
    }

    public static bool AddStep(this OperationChain<long> chain, UnsignedOperation operation, IValueWrapper<long> operand,
      bool required = false, object owner = null)
    {
      return chain.AddStep(OperationBuilder.Step(operation, operand, required), owner);
    }

    public static bool AddStep(this OperationChain<decimal> chain, UnsignedOperation operation, IValueWrapper<decimal> operand,
      bool required = false, object owner = null)
    {
      return chain.AddStep(OperationBuilder.Step(operation, operand, required), owner);
    }

    public static bool AddStep(this OperationChain<float> chain, UnsignedOperation operation, IValueWrapper<float> operand,
      bool required = false, object owner = null)
    {
      return chain.AddStep(OperationBuilder.Step(operation, operand, required), owner);
    }

    public static bool AddStep(this OperationChain<double> chain, UnsignedOperation operation, IValueWrapper<double> operand,
      bool required = false, object owner = null)
    {
      return chain.AddStep(OperationBuilder.Step(operation, operand, required), owner);
    }

    internal static OperationChain<T> Then<T>(this OperationChain<T> chain, NumericOperation operation, IValueWrapper<T> operand,
      bool required = false, object owner = null)
      where T : unmanaged
    {
      throw new ArgumentException($"Invalid OperationStep type: {typeof(T).Name}. Only numeric types are allowed.");
    }

  }
}