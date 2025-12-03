

namespace Watchables
{
  public sealed class OperationStep<T> where T : unmanaged
  {

    public bool IsRequired { get; private set; }
    public IValueWrapper<T> Value { get; private set; }
    internal NumericOperation Operation { get; private set; }

    internal OperationStep(NumericOperation op, IValueWrapper<T> value = null, bool required = false)
    {
      Operation = op;
      Value = value ?? default(T).Wrap();
      IsRequired = required;
    }

    ///// <summary> Creates a new Add value step. </summary> 
    ///// <param name="fatal"> If the value's destruction is fatal to the chain. </param>
    //public static OperationStep<T> Add(IValueWrapper<T> value, bool fatal = false) => new(OperationType.Add, value, fatal);

    ///// <summary> Creates a new Subtract value step. </summary> 
    ///// <param name="fatal"> If the value's destruction is fatal to the chain. </param>
    //public static OperationStep<T> Subtract(IValueWrapper<T> value, bool fatal = false) => new(OperationType.Subtract, value, fatal);

    ///// <summary> Creates a new Multiply by value step. </summary> 
    ///// <param name="fatal"> If the value's destruction is fatal to the chain. </param>
    //public static OperationStep<T> Multiply(IValueWrapper<T> value, bool fatal = false) => new(OperationType.Multiply, value, fatal);

    ///// <summary> Creates a new Divide by value step. </summary> 
    ///// <param name="fatal"> If the value's destruction is fatal to the chain. </param>
    //public static OperationStep<T> Divide(IValueWrapper<T> value, bool fatal = false) => new(OperationType.Divide, value, fatal);

    ///// <summary> Create a new Modulo by value step. </summary> 
    ///// <param name="fatal"> If the value's destruction is fatal to the chain. </param>
    //public static OperationStep<T> Modulo(IValueWrapper<T> value, bool fatal = false) => new(OperationType.Modulo, value, fatal);

    ///// <summary> Create a new step raising chain's current value to the power of the given value. </summary> 
    ///// <param name="fatal"> If the value's destruction is fatal to the chain. </param>
    //public static OperationStep<T> ToPower(IValueWrapper<T> value, bool fatal = false) => new(OperationType.ToPower, value, fatal);

    ///// <summary> Create a new step raising the given value to the power of the chain's current value. </summary> 
    ///// <param name="fatal"> If the value's destruction is fatal to the chain. </param>
    //public static OperationStep<T> AsPower(IValueWrapper<T> value, bool fatal = false) => new(OperationType.AsPower, value, fatal);

    ///// <summary> Create a new step taking the root of the chain's current value to the degree of the given value. Unsupported for integral types. </summary> 
    ///// <param name="fatal"> If the value's destruction is fatal to the chain. </param>
    //public static OperationStep<T> ToRoot(IValueWrapper<T> value, bool fatal = false) => new(OperationType.ToRoot, value, fatal);

    ///// <summary> Create a new step taking the root of the given value with the chain's current value as the degree. Unsupported for integral types. </summary> 
    ///// <param name="fatal"> If the value's destruction is fatal to the chain. </param>
    //public static OperationStep<T> AsRoot(IValueWrapper<T> value, bool fatal = false) => new(OperationType.AsRoot, value, fatal);

    ///// <summary> Create a new Minimum value step. </summary> 
    ///// <param name="fatal"> If the value's destruction is fatal to the chain. </param>
    //public static OperationStep<T> Minimum(IValueWrapper<T> value, bool fatal = false) => new(OperationType.Minimum, value, fatal);

    ///// <summary> Create a new Maximum value step. </summary> 
    ///// <param name="fatal"> If the value's destruction is fatal to the chain. </param>
    //public static OperationStep<T> Maximum(IValueWrapper<T> value, bool fatal = false) => new(OperationType.Maximum, value, fatal);

    ///// <summary> Create a new Round step. Unsupported for integral types.</summary>
    //public static OperationStep<T> Round() => new(OperationType.Round, false);
    ///// <summary> Create a new Floor step. Unsupported for integral types. </summary>
    //public static OperationStep<T> Floor() => new(OperationType.Floor, false);
    ///// <summary> Create a new Ceiling step. Unsupported for integral types. </summary>
    //public static OperationStep<T> Ceiling() => new(OperationType.Ceiling, false);
    ///// <summary> Create a new Truncate step. Unsupported for integral types. </summary>
    //public static OperationStep<T> Truncate() => new(OperationType.Truncate, false);
    ///// <summary> Create a new Absolute Value step. </summary>
    //public static OperationStep<T> Absolute() => new(OperationType.Absolute, false);
    ///// <summary> Create a new step forcing the chain's value to be negative. </summary>
    //public static OperationStep<T> AsNegative() => new(OperationType.AsNegative, false);
    ///// <summary> Create a new step flipping the chain's sign. </summary>
    //public static OperationStep<T> FlipSign() => new(OperationType.FlipSign, false);
    ///// <summary> Create a new step taking the reciprocal of the chain's current value. Unsupported for integral types. </summary>
    //public static OperationStep<T> Reciprocal() => new(OperationType.Reciprocal, false);

  }
}