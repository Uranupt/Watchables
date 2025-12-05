

namespace Watchables
{
  public sealed class TagsCompositePart<T> : CompositePartBase<Tags<T>, TagsCompositePart<T>> where T : Tag<T>
  {

    public bool SetTo { get; private set; }

    public TagsCompositePart(IValueWrapper<Tags<T>> value, bool setTo = true, CompositePartPriority priority = CompositePartPriority.None)
      : base(value, priority)
    {
      SetTo = setTo;
    }

  }
}
