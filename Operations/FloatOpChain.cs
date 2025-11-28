using System;


namespace Watchables
{
  public sealed class FloatOpChain : OpChain<float>
  {

    public FloatOpChain(IValueWrapper<float> baseValue) : base(baseValue)
    {

    }

    protected override void ApplyOperation(OpChainStep<float> step)
    {
      _value = step.OperationType.Operate(_value, step.Value != null ? step.Value.ToValue() : 0f );
    }

  }
}