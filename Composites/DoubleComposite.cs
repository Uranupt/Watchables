

namespace Watchables
{
  public sealed class DoubleComposite : CompositeWatchable<double>
  {

    protected override void ApplyClamp(double bound, bool upperBound)
    {
      if(upperBound)
      {
        _value = _value > bound ? bound : _value;
      }
      else
      {
        _value = _value < bound ? bound : _value;
      }
    }

    protected override void ApplyScaling(double value, bool divide)
    {
      if(value == 0)
      {
        _value = 0;
      }
      else
      {
        _value = divide ? _value / value : _value * value;
      }
    }

    protected override void ApplyTranslation(double value, bool subtract)
    {
      _value = subtract ? _value - value : value + value;
    }

  }
}