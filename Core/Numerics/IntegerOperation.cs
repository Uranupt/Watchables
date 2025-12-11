

namespace Watchables
{
  /// <summary>
  /// Wrapper for <see cref="NumericOperation"/> which only allows operations that are valid for signed integer types.
  /// Implicitly castable to <see cref="NumericOperation"/>.
  /// </summary>
  public readonly struct IntegerOperation : IWrapper<NumericOperation>
  {

    /// <inheritdoc cref="NumericOperation.Add"/>
    public static IntegerOperation Add => new(NumericOperation.Add);

    /// <inheritdoc cref="NumericOperation.Subtract"/>
    public static IntegerOperation Subtract => new(NumericOperation.Subtract);

    /// <inheritdoc cref="NumericOperation.Multiply"/>
    public static IntegerOperation Multiply => new(NumericOperation.Multiply);

    /// <inheritdoc cref="NumericOperation.Divide"/>
    public static IntegerOperation Divide => new(NumericOperation.Divide);

    /// <inheritdoc cref="NumericOperation.Modulo"/>
    public static IntegerOperation Modulo => new(NumericOperation.Modulo);

    /// <inheritdoc cref="NumericOperation.Power"/>
    public static IntegerOperation Power => new(NumericOperation.Power);

    /// <inheritdoc cref="NumericOperation.GreaterOf"/>
    public static IntegerOperation GreaterOf => new(NumericOperation.GreaterOf);

    /// <inheritdoc cref="NumericOperation.LesserOf"/>
    public static IntegerOperation LesserOf => new(NumericOperation.LesserOf);

    /// <inheritdoc cref="NumericOperation.Absolute"/>
    public static IntegerOperation Absolute => new(NumericOperation.Absolute);

    /// <inheritdoc cref="NumericOperation.AsNegative"/>
    public static IntegerOperation AsNegative => new(NumericOperation.AsNegative);

    /// <inheritdoc cref="NumericOperation.FlipSign"/>
    public static IntegerOperation FlipSign => new(NumericOperation.FlipSign);

    public readonly NumericOperation Value { get; }

    internal IntegerOperation(NumericOperation operation)
    {
      Value = operation;
    }

    public static implicit operator NumericOperation(IntegerOperation operation) => operation.Value;

  }
}