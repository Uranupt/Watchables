

namespace Watchables
{
  public readonly struct SignedOperation
  {

    internal readonly NumericOperation Operation;

    public static SignedOperation Add => new(NumericOperation.Add);
    public static SignedOperation Subtract => new(NumericOperation.Subtract);
    public static SignedOperation Multiply => new(NumericOperation.Multiply);
    public static SignedOperation Divide => new(NumericOperation.Divide);
    public static SignedOperation Modulo => new(NumericOperation.Modulo);
    public static SignedOperation Power => new(NumericOperation.Power);
    public static SignedOperation Minimum => new(NumericOperation.Minimum);
    public static SignedOperation Maximum => new(NumericOperation.Maximum);
    public static SignedOperation Absolute => new(NumericOperation.Absolute);
    public static SignedOperation AsNegative => new(NumericOperation.AsNegative);
    public static SignedOperation FlipSign => new(NumericOperation.FlipSign);

    internal SignedOperation(NumericOperation operation)
    {
      Operation = operation;
    }

    public static implicit operator RealOperation(SignedOperation operation) => new(operation.Operation);

  }
}