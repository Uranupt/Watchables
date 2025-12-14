

namespace Watchables
{
  /// <summary>
  /// Represents a single operation in an <see cref="OperationChain{T}"/>. Must be constructed via
  /// <see cref="OperationBuilder"/>'s Step overload methods to ensure valid operations for the given type.
  /// </summary>
  /// <remarks> 
  /// While possible, it is not recommended to have the same <see cref="OperationStep{T}"/> in multiple 
  /// <see cref="OperationChain{T}"/> instances unless under strict oversight.
  /// </remarks>
  public sealed class OperationStep<T> : IWrapper<T>, IEnforceNumeric<T> where T : unmanaged
  {

    /// <summary> Whether this step is required for the <see cref="OperationChain{T}"/> it is a part of. </summary>
    public bool IsRequired { get; private set; }

    /// <summary> The source of the step's <typeparamref name="T"/> value. </summary>
    public IWrapper<T> ValueSource { get; private set; }

    /// <inheritdoc/>
    public T Value => ValueSource.Value;

    /// <summary> The operation this step performs. </summary>
    public NumericOperation Operation { get; private set; }

    internal OperationStep(NumericOperation op, IWrapper<T> value = null, bool required = false)
    {
      NumericUtility.ValidateType(typeof(T));
      Operation = op;
      ValueSource = value ?? default(T).Wrap();
      IsRequired = required;
    }

    public static implicit operator T(OperationStep<T> step) => step.Value;

  }
}