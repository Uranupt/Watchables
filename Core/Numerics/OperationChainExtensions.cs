using System;


namespace Watchables
{
  /// <summary>
  /// Utility extensions for <see cref="OperationChain{T}"/>
  /// </summary>
  public static class OperationChainExtensions
  {

    #region Then

    /// <inheritdoc cref="OperationChain{T}.Then"/>
    public static OperationChain<uint> Then(this OperationChain<uint> chain, UnsignedOperation operation, IWrapper<uint> operand,
      bool required = false, object owner = null)
    {
      return chain.Then(OperationBuilder.Step(operation, operand, required), owner);
    }

    /// <inheritdoc cref="OperationChain{T}.Then"/>
    public static OperationChain<ulong> Then(this OperationChain<ulong> chain, UnsignedOperation operation, IWrapper<ulong> operand,
      bool required = false, object owner = null)
    {
      return chain.Then(OperationBuilder.Step(operation, operand, required), owner);
    }

    /// <inheritdoc cref="OperationChain{T}.Then"/>
    public static OperationChain<int> Then(this OperationChain<int> chain, IntegerOperation operation, IWrapper<int> operand,
      bool required = false, object owner = null)
    {
      return chain.Then(OperationBuilder.Step(operation, operand, required), owner);
    }

    /// <inheritdoc cref="OperationChain{T}.Then"/>
    public static OperationChain<long> Then(this OperationChain<long> chain, IntegerOperation operation, IWrapper<long> operand,
      bool required = false, object owner = null)
    {
      return chain.Then(OperationBuilder.Step(operation, operand, required), owner);
    }

    /// <inheritdoc cref="OperationChain{T}.Then"/>
    public static OperationChain<decimal> Then(this OperationChain<decimal> chain, NumericOperation operation, IWrapper<decimal> operand,
      bool required = false, object owner = null)
    {
      return chain.Then(OperationBuilder.Step(operation, operand, required), owner);
    }

    /// <inheritdoc cref="OperationChain{T}.Then"/>
    public static OperationChain<float> Then(this OperationChain<float> chain, NumericOperation operation, IWrapper<float> operand,
      bool required = false, object owner = null)
    {
      return chain.Then(OperationBuilder.Step(operation, operand, required), owner);
    }

    /// <inheritdoc cref="OperationChain{T}.Then"/>
    public static OperationChain<double> Then(this OperationChain<double> chain, NumericOperation operation, IWrapper<double> operand,
      bool required = false, object owner = null)
    {
      return chain.Then(OperationBuilder.Step(operation, operand, required), owner);
    }

    #endregion

    #region AddStep

    /// <inheritdoc cref="OperationChain{T}.AddStep"/>
    public static bool AddStep(this OperationChain<uint> chain, UnsignedOperation operation, IWrapper<uint> operand,
      bool required = false, object owner = null)
    {
      return chain.AddStep(OperationBuilder.Step(operation, operand, required), owner);
    }

    /// <inheritdoc cref="OperationChain{T}.AddStep"/>
    public static bool AddStep(this OperationChain<ulong> chain, UnsignedOperation operation, IWrapper<ulong> operand,
      bool required = false, object owner = null)
    {
      return chain.AddStep(OperationBuilder.Step(operation, operand, required), owner);
    }

    /// <inheritdoc cref="OperationChain{T}.AddStep"/>
    public static bool AddStep(this OperationChain<int> chain, IntegerOperation operation, IWrapper<int> operand,
      bool required = false, object owner = null)
    {
      return chain.AddStep(OperationBuilder.Step(operation, operand, required), owner);
    }

    /// <inheritdoc cref="OperationChain{T}.AddStep"/>
    public static bool AddStep(this OperationChain<long> chain, IntegerOperation operation, IWrapper<long> operand,
      bool required = false, object owner = null)
    {
      return chain.AddStep(OperationBuilder.Step(operation, operand, required), owner);
    }

    /// <inheritdoc cref="OperationChain{T}.AddStep"/>
    public static bool AddStep(this OperationChain<decimal> chain, NumericOperation operation, IWrapper<decimal> operand,
      bool required = false, object owner = null)
    {
      return chain.AddStep(OperationBuilder.Step(operation, operand, required), owner);
    }

    /// <inheritdoc cref="OperationChain{T}.AddStep"/>
    public static bool AddStep(this OperationChain<float> chain, NumericOperation operation, IWrapper<float> operand,
      bool required = false, object owner = null)
    {
      return chain.AddStep(OperationBuilder.Step(operation, operand, required), owner);
    }

    /// <inheritdoc cref="OperationChain{T}.AddStep"/>
    public static bool AddStep(this OperationChain<double> chain, NumericOperation operation, IWrapper<double> operand,
      bool required = false, object owner = null)
    {
      return chain.AddStep(OperationBuilder.Step(operation, operand, required), owner);
    }

    #endregion

    internal static OperationChain<T> Then<T>(this OperationChain<T> chain, NumericOperation operation, IWrapper<T> operand,
      bool required = false, object owner = null)
      where T : unmanaged
    {
      throw new ArgumentException($"Invalid OperationStep type: {typeof(T).Name}. Only numeric types are allowed.");
    }

  }
}