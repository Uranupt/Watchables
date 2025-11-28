

namespace Watchables
{
  public sealed class DoubleOpChain : OpChain<double>
  {

    public DoubleOpChain(IValueWrapper<double> baseValue) : base(baseValue)
    {

    }

    protected override void ApplyOperation(OpChainStep<double> step)
    {
      _value = step.OperationType.Operate(_value, step.Value != null ? step.Value.ToValue() : 0);
    }

  }
}