

namespace Watchables
{
  public sealed class TagsComposite<T> : CompositeBase<Tags<T>, TagsCompositePart<T>> where T : Tag<T>
  {

    protected override void Evaluate()
    {
      _value = default;
      foreach(TagsCompositePart<T> part in _parts)
      {
        _value = part.SetTo ? _value + part : _value - part;
      }
    }

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
