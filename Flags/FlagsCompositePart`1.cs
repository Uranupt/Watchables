using System;


namespace Watchables
{
  public sealed class FlagsCompositePart<T> : CompositePartBase<T, FlagsCompositePart<T>> where T : struct, Enum
  {

    public bool SetTo { get; private set;  }

    public FlagsCompositePart(IValueWrapper<T> value,  bool setTo = true, CompositePartPriority priority = CompositePartPriority.None)
      : base(value, priority)
    {
      SetTo = setTo;
    }

  }
}
