using System;


namespace Watchables
{
  public sealed class NumericCompositePart<T> : CompositeValueBase<T>, IEnforceNumeric<T> where T : unmanaged
  {

    public CompositeOperation Operation { get; private set; }

    public NumericCompositePart(CompositeOperation op, IValueWrapper<T> value, CompositePartPriority priority) : base(value, priority)
    {
      NumericUtility.ValidateType(typeof(T));
      Operation = op;
    }

  }

}