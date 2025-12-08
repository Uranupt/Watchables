

namespace Watchables
{
  /// <summary>
  /// Implementation of <see cref="CompositePartBase{TValue, TPart}"/> for <see cref="Tag{T}"/> types.
  /// </summary>
  public sealed class TagsCompositePart<T> : CompositePartBase<Tags<T>, TagsCompositePart<T>> where T : Tag<T>
  {

    /// <summary> Whether to add or remove the targeted <typeparamref name="T"/> values from the composite. </summary>
    public bool Add { get; private set; }

    /// <param name="add"> 
    /// Whether to add (<see langword="true"/>) or remove (<see langword="false"/>) the targeted <typeparamref name="T"/>
    /// values from the composite.
    /// </param>
    public TagsCompositePart(IWrapper<Tags<T>> value, bool add = true, CompositePartPriority priority = CompositePartPriority.None)
      : base(value, priority)
    {
      Add = add;
    }

  }
}
