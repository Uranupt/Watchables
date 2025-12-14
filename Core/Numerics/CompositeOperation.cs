using System;


namespace Watchables
{
  /// <summary>
  /// <see cref="Enum"/> defining the operation a <see cref="CompositePart{T}"/> represents.
  /// </summary>
  public enum CompositeOperation
  {
    /// <summary> Force the final value. This bypasses clamping. </summary>
    Force,
    /// <summary> Set the final value, ignoring translation and scaling. Obeys clamping. </summary>
    SetFinal,
    /// <summary> Set the starting value. </summary>
    SetBase,
    /// <summary> Add to the value. </summary>
    Add,
    /// <summary> Remove from the value. </summary>
    Subtract,
    /// <summary> Multiply against the value. </summary>
    Multiply,
    /// <summary> Divide the value. </summary>
    Divide,
    /// <summary> Define a minimum value to check against after translation and scaling. </summary>
    Minimum,
    /// <summary> Define a maximum value to check against after translation and scaling, and after minimum. </summary>
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
        CompositeOperation.Minimum => NumericOperation.GreaterOf,
        CompositeOperation.Maximum => NumericOperation.LesserOf,
        _ => throw new ArgumentException($"Invalid CompositeOperation value to cast to NumericOperation: {operation}")
      };
    }

  }
}