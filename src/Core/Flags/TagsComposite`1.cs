

namespace Watchables
{
  /// <summary>
  /// Implementation of <see cref="CompositeBase{TValue, TPart}"/> for <see cref="Tag{T}"/> types. Parts which Remove
  /// values will override any Parts with the same <see cref="CompositePartPriority"/> which would Add the same values.
  /// </summary>
  public sealed class TagsComposite<T> : CompositeBase<Tags<T>, TagsCompositePart<T>> where T : Tag<T>
  {

    /// <inheritdoc/>
    protected override void Evaluate()
    {
      Value = default;
      foreach(TagsCompositePart<T> part in _parts)
      {
        Value = part.Add ? Value + part : Value - part;
      }
    }

    /// <inheritdoc/>
    protected override void Sort()
    {
      _parts.Sort(
        (x, y) =>
        {
          int resl = x.Priority.CompareTo(y.Priority);
          return resl != 0 ? resl : y.Add.CompareTo(x.Add);
        }
      );
    }

  }
}
