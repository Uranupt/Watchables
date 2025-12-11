using System;


namespace Watchables
{
  /// <summary>
  /// An <see cref="IWrapper{T}"/> implementation which converts one <see cref="IConvertible"/> type to another.
  /// </summary>
  /// <typeparam name="TSource"> The type to convert from. </typeparam>
  /// <typeparam name="TValue"> The type to convert to. </typeparam>
  public sealed class ConversionWrapper<TSource, TValue> : IWrapper<TValue>
    where TSource : IConvertible
    where TValue : IConvertible
  {

    private readonly IWrapper<TSource> _source;

    /// <inheritdoc/>
    public TValue Value => (TValue)Convert.ChangeType(_source.Value, typeof(TValue));

    public ConversionWrapper(IWrapper<TSource> source)
    {
      _source = source;
    }

    public static implicit operator TValue(ConversionWrapper<TSource, TValue> wrapper) => wrapper.Value;

  }
}