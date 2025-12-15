using System;


namespace Watchables
{
  /// <summary>
  /// <see cref="Enum"/> which defines generic math operations.
  /// </summary>
  public enum NumericOperation
  {
    /// <summary> Add two values. </summary>
    Add,
    /// <summary> Subtract a value from another. </summary>
    Subtract,
    /// <summary> Multiply two values. </summary>
    Multiply,
    /// <summary> Divide a value by another. </summary>
    Divide,
    /// <summary> Return the remainder of the division of a value by another. </summary>
    Modulo,
    /// <summary> Raise a value to the power of another. </summary>
    Power,
    /// <summary> Return the root of a value to the degree of another. </summary>
    Root,
    /// <summary> Return the larger of two values. </summary>
    GreaterOf,
    /// <summary> Return the smaller of two values. </summary>
    LesserOf,
    /// <summary> Round a value to nearest integer. </summary>
    Round,
    /// <summary> Round a value downwards. </summary>
    Floor,
    /// <summary> Round a value upwards. </summary>
    Ceiling,
    /// <summary> Remove any fractional portion of a value. </summary>
    Truncate,
    /// <summary> Return the non-negative magnitude of a value (distance from 0). </summary>
    Absolute,
    /// <summary> Return the value as a negative with the same magnitude. </summary>
    AsNegative,
    /// <summary> Flip the sign of a value. </summary>
    FlipSign,
    /// <summary> Return 1 divided by a value. </summary>
    Reciprocal
  }
}