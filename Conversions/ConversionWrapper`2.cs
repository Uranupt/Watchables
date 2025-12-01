using System;


namespace Watchables
{
  public sealed class ConversionWrapper<TSource, TValue> : ConversionWrapper<TValue>
    where TSource : unmanaged
    where TValue : unmanaged
  {

    private readonly IValueWrapper<TSource> _source;

    public override Type SourceType => typeof(TSource);

    internal ConversionWrapper(IValueWrapper<TSource> source) : base()
    {
      _source = source;
    }

    public override TValue ToValue() => (TValue)Convert.ChangeType(_source.ToValue(), typeof(TValue));

  }
}