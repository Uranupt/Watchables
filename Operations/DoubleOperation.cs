

namespace Watchables
{
  public sealed class DoubleOperation : Operation<double>
  {

    private DoubleOperation(OperationType op, IValueWrapper<double> baseValue, IValueWrapper<double> operand = null) : base(op, baseValue, operand)
    {

    }

    public static DoubleOperation Add(IValueWrapper<double> value, IValueWrapper<double> operand) => new(OperationType.Add, value, operand);
    public static DoubleOperation Substract(IValueWrapper<double> value, IValueWrapper<double> operand) => new(OperationType.Subtract, value, operand);
    public static DoubleOperation Multiply(IValueWrapper<double> value, IValueWrapper<double> operand) => new(OperationType.Multiply, value, operand);
    public static DoubleOperation Divide(IValueWrapper<double> value, IValueWrapper<double> operand) => new(OperationType.Divide, value, operand);
    public static DoubleOperation Modulo(IValueWrapper<double> value, IValueWrapper<double> operand) => new(OperationType.Modulo, value, operand);
    public static DoubleOperation ToPower(IValueWrapper<double> value, IValueWrapper<double> operand) => new(OperationType.ToPower, value, operand);
    public static DoubleOperation AsPower(IValueWrapper<double> value, IValueWrapper<double> operand) => new(OperationType.AsPower, value, operand);
    public static DoubleOperation ToRoot(IValueWrapper<double> value, IValueWrapper<double> operand) => new(OperationType.ToRoot, value, operand);
    public static DoubleOperation AsRoot(IValueWrapper<double> value, IValueWrapper<double> operand) => new(OperationType.AsRoot, value, operand);
    public static DoubleOperation Minimum(IValueWrapper<double> value, IValueWrapper<double> operand) => new(OperationType.Minimum, value, operand);
    public static DoubleOperation Maximum(IValueWrapper<double> value, IValueWrapper<double> operand) => new(OperationType.Maximum, value, operand);
    public static DoubleOperation Round(IValueWrapper<double> value) => new(OperationType.Round, value);
    public static DoubleOperation Floor(IValueWrapper<double> value) => new(OperationType.Floor, value);
    public static DoubleOperation Ceiling(IValueWrapper<double> value) => new(OperationType.Ceiling, value);
    public static DoubleOperation Truncate(IValueWrapper<double> value) => new(OperationType.Truncate, value);
    public static DoubleOperation Absolute(IValueWrapper<double> value) => new(OperationType.Absolute, value);
    public static DoubleOperation AsNegative(IValueWrapper<double> value) => new(OperationType.AsNegative, value);
    public static DoubleOperation FlipSign(IValueWrapper<double> value) => new(OperationType.FlipSign, value);
    public static DoubleOperation Reciprocal(IValueWrapper<double> value) => new(OperationType.Reciprocal, value);

    protected override void Evaluate()
    {
      _value = _operation.Operate(_base.ToValue(), _operand != null ? _operand.ToValue() : 0);
      InvokeChanged();
    }

  }
}