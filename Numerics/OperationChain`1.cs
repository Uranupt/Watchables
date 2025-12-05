using System.Collections.Generic;

namespace Watchables
{
  public sealed class OperationChain<T> : SealableNestedBase<T>, IEnforceNumeric<T> where T : unmanaged
  {

    private readonly List<OperationStep<T>> _steps = new();
    private IValueWrapper<T> _baseValue;

    public OperationChain(IValueWrapper<T> baseValue)
    {
      NumericUtility.ValidateType(typeof(T));
      _baseValue = baseValue;
      if(_baseValue is IWatchable)
      {
        Register(_baseValue as IWatchable);
      }
    }

    public bool AddStep(OperationStep<T> step, object owner = null) => MutationGuard(() => AddStepPrivate(step), owner);

    public OperationChain<T> Then(OperationStep<T> step, object owner = null)
    {
      AddStep(step, owner);
      return this;
    }

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

    public bool RemoveStep(OperationStep<T> step, object owner = null) => MutationGuard(() => RemoveStepPrivate(step), owner);

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

    protected override void OnNonFatalDestruction(IWatchable dependency)
    {
      if(dependency is not IValueWrapper<T>) { return; }
      IValueWrapper<T> castDep = dependency as IValueWrapper<T>;
      for(int i = 0; i < _steps.Count; i++)
      {
        if(_steps[i].Value == castDep)
        {
          RemoveStepPrivate(_steps[i]);
          break;
        }
      }
      Evaluate();
    }

    protected override bool CheckFatalDestruction(IWatchable dependency)
    {
      if(dependency is not IValueWrapper<T> wrapper) { return false; }
      if(wrapper == _baseValue) { return true; }
      foreach(OperationStep<T> step in _steps)
      {
        if(step.Value == wrapper) { return step.IsRequired; }
      }
      return false;
    }

    protected override void OnDestroyed(IWatchable self)
    {
      foreach(OperationStep<T> step in _steps)
      {
        if(step.Value != null && step.Value is IWatchable)
        {
          Unregister(step.Value as IWatchable);
        }
      }
      _steps.Clear();
      if(_baseValue is IWatchable)
      {
        Unregister(_baseValue as IWatchable);
      }
      _baseValue = null;
    }

    protected override void Evaluate()
    {
      _value = _baseValue.ToValue();
      foreach(OperationStep<T> step in _steps)
      {
        _value.Operate(step.Operation, step.Value.ToValue(), true);
      }
    }

    private void AddStepPrivate(OperationStep<T> step)
    {
      _steps.Add(step);
      if(step.Value is IWatchable)
      {
        Register(step.Value as IWatchable);
      }
    }

    private void RemoveStepPrivate(OperationStep<T> step)
    {
      _steps.Remove(step);
      if(step.Value is IWatchable)
      {
        Unregister(step.Value as IWatchable);
      }
    }

  }
}