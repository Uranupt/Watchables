

namespace Watchables
{
  public sealed class OpChainStep<T> where T : unmanaged
  {

    public bool IsDestructionFatal { get; private set; }
    public IValueWrapper<T> Value { get; private set; }
    public OperationType OperationType { get; private set; }

    private OpChainStep(OperationType op, bool fatal)
    {
      OperationType = op;
      IsDestructionFatal = fatal;
    }

    private OpChainStep(OperationType op, IValueWrapper<T> value, bool fatal)
    {
      OperationType = op;
      Value = value;
      IsDestructionFatal = fatal;
    }

    public static OpChainStep<T> Add(IValueWrapper<T> value, bool fatal = false) => new(OperationType.Add, value, fatal);
    public static OpChainStep<T> Substract(IValueWrapper<T> value, bool fatal = false) => new(OperationType.Subtract, value, fatal);
    public static OpChainStep<T> Multiply(IValueWrapper<T> value, bool fatal = false) => new(OperationType.Multiply, value, fatal);
    public static OpChainStep<T> Divide(IValueWrapper<T> value, bool fatal = false) => new(OperationType.Divide, value, fatal);
    public static OpChainStep<T> Modulo(IValueWrapper<T> value, bool fatal = false) => new(OperationType.Modulo, value, fatal);
    public static OpChainStep<T> ToPower(IValueWrapper<T> value, bool fatal = false) => new(OperationType.ToPower, value, fatal);
    public static OpChainStep<T> AsPower(IValueWrapper<T> value, bool fatal = false) => new(OperationType.AsPower, value, fatal);
    public static OpChainStep<T> ToRoot(IValueWrapper<T> value, bool fatal = false) => new(OperationType.ToRoot, value, fatal);
    public static OpChainStep<T> AsRoot(IValueWrapper<T> value, bool fatal = false) => new(OperationType.AsRoot, value, fatal);
    public static OpChainStep<T> Minimum(IValueWrapper<T> value, bool fatal = false) => new(OperationType.Minimum, value, fatal);
    public static OpChainStep<T> Maximum(IValueWrapper<T> value, bool fatal = false) => new(OperationType.Maximum, value, fatal);
    public static OpChainStep<T> Round(bool fatal = false) => new(OperationType.Round, fatal);
    public static OpChainStep<T> Floor(bool fatal = false) => new(OperationType.Floor, fatal);
    public static OpChainStep<T> Ceiling(bool fatal = false) => new(OperationType.Ceiling, fatal);
    public static OpChainStep<T> Truncate(bool fatal = false) => new(OperationType.Truncate, fatal);
    public static OpChainStep<T> Absolute(bool fatal = false) => new(OperationType.Absolute, fatal);
    public static OpChainStep<T> AsNegative(bool fatal = false) => new(OperationType.AsNegative, fatal);
    public static OpChainStep<T> FlipSign(bool fatal = false) => new(OperationType.FlipSign, fatal);
    public static OpChainStep<T> Reciprocal(bool fatal = false) => new(OperationType.Reciprocal, fatal);

  }
}