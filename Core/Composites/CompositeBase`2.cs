using System.Collections.Generic;


namespace Watchables
{
  /// <summary>
  /// Extension base class of <see cref="CompositeBase{T}"/> which implements <see cref="IWatchable{T}"/>, adding value definition.
  /// </summary>
  /// <typeparam name="TValue"> The contained value type. </typeparam>
  /// <typeparam name="TPart"> The type of part used to compose. </typeparam>
  public abstract class CompositeBase<TValue, TPart> : CompositeBase<TPart>, IWatchable<TValue> where TPart : CompositePartBase<TValue, TPart>
  {

    private ReadOnlyWatchable<TValue> _readOnlyWrapper;

    /// <summary> A read-only version of this instance. </summary>
    public ReadOnlyWatchable<TValue> ReadOnlyWrapper => _readOnlyWrapper ??= new(this);

    /// <inheritdoc/>
    public TValue Value { get; protected set; }

    public static implicit operator TValue(CompositeBase<TValue, TPart> watchable) => watchable.Value;

    /// <summary> Returns a string representation of the underlying value. </summary>
    public override string ToString() => Value.ToString();

    /// <inheritdoc/>
    protected override void OnPartAdded(TPart part)
    {
      if(part.ValueSource is IWatchable)
      {
        (part.ValueSource as IWatchable).Changed += InvokeChanged;
      }
    }

    /// <inheritdoc/>
    protected override void OnPartRemoved(TPart part)
    {
      if(part.ValueSource is IWatchable)
      {
        (part.ValueSource as IWatchable).Changed -= InvokeChanged;
      }
    }

    /// <inheritdoc/>
    protected override void BeforeChanged()
    {
      Evaluate();
    }

    /// <inheritdoc/>
    protected override void ClearValue() => Value = default;

    /// <summary> 
    /// This method defines the behavior around setting the instance's value when an internal change occurs. 
    /// Never call <see cref="WatchableBase.InvokeChanged"/> from within this method as it will cause recursion. 
    /// </summary>
    protected abstract void Evaluate();

  }
}
