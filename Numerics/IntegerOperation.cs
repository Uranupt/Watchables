

namespace Watchables
{
  public readonly struct IntegerOperation
  {

    internal readonly NumericOperation Value;

    public static IntegerOperation Add => new(NumericOperation.Add);
    public static IntegerOperation Subtract => new(NumericOperation.Subtract);
    public static IntegerOperation Multiply => new(NumericOperation.Multiply);
    public static IntegerOperation Divide => new(NumericOperation.Divide);
    public static IntegerOperation Modulo => new(NumericOperation.Modulo);
    public static IntegerOperation Power => new(NumericOperation.Power);
    public static IntegerOperation Minimum => new(NumericOperation.Minimum);
    public static IntegerOperation Maximum => new(NumericOperation.Maximum);
    public static IntegerOperation Absolute => new(NumericOperation.Absolute);
    public static IntegerOperation AsNegative => new(NumericOperation.AsNegative);
    public static IntegerOperation FlipSign => new(NumericOperation.FlipSign);

    internal IntegerOperation(NumericOperation operation)
    {
      Value = operation;
    }

    public static implicit operator NumericOperation(IntegerOperation operation) => operation.Value;

  }
}