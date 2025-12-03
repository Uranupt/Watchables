

namespace Watchables
{
  public readonly struct UnsignedOperation
  {

    internal readonly NumericOperation Operation;

    public static UnsignedOperation Add => new(NumericOperation.Add);
    public static UnsignedOperation Subtract => new(NumericOperation.Subtract);
    public static UnsignedOperation Multiply => new(NumericOperation.Multiply);
    public static UnsignedOperation Divide => new(NumericOperation.Divide);
    public static UnsignedOperation Modulo => new(NumericOperation.Modulo);
    public static UnsignedOperation Power => new(NumericOperation.Power);
    public static UnsignedOperation Minimum => new(NumericOperation.Minimum);
    public static UnsignedOperation Maximum => new(NumericOperation.Maximum);

    internal UnsignedOperation(NumericOperation operation)
    {
      Operation = operation;
    }

    public static implicit operator SignedOperation(UnsignedOperation operation) => new(operation.Operation);
    public static implicit operator RealOperation(UnsignedOperation operation) => new(operation.Operation); 

  }
}
