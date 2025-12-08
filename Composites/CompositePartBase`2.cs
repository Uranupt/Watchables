using System;


namespace Watchables
{
  /// <summary>
  /// Extension base class of <see cref="CompositePartBase{T}"/>, adding value definition by implementing <see cref="IWrapper{T}"/>.
  /// </summary>
  /// <typeparam name="TValue"> The contained value type. </typeparam>
  /// <typeparam name="TSelf"> The self-referential type. </typeparam>
  public abstract class CompositePartBase<TValue, TSelf> : CompositePartBase<TSelf>, IWrapper<TValue> where TSelf : CompositePartBase<TValue, TSelf>
  {

    /// <summary> The source of the part's <typeparamref name="TValue"/> value. </summary>
    public IWrapper<TValue> ValueSource { get; protected set;  }

    /// <inheritdoc/>
    public TValue Value => IsValid ? ValueSource.Value : default;

    /// <summary> Whether the part's <see cref="ValueSource"/> is still present. </summary>
    public bool IsValid => ValueSource != null;

    protected CompositePartBase(IWrapper<TValue> value, CompositePartPriority priority) : base(priority)
    {
      ValueSource = value;
      if(value is IWatchable watchable)
      {
        watchable.Destroyed += OnValueDestroyed;
      }
    }

    public static implicit operator TValue(CompositePartBase<TValue, TSelf> part) => part.Value;

    /// <inheritdoc/>
    public override bool SetOwner(object owner)
    {
      if(!IsValid) { return false; }
      return base.SetOwner(owner);
    }

    /// <inheritdoc/>
    public override bool ClearOwner(object owner)
    {
      if(!IsValid) { return false; }
      return base.ClearOwner(owner);
    }

    private void OnValueDestroyed(IWatchable value)
    {
      value.Destroyed -= OnValueDestroyed;
      Remove(_owner);
      ClearOwner(_owner);
      ValueSource = null;
    }

  }
}