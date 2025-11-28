using System.Collections.Generic;


namespace Watchables
{
  public abstract class OpChain<T> : NestedWatchable<T>, ISealable where T : unmanaged
  {

    protected readonly List<OpChainStep<T>> _steps = new();
    protected IValueWrapper<T> _baseValue;

    public bool IsSealed { get; protected set; }

    public OpChain(IValueWrapper<T> baseValue)
    {
      _baseValue = baseValue;
      if(_baseValue is IWatchable)
      {
        Register(_baseValue as IWatchable);
      }
    }

    public bool AddStep(OpChainStep<T> step)
    {
      if(IsSealed || IsDestroyed) { return false; }
      _steps.Add(step);
      if(step.Value != null && step.Value is IWatchable)
      {
        Register(step.Value as IWatchable);
      }
      Evaluate();
      return true;
    }

    public bool AddSteps(List<OpChainStep<T>> steps)
    {
      if(IsSealed || IsDestroyed) { return false; }
      foreach(OpChainStep<T> step in steps)
      {
        _steps.Add(step);
        if(step.Value != null && step.Value is IWatchable)
        {
          Register(step.Value as IWatchable);
        }
      }
      Evaluate();
      return true;
    }

    public bool RemoveStep(OpChainStep<T> step)
    {
      if(IsSealed || IsDestroyed) { return false; }
      if(!_steps.Contains(step)) { return true; } //Concept is to report whether the step is successfully gone, so if it never existed this counts as a success.
      _steps.Remove(step);
      if(step.Value != null && step.Value is IWatchable)
      {
        Unregister(step.Value as IWatchable);
      }
      Evaluate();
      return true;
    }

    public bool SetSealed(bool sealedState, object owner = null)
    {
      if(IsOwned && !CompareToOwner(owner)) { return false; }
      IsSealed = sealedState;
      return true;
    }

    protected override void OnNonFatalDestruction(IWatchable dependency)
    {
      if(dependency is not IValueWrapper<T>) { return; }
      bool sealState = IsSealed;
      IsSealed = false;
      IValueWrapper<T> castDep = dependency as IValueWrapper<T>;
      for(int i = 0; i < _steps.Count; i++)
      {
        if(_steps[i].Value == castDep)
        {
          RemoveStep(_steps[i]);
          break;
        }
      }
      IsSealed = sealState;
    }

    protected override bool CheckFatalDestruction(IWatchable dependency)
    {
      if(dependency is not IValueWrapper<T>) { return false; }
      IValueWrapper<T> castDep = dependency as IValueWrapper<T>;
      if(castDep == _baseValue) { return true; }
      foreach(OpChainStep<T> step in _steps)
      {
        if(step.Value == castDep) { return step.IsDestructionFatal; }
      }
      return false;
    }

    protected override void OnDestroyed(IWatchable self)
    {
      IsSealed = false;
      foreach(OpChainStep<T> step in _steps)
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
      foreach(OpChainStep<T> step in _steps)
      {
        ApplyOperation(step);
      }
      InvokeChanged();
    }

    protected abstract void ApplyOperation(OpChainStep<T> step);

  }
}