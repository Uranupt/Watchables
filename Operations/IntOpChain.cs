

namespace Watchables
{
  public sealed class IntOpChain : OpChain<int>
  {

    public IntOpChain(IValueWrapper<int> baseValue) : base(baseValue)
    {

    }

    protected override void ApplyOperation(OpChainStep<int> step)
    {
      _value = step.OperationType.Operate(_value, step.Value != null ? step.Value.ToValue() : 0);
    }

  }
}