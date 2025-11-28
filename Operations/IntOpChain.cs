

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

    protected override bool CheckValidOperation(OperationType op)
    {
      return op switch
      {
        OperationType.ToRoot or
        OperationType.AsRoot or
        OperationType.Round or
        OperationType.Floor or
        OperationType.Ceiling or
        OperationType.Truncate or
        OperationType.Reciprocal => false,
        _ => true
      };
    }
  }
}