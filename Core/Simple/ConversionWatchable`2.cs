using System;


namespace Watchables
{
  /// <summary>
  /// An <see cref="IWatchable{T}"/> implementation which converts one <see cref="IConvertible"/> type to another.
  /// </summary>
  /// <typeparam name="TSource"> The type to convert from. </typeparam>
  /// <typeparam name="TValue"> The type to convert to. </typeparam>
  public sealed class ConversionWatchable<TSource, TValue> : NestedWatchable<TValue>
    where TSource : IConvertible
    where TValue : IConvertible
  {

    private IWatchable<TSource> _source;

    public ConversionWatchable(IWatchable<TSource> source)
    {
      _source = source;
      Register(_source);
    }

    /// <inheritdoc/>
    protected override bool CheckFatalDestruction(IWatchable dependency) => true;

    /// <inheritdoc/>
    protected override void Evaluate()
    {
      Value = (TValue)Convert.ChangeType(_source.Value, typeof(TValue));
    }

    /// <inheritdoc/>
    protected override void BeforeDestroyed()
    {
      Unregister(_source);
      _source = null;
    }
  }

}