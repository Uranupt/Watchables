using System;


namespace Watchables
{ 
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

    protected override bool CheckFatalDestruction(IWatchable dependency) => true;

    protected override void Evaluate()
    {
      _value =
      _value = (TValue)Convert.ChangeType(_source.ToValue(), typeof(TValue));
    }

    protected override void OnDestroyed(IWatchable self)
    {
      Unregister(_source);
      _source = null;
    }
  }

}