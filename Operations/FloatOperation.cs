using System;


namespace Watchables
{
  public sealed class FloatOperation : Operation<float>
  {

    private FloatOperation(OperationType op, IValueWrapper<float> baseValue, IValueWrapper<float> operand = null) : base(op, baseValue, operand)
    {

    }

    public static FloatOperation Add(IValueWrapper<float> value, IValueWrapper<float> operand) => new(OperationType.Add, value, operand);
    public static FloatOperation Substract(IValueWrapper<float> value, IValueWrapper<float> operand) => new(OperationType.Subtract, value, operand);
    public static FloatOperation Multiply(IValueWrapper<float> value, IValueWrapper<float> operand) => new(OperationType.Multiply, value, operand);
    public static FloatOperation Divide(IValueWrapper<float> value, IValueWrapper<float> operand) => new(OperationType.Divide, value, operand);
    public static FloatOperation Modulo(IValueWrapper<float> value, IValueWrapper<float> operand) => new(OperationType.Modulo, value, operand);
    public static FloatOperation ToPower(IValueWrapper<float> value, IValueWrapper<float> operand) => new(OperationType.ToPower, value, operand);
    public static FloatOperation AsPower(IValueWrapper<float> value, IValueWrapper<float> operand) => new(OperationType.AsPower, value, operand);
    public static FloatOperation ToRoot(IValueWrapper<float> value, IValueWrapper<float> operand) => new(OperationType.ToRoot, value, operand);
    public static FloatOperation AsRoot(IValueWrapper<float> value, IValueWrapper<float> operand) => new(OperationType.AsRoot, value, operand);
    public static FloatOperation Minimum(IValueWrapper<float> value, IValueWrapper<float> operand) => new(OperationType.Minimum, value, operand);
    public static FloatOperation Maximum(IValueWrapper<float> value, IValueWrapper<float> operand) => new(OperationType.Maximum, value, operand);
    public static FloatOperation Round(IValueWrapper<float> value) => new(OperationType.Round, value);
    public static FloatOperation Floor(IValueWrapper<float> value) => new(OperationType.Floor, value);
    public static FloatOperation Ceiling(IValueWrapper<float> value) => new(OperationType.Ceiling, value);
    public static FloatOperation Truncate(IValueWrapper<float> value) => new(OperationType.Truncate, value);
    public static FloatOperation Absolute(IValueWrapper<float> value) => new(OperationType.Absolute, value);
    public static FloatOperation AsNegative(IValueWrapper<float> value) => new(OperationType.AsNegative, value);
    public static FloatOperation FlipSign(IValueWrapper<float> value) => new(OperationType.FlipSign, value);
    public static FloatOperation Reciprocal(IValueWrapper<float> value) => new(OperationType.Reciprocal, value);

    protected override void Evaluate()
    {
      _value = _operation.Operate(_base.ToValue(), _operand != null ? _operand.ToValue() : 0f);
      InvokeChanged();
    }

  }
}