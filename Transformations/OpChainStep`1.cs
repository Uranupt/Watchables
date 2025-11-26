

namespace Watchables
{
  public class OpChainStep<T> where T : unmanaged
  {

    public enum OpType
    {
      Add,
      Subtract,
      Multiply,
      Divide,
      Exponent,
      Round
    }

    public bool IsDestructionFatal { get; private set; }
    public IValueWrapper<T> Value { get; private set; }
    public OpType Op { get; private set; }

    private OpChainStep(OpType op, bool fatal)
    {
      Op = op;
      IsDestructionFatal = fatal;
    }

    private OpChainStep(OpType op, IValueWrapper<T> value, bool fatal)
    {
      Op = op;
      Value = value;
      IsDestructionFatal = fatal;
    }

    public static OpChainStep<T> Add(IValueWrapper<T> value, bool fatal = false) => new OpChainStep<T>(OpType.Add, value, fatal);
    public static OpChainStep<T> Substract(IValueWrapper<T> value, bool fatal = false) => new OpChainStep<T>(OpType.Subtract, value, fatal);
    public static OpChainStep<T> Multiply(IValueWrapper<T> value, bool fatal = false) => new OpChainStep<T>(OpType.Multiply, value, fatal);
    public static OpChainStep<T> Divide(IValueWrapper<T> value, bool fatal = false) => new OpChainStep<T>(OpType.Divide, value, fatal);
    public static OpChainStep<T> Exponent(IValueWrapper<T> value, bool fatal = false) => new OpChainStep<T>(OpType.Exponent, value, fatal);
    public static OpChainStep<T> Round(bool fatal = false) => new OpChainStep<T>(OpType.Round, fatal);


  }
}