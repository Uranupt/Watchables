using System;


namespace Watchables
{
  public enum CompositeOperation
  {
    Force,
    SetFinal,
    SetBase,
    Add,
    Subtract,
    Multiply,
    Divide,
    Minimum,
    Maximum
  }

  internal static class CompositeOperationExtensions
  {

    internal static NumericOperation ToNumeric(this CompositeOperation operation)
    {
      return operation switch
      {
        CompositeOperation.Add => NumericOperation.Add,
        CompositeOperation.Subtract => NumericOperation.Subtract,
        CompositeOperation.Multiply => NumericOperation.Multiply,
        CompositeOperation.Divide => NumericOperation.Divide,
        CompositeOperation.Minimum => NumericOperation.Minimum,
        CompositeOperation.Maximum => NumericOperation.Maximum,
        _ => throw new ArgumentException($"Invalid CompositeOperation value to cast to NumericOperation: {operation.ToString()}")
      };
    }

  }
}