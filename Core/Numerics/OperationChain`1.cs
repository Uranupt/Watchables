using System.Collections.Generic;


namespace Watchables
{
  /// <summary>
  /// An <see cref="IWatchable{T}"/> implementation which represents a series of <see cref="NumericOperation"/>s performed on 
  /// a base value in a set sequence. Operations and operands are defined by <see cref="OperationStep{T}"/>.
  /// </summary>
  public sealed class OperationChain<T> : SealableNestedBase<T>, IEnforceNumeric<T> where T : unmanaged
  {

    private readonly List<OperationStep<T>> _steps = new();
    private IWrapper<T> _baseValue;

    public OperationChain(IWrapper<T> baseValue)
    {
      NumericUtility.ValidateType(typeof(T));
      _baseValue = baseValue;
      if(_baseValue is IWatchable)
      {
        Register(_baseValue as IWatchable);
      }
    }

    /// <summary> Attempts to add a new <see cref="OperationStep{T}"/> to the <see cref="OperationChain{T}"/>. </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool AddStep(OperationStep<T> step, object owner = null) => MutationGuard(() => AddStepPrivate(step), owner);


    /// <summary> Attempts to add a new <see cref="OperationStep{T}"/> to the <see cref="OperationChain{T}"/>. </summary>
    /// <remarks> Allows for method chaining the addition of <see cref="OperationStep{T}"/>s. </remarks>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> The <see cref="OperationChain{T}"/> this was called on. </returns>
    public OperationChain<T> Then(OperationStep<T> step, object owner = null)
    {
      AddStep(step, owner);
      return this;
    }

    /// <summary> Attempts to add a range of new <see cref="OperationStep{T}"/>s to the <see cref="OperationChain{T}"/>. </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool AddSteps(IEnumerable<OperationStep<T>> steps, object owner = null)
    {
      if(!MutationGuard(
        () => { foreach(OperationStep<T> step in steps) { AddStepPrivate(step); } },
        owner
      ))
      {
        return false;
      }
      return true;
    }

    /// <summary> Attempts to remove a <see cref="OperationStep{T}"/> from the <see cref="OperationChain{T}"/>. </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool RemoveStep(OperationStep<T> step, object owner = null) => MutationGuard(() => RemoveStepPrivate(step), owner);

    /// <summary> Attempts to remove a range of <see cref="OperationStep{T}"/>s from the <see cref="OperationChain{T}"/>. </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool RemoveSteps(IEnumerable<OperationStep<T>> steps, object owner = null)
    {
      if(!MutationGuard(
        () => { foreach(OperationStep<T> step in steps) { AddStepPrivate(step); } },
        owner
      ))
      {
        return false;
      }
      return true;
    }

    /// <inheritdoc/>
    protected override void OnNonFatalDestruction(IWatchable dependency)
    {
      if(dependency is not IWrapper<T>) { return; }
      IWrapper<T> castDep = dependency as IWrapper<T>;
      for(int i = 0; i < _steps.Count; i++)
      {
        if(_steps[i].ValueSource == castDep)
        {
          RemoveStepPrivate(_steps[i]);
          break;
        }
      }
      Evaluate();
    }

    /// <inheritdoc/>
    protected override bool CheckFatalDestruction(IWatchable dependency)
    {
      if(dependency is not IWrapper<T> wrapper) { return false; }
      if(wrapper == _baseValue) { return true; }
      foreach(OperationStep<T> step in _steps)
      {
        if(step.ValueSource == wrapper) { return step.IsRequired; }
      }
      return false;
    }

    /// <inheritdoc/>
    protected override void BeforeDestroyed()
    {
      foreach(OperationStep<T> step in _steps)
      {
        if(step.ValueSource != null && step.ValueSource is IWatchable)
        {
          Unregister(step.ValueSource as IWatchable);
        }
      }
      _steps.Clear();
      if(_baseValue is IWatchable)
      {
        Unregister(_baseValue as IWatchable);
      }
      _baseValue = null;
    }

    /// <inheritdoc/>
    protected override void Evaluate()
    {
      Value = _baseValue.Value;
      foreach(OperationStep<T> step in _steps)
      {
        Value.Operate(step.Operation, step.Value, true);
      }
    }

    private void AddStepPrivate(OperationStep<T> step)
    {
      _steps.Add(step);
      if(step.ValueSource is IWatchable)
      {
        Register(step.ValueSource as IWatchable);
      }
    }

    private void RemoveStepPrivate(OperationStep<T> step)
    {
      _steps.Remove(step);
      if(step.ValueSource is IWatchable)
      {
        Unregister(step.ValueSource as IWatchable);
      }
    }

  }
}