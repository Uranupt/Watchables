

namespace Watchables
{
  public sealed class ClampedDouble : ClampedWatchable<double>
  {

    public ClampedDouble(IValueWrapper<double> minimum, IValueWrapper<double> maximum) : base(minimum, maximum)
    {

    }

    protected override double Add(double input, double value) => input + value;

    protected override double Subtract(double input, double value) => input - value;

    protected override double Clamp(double input, double min, double max) => input < min ? min : (input > max ? max : input);

  }
}