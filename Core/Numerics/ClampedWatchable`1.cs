using System;


namespace Watchables
{
  /// <summary>
  /// An <see cref="IWatchable{T}"/> implementation wrapping an <see cref="OperationChain{T}"/> that clamps an internally managed value.
  /// Provides methods to directly set, add to, or subtract from the internal value.
  /// </summary>
  public sealed class ClampedWatchable<T> : SealableNestedBase<T>, IEnforceNumeric<T> where T : unmanaged
  {

    private OperationChain<T> _chain;
    private BasicWatchable<T> _baseValue;

    public IWrapper<T> Minimum { get; private set; }
    public IWrapper<T> Maximum { get; private set; }

    public ClampedWatchable(IWrapper<T> min, IWrapper<T> max)
    {
      NumericUtility.ValidateType(typeof(T));
      Minimum = min;
      Maximum = max;
      _baseValue = new BasicWatchable<T>();
      _chain = OperationBuilder.Clamp(_baseValue, Minimum, Maximum);
      Register(_chain);
    }

    /// <summary> Attempts to set the <paramref name="value"/>. </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool Set(T value, object owner = null) => MutationGuard(() => _baseValue.SetValue(value), owner);

    /// <summary> Attempts to add the <paramref name="value"/>. </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool Add(T value, object owner = null) => Set(Value.Add(value), owner);

    /// <summary> Attempts to subtract the <paramref name="value"/>. </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool Subtract(T value, object owner = null) => Set(Value.Subtract(value), owner);

    /// <inheritdoc cref="Subtract"/>
    public bool Sub(T value, object owner = null) => Subtract(value, owner);

    /// <summary> Attempts to set the value to the upper bound value of <see cref="Maximum"/>. </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool Fill(object owner = null) => Set(Maximum.Value, owner);

    /// <summary> Attempts to set the value to the lower bound value of <see cref="Minimum"/>. </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool Empty(object owner = null) => Set(Minimum.Value, owner);

    /// <inheritdoc/>
    protected override bool CheckFatalDestruction(IWatchable dependency) => true;

    /// <inheritdoc/>
    protected override void Evaluate() => Value = _chain;

    /// <inheritdoc/>
    protected override void BeforeDestroyed()
    {
      Unregister(_chain);
      _chain.Destroy();
      _baseValue.Destroy();
      _chain = null;
      _baseValue = null;
      Minimum = null;
      Maximum = null;
    }

  }
}
