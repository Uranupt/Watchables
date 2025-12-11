using System;


namespace Watchables
{
  /// <summary> 
  /// Numeric implementation of <see cref="CompositePartBase{T, CompositePart{T}}"/> for use with <see cref="CompositeWatchable{T}"/>.
  /// </summary>
  public sealed class CompositePart<T> : CompositePartBase<T, CompositePart<T>>, IEnforceNumeric<T> where T : unmanaged
  {

    /// <summary> The operation to perform. </summary>
    public CompositeOperation Operation { get; private set; }

    public CompositePart(CompositeOperation op, IWrapper<T> value, CompositePartPriority priority) : base(value, priority)
    {
      NumericUtility.ValidateType(typeof(T));
      Operation = op;
    }

  }

}