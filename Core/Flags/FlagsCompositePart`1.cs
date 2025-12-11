using System;


namespace Watchables
{
  /// <summary>
  /// Implementation of <see cref="CompositePartBase{TValue, TPart}"/> for <see cref="FlagsAttribute"/> marked <see cref="Enum"/>s. 
  /// </summary>
  public sealed class FlagsCompositePart<T> : CompositePartBase<T, FlagsCompositePart<T>> where T : struct, Enum
  {

    /// <summary> The value to set the targeted bits to. </summary>
    public bool SetTo { get; private set;  }

    /// <param name="setTo"> Whether to set the targeted bits on (<see langword="true"/>) or off (<see langword="false"/>)</param>
    public FlagsCompositePart(IWrapper<T> value,  bool setTo = true, CompositePartPriority priority = CompositePartPriority.None)
      : base(value, priority)
    {
      SetTo = setTo;
    }

  }
}
