

namespace Watchables
{
  public sealed class IntComposite : CompositeWatchable<int>
  {

    protected override void ApplyClamp(int bound, bool upperBound)
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

    protected override void ApplyScaling(int value, bool divide)
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

    protected override void ApplyTranslation(int value, bool subtract)
    {
      _value = subtract ? _value - value : value + value;
    }

  }
}