using System;


namespace Watchables
{
  public sealed class ConversionWrapper<TSource, TValue> : IValueWrapper<TValue>
    where TSource : IConvertible
    where TValue : IConvertible
  {

    private readonly IValueWrapper<TSource> _source;

    public ConversionWrapper(IValueWrapper<TSource> source)
    {
      _source = source;
    }

    public TValue ToValue() => (TValue)Convert.ChangeType(_source.ToValue(), typeof(TValue));

  }
}