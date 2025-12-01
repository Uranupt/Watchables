using System;


namespace Watchables
{ 
  public sealed class ConversionWatchable<TSource, TValue> : ConversionWatchable<TValue>
    where TSource : unmanaged
    where TValue : unmanaged
  {

    private IWatchable<TSource> _source;

    public override Type SourceType => typeof(TSource);

    internal ConversionWatchable(IWatchable<TSource> source) : base()
    {
      _source = source;
      Register(_source);
    }

    protected override void Evaluate()
    {
      _value = (TValue)Convert.ChangeType(_source.ToValue(), typeof(TValue));
    }

    protected override void OnDestroyed(IWatchable self)
    {
      Unregister(_source);
      _source = null;
    }
  }

}