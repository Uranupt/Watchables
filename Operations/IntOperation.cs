

namespace Watchables
{
  public sealed class IntOperation : Operation<int>
  {

    private IntOperation(OperationType op, IValueWrapper<int> baseValue, IValueWrapper<int> operand = null) : base(op, baseValue, operand)
    {

    }

    public static IntOperation Add(IValueWrapper<int> value, IValueWrapper<int> operand) => new(OperationType.Add, value, operand);
    public static IntOperation Substract(IValueWrapper<int> value, IValueWrapper<int> operand) => new(OperationType.Subtract, value, operand);
    public static IntOperation Multiply(IValueWrapper<int> value, IValueWrapper<int> operand) => new(OperationType.Multiply, value, operand);
    public static IntOperation Divide(IValueWrapper<int> value, IValueWrapper<int> operand) => new(OperationType.Divide, value, operand);
    public static IntOperation Modulo(IValueWrapper<int> value, IValueWrapper<int> operand) => new(OperationType.Modulo, value, operand);
    public static IntOperation ToPower(IValueWrapper<int> value, IValueWrapper<int> operand) => new(OperationType.ToPower, value, operand);
    public static IntOperation AsPower(IValueWrapper<int> value, IValueWrapper<int> operand) => new(OperationType.AsPower, value, operand);
    public static IntOperation Minimum(IValueWrapper<int> value, IValueWrapper<int> operand) => new(OperationType.Minimum, value, operand);
    public static IntOperation Maximum(IValueWrapper<int> value, IValueWrapper<int> operand) => new(OperationType.Maximum, value, operand);
    public static IntOperation Absolute(IValueWrapper<int> value) => new(OperationType.Absolute, value);
    public static IntOperation AsNegative(IValueWrapper<int> value) => new(OperationType.AsNegative, value);
    public static IntOperation FlipSign(IValueWrapper<int> value) => new(OperationType.FlipSign, value);

    protected override void Evaluate()
    {
      _value = _operation.Operate(_base.ToValue(), _operand != null ? _operand.ToValue() : 0);
      InvokeChanged();
    }

  }
}