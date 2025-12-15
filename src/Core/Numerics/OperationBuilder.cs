using System;


namespace Watchables
{
  /// <summary>
  /// Static constructors for <see cref="OperationWatchable{T}"/> and <see cref="OperationStep{T}"/>, as well as 
  /// constructions for common <see cref="OperationChain{T}"/> uses.
  /// </summary>
  public static class OperationBuilder
  {

    #region Step

    /// <inheritdoc cref="Step{T}"/>
    public static OperationStep<uint> Step(UnsignedOperation operation, IWrapper<uint> operand, bool required = false)
      => new OperationStep<uint>(operation, operand, required);

    /// <inheritdoc cref="Step{T}"/>
    public static OperationStep<ulong> Step(UnsignedOperation operation, IWrapper<ulong> operand, bool required = false)
      => new OperationStep<ulong>(operation, operand, required);

    /// <inheritdoc cref="Step{T}"/>
    public static OperationStep<int> Step(IntegerOperation operation, IWrapper<int> operand = null, bool required = false)
      => new OperationStep<int>(operation, operand, required);

    /// <inheritdoc cref="Step{T}"/>
    public static OperationStep<long> Step(IntegerOperation operation, IWrapper<long> operand = null, bool required = false)
      => new OperationStep<long>(operation, operand, required);

    /// <inheritdoc cref="Step{T}"/>
    public static OperationStep<decimal> Step(NumericOperation operation, IWrapper<decimal> operand = null, bool required = false)
      => new OperationStep<decimal>(operation, operand, required);

    /// <inheritdoc cref="Step{T}"/>
    public static OperationStep<float> Step(NumericOperation operation, IWrapper<float> operand = null, bool required = false)
      => new OperationStep<float>(operation, operand, required);

    /// <inheritdoc cref="Step{T}"/>
    public static OperationStep<double> Step(NumericOperation operation, IWrapper<double> operand = null, bool required = false)
     => new OperationStep<double>(operation, operand, required);

    #endregion

    #region Watchable

    /// <inheritdoc cref="Watchable{T}"/>
    public static OperationWatchable<uint> Watchable(UnsignedOperation operation, IWrapper<uint> value, IWrapper<uint> operand)
      => new OperationWatchable<uint>(operation, value, operand);

    /// <inheritdoc cref="Watchable{T}"/>
    public static OperationWatchable<ulong> Watchable(UnsignedOperation operation, IWrapper<ulong> value, IWrapper<ulong> operand)
      => new OperationWatchable<ulong>(operation, value, operand);

    /// <inheritdoc cref="Watchable{T}"/>
    public static OperationWatchable<int> Watchable(IntegerOperation operation, IWrapper<int> value, IWrapper<int> operand = null)
      => new OperationWatchable<int>(operation, value, operand);

    /// <inheritdoc cref="Watchable{T}"/>
    public static OperationWatchable<long> Watchable(IntegerOperation operation, IWrapper<long> value, IWrapper<long> operand = null)
      => new OperationWatchable<long>(operation, value, operand);

    /// <inheritdoc cref="Watchable{T}"/>
    public static OperationWatchable<decimal> Watchable(NumericOperation operation, IWrapper<decimal> value, IWrapper<decimal> operand = null)
      => new OperationWatchable<decimal>(operation, value, operand);

    /// <inheritdoc cref="Watchable{T}"/>
    public static OperationWatchable<float> Watchable(NumericOperation operation, IWrapper<float> value, IWrapper<float> operand = null)
     => new OperationWatchable<float>(operation, value, operand);

    /// <inheritdoc cref="Watchable{T}"/>
    public static OperationWatchable<double> Watchable(NumericOperation operation, IWrapper<double> value, IWrapper<double> operand = null)
      => new OperationWatchable<double>(operation, value, operand);

    #endregion

    #region Auto Chains

    /// <summary>
    /// Creates a new <see cref="OperationChain{T}"/> which will clamp the <paramref name="value"/> between <paramref name="min"/> and <paramref name="max"/>
    /// </summary>
    public static OperationChain<T> Clamp<T>(IWrapper<T> value, IWrapper<T> min, IWrapper<T> max) where T : unmanaged
    {
      return new OperationChain<T>(value)
        .Then(UnsignedOperation.GreaterOf, min, true)
        .Then(UnsignedOperation.LesserOf, max, true);
    }

    /// <summary>
    /// Creates a new <see cref="OperationChain{T}"/> which scales the <paramref name="value"/> by the ratio of the 
    /// <paramref name="ante"/>cedent to the <paramref name="cons"/>equent.
    /// <br/> If <paramref name="baseOne"/> is true, the ratio will be offset by 1 so that a ratio of 1:1 results in a doubling. 
    /// <br/> If <paramref name="normalize"/> is true, the ratio (before offset from baseOne) will be clamped between 0 and 1.
    /// </summary>
    public static OperationChain<decimal> RatioScale(IWrapper<decimal> value, IWrapper<decimal> ante, IWrapper<decimal> cons,
      bool baseOne = false, bool normalize = false)
    {
      OperationChain<decimal> resl = new OperationChain<decimal>(ante).Then(NumericOperation.Divide, cons, true);
      if(normalize)
      {
        resl.Then(NumericOperation.GreaterOf, 0m.Wrap())
          .Then(NumericOperation.LesserOf, 1m.Wrap());
      }
      if(baseOne)
      {
        resl.Then(NumericOperation.Add, 1m.Wrap());
      }
      return resl.Then(NumericOperation.Multiply, value, true);
    }

    /// <summary>
    /// Creates a new <see cref="OperationChain{T}"/> which scales the <paramref name="value"/> by the ratio of the 
    /// <paramref name="ante"/>cedent to the <paramref name="cons"/>equent.
    /// <br/> If <paramref name="baseOne"/> is true, the ratio will be offset by 1 so that a ratio of 1:1 results in a doubling. 
    /// <br/> If <paramref name="normalize"/> is true, the ratio (before offset from baseOne) will be clamped between 0 and 1.
    /// </summary>
    public static OperationChain<float> RatioScale(IWrapper<float> value, IWrapper<float> ante, IWrapper<float> cons,
      bool baseOne = false, bool normalize = false)
    {
      OperationChain<float> resl = new OperationChain<float>(ante).Then(NumericOperation.Divide, cons, true);
      if(normalize)
      {
        resl.Then(NumericOperation.GreaterOf, 0f.Wrap())
          .Then(NumericOperation.LesserOf, 1f.Wrap());
      }
      if(baseOne)
      {
        resl.Then(NumericOperation.Add, 1f.Wrap());
      }
      return resl.Then(NumericOperation.Multiply, value, true);
    }

    /// <summary>
    /// Creates a new <see cref="OperationChain{T}"/> which scales the <paramref name="value"/> by the ratio of the 
    /// <paramref name="ante"/>cedent to the <paramref name="cons"/>equent.
    /// <br/> If <paramref name="baseOne"/> is true, the ratio will be offset by 1 so that a ratio of 1:1 results in a doubling. 
    /// <br/> If <paramref name="normalize"/> is true, the ratio (before offset from baseOne) will be clamped between 0 and 1.
    /// </summary>
    public static OperationChain<double> RatioScale(IWrapper<double> value, IWrapper<double> ante, IWrapper<double> cons,
      bool baseOne = false, bool normalize = false)
    {
      OperationChain<double> resl = new OperationChain<double>(ante).Then(NumericOperation.Divide, cons, true);
      if(normalize)
      {
        resl.Then(NumericOperation.GreaterOf, 0d.Wrap())
          .Then(NumericOperation.LesserOf, 1d.Wrap());
      }
      if(baseOne)
      {
        resl.Then(NumericOperation.Add, 1d.Wrap());
      }
      return resl.Then(NumericOperation.Multiply, value, true);
    }

    #endregion

    /// <summary> Creates a new <see cref="OperationStep{T}"/> which performs the given <paramref name="operation"/>. </summary>
    /// <param name="required"> Whether the step is required for the <see cref="OperationChain{T}"/> it's a part of. </param>
    internal static OperationStep<T> Step<T>(NumericOperation operation, IWrapper<T> operand, bool required = false) where T : unmanaged
    {
      throw new ArgumentException($"Invalid OperationStep type: {typeof(T).Name}. Only numeric types are allowed.");
    }

    /// <summary> Creates a new <see cref="OperationWatchable{T}"/> which performs the given <paramref name="operation"/>. </summary>
    private static OperationWatchable<T> Watchable<T>(NumericOperation operation, IWrapper<ushort> value, IWrapper<ushort> operand) where T : unmanaged
    {
      throw new InvalidOperationException("This method should never be called.");
    }

  }
}
