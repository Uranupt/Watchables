using System;


namespace Watchables
{
  /// <summary>
  /// Implementation of <see cref="CompositeBase{TValue, TPart}"/> for <see cref="FlagsAttribute"/> marked <see cref="Enum"/>s. 
  /// Parts which set to Off will override Parts which set to On and have the same <see cref="CompositePartPriority"/> and target the same bits.
  /// </summary>
  public sealed class FlagsComposite<T> : CompositeBase<T, FlagsCompositePart<T>> where T : struct, Enum
  {

    /// <inheritdoc/>
    protected override void Evaluate()
    {
      ulong val = 0;
      foreach(FlagsCompositePart<T> part in _parts)
      {
        val = part.SetTo ? val | part.Value.ToULong() : val & ~part.Value.ToULong();
      }
      Value = val.ToEnum<T>();
    }

    /// <inheritdoc/>
    protected override void Sort()
    {
      _parts.Sort(
        (x, y) =>
        {
          int resl = x.Priority.CompareTo(y.Priority);
          return resl != 0 ? resl : y.SetTo.CompareTo(x.SetTo);
        }
      );
    }

  }
}
