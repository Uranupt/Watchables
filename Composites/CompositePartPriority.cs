using System;


namespace Watchables
{
  /// <summary>
  /// <see cref="Enum"/> used for defining priority for sorting <see cref="CompositePartBase{TValue, TSelf}"/> instances in 
  /// <see cref="CompositeBase{TValue, TPart}"/> implementations.
  /// </summary>
  public enum CompositePartPriority : byte
  {
    None,
    Low,
    Medium,
    High,
    Critical,
    Override,
    Absolute
  }
}