

namespace Watchables
{ 
  public sealed class ClampedInt : ClampedWatchable<int>
  {

    public ClampedInt(IValueWrapper<int> minimum, IValueWrapper<int> maximum) : base(minimum, maximum)
    {

    }

    protected override int Add(int input, int value) => input + value;

    protected override int Subtract(int input, int value) => input - value;

    protected override int Clamp(int input, int min, int max) => input < min ? min : (input > max ? max : input);

  }
}
