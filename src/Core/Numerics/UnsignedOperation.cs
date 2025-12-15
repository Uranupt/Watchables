

namespace Watchables
{
  /// <summary>
  /// Wrapper for <see cref="NumericOperation"/> which only allows operations that are valid for unsigned integer types.
  /// Implicitly castable to <see cref="NumericOperation"/> and <see cref="IntegerOperation"/>.
  /// </summary>
  public readonly struct UnsignedOperation : IWrapper<NumericOperation>
  {

    /// <inheritdoc cref="NumericOperation.Add"/>
    public static UnsignedOperation Add => new(NumericOperation.Add);

    /// <inheritdoc cref="NumericOperation.Subtract"/>
    public static UnsignedOperation Subtract => new(NumericOperation.Subtract);

    /// <inheritdoc cref="NumericOperation.Multiply"/>
    public static UnsignedOperation Multiply => new(NumericOperation.Multiply);

    /// <inheritdoc cref="NumericOperation.Divide"/>
    public static UnsignedOperation Divide => new(NumericOperation.Divide);

    /// <inheritdoc cref="NumericOperation.Modulo"/>
    public static UnsignedOperation Modulo => new(NumericOperation.Modulo);

    /// <inheritdoc cref="NumericOperation.Power"/>
    public static UnsignedOperation Power => new(NumericOperation.Power);

    /// <inheritdoc cref="NumericOperation.GreaterOf"/>
    public static UnsignedOperation GreaterOf => new(NumericOperation.GreaterOf);

    /// <inheritdoc cref="NumericOperation.LesserOf"/>
    public static UnsignedOperation LesserOf => new(NumericOperation.LesserOf);

    public readonly NumericOperation Value { get; }

    internal UnsignedOperation(NumericOperation operation)
    {
      Value = operation;
    }

    public static implicit operator IntegerOperation(UnsignedOperation operation) => new(operation.Value);
    public static implicit operator NumericOperation(UnsignedOperation operation) => operation.Value;

  }
}
