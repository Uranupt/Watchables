using System;


namespace Watchables
{
  public sealed class CompositePart<T> : CompositePartBase<T, CompositePart<T>>, IEnforceNumeric<T> where T : unmanaged
  {

    public CompositeOperation Operation { get; private set; }

    public CompositePart(CompositeOperation op, IValueWrapper<T> value, CompositePartPriority priority) : base(value, priority)
    {
      NumericUtility.ValidateType(typeof(T));
      Operation = op;
    }

  }

}