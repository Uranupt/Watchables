

namespace Watchables
{
  /// <summary>
  /// Represents a single operation defined by <see cref="NumericOperation"/>. Must be constructed via <see cref="OperationBuilder"/>'s 
  /// Watchable overload methods to ensure valid operations for the given type.
  /// </summary>
  public sealed class OperationWatchable<T> : NestedWatchable<T>, IEnforceNumeric<T> where T : unmanaged
  {

    private IWrapper<T> _base;
    private IWrapper<T> _operand;

    /// <summary> The operation this instance performs. </summary>
    public readonly NumericOperation Operation;

    internal OperationWatchable(NumericOperation op, IWrapper<T> baseValue, IWrapper<T> operand = null)
    {
      NumericUtility.ValidateType(typeof(T));
      Operation = op;
      _base = baseValue;
      _operand = operand ?? new ReadOnlyWrapper<T>(default);
      if(_base is IWatchable)
      {
        Register(_base as IWatchable);
      }
      if(_operand is IWatchable)
      {
        Register(_operand as IWatchable);
      }
    }

    /// <inheritdoc/>
    protected override void Evaluate()
    {
      Value = _base.Value.Operate(Operation, _operand.Value, true);
    }

    /// <inheritdoc/>
    protected override bool CheckFatalDestruction(IWatchable dependency)
    {
      return dependency == _base || dependency == _operand;
    }

    /// <inheritdoc/>
    protected override void BeforeDestroyed()
    {
      if(_base is IWatchable)
      {
        Unregister(_base as IWatchable);
      }
      if(_operand is IWatchable)
      {
        Unregister(_operand as IWatchable);
      }
      _base = null;
      _operand = null;
    }

  }
}