

namespace Watchables
{
  public sealed class ClampedFloat : ClampedWatchable<float>
  {

    public ClampedFloat(IValueWrapper<float> minimum, IValueWrapper<float> maximum) : base(minimum, maximum)
    {

    }

    protected override float Add(float input, float value) => input + value;

    protected override float Subtract(float input, float value) => input - value;

    protected override float Clamp(float input, float min, float max) => input < min ? min : (input > max ? max : input);

  }
}
