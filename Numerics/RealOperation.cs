

namespace Watchables
{
  public readonly struct RealOperation
  {

    internal readonly NumericOperation Operation;

    public static RealOperation Add => new(NumericOperation.Add);
    public static RealOperation Subtract => new(NumericOperation.Subtract);
    public static RealOperation Multiply => new(NumericOperation.Multiply);
    public static RealOperation Divide => new(NumericOperation.Divide);
    public static RealOperation Modulo => new(NumericOperation.Modulo);
    public static RealOperation Power => new(NumericOperation.Power);
    public static RealOperation Root => new(NumericOperation.Root);
    public static RealOperation Minimum => new(NumericOperation.Minimum);
    public static RealOperation Maximum => new(NumericOperation.Maximum);
    public static RealOperation Round => new(NumericOperation.Round);
    public static RealOperation Floor => new(NumericOperation.Floor);
    public static RealOperation Ceiling => new(NumericOperation.Ceiling);
    public static RealOperation Truncate => new(NumericOperation.Truncate);
    public static RealOperation Absolute => new(NumericOperation.Absolute);
    public static RealOperation AsNegative => new(NumericOperation.AsNegative);
    public static RealOperation FlipSign => new(NumericOperation.FlipSign);
    public static RealOperation Reciprocal => new(NumericOperation.Reciprocal);

    internal RealOperation(NumericOperation operation)
    {
      Operation = operation; 
    }

  }
}
