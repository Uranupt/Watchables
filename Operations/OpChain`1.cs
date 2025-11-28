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

    public bool AddStep(OpChainStep<T> step, object owner = null)
    {
      if(!IsEditAllowed(owner) || !CheckValidOperation(step.OperationType)) { return false; }
      _steps.Add(step);
      if(step.Value != null && step.Value is IWatchable)
      {
        Register(step.Value as IWatchable);
      }
      Evaluate();
      return true;
    }

    public bool AddSteps(List<OpChainStep<T>> steps, bool allValidOps = false, object owner = null)
    {
      if(!IsEditAllowed(owner)) { return false; }
      if(allValidOps)
      {
        foreach(OpChainStep<T> step in steps)
        {
          if(!CheckValidOperation(step.OperationType)) { return false; }
        }
      }
      foreach(OpChainStep<T> step in steps)
      {
        if(!allValidOps && !CheckValidOperation(step.OperationType)) { continue; }
        _steps.Add(step);
        if(step.Value != null && step.Value is IWatchable)
        {
          Register(step.Value as IWatchable);
        }
      }
      Evaluate();
      return true;
    }

    public bool RemoveStep(OpChainStep<T> step, object owner = null)
    {
      if(!IsEditAllowed(owner) { return false; }
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

    public OpChain<T> Add(IValueWrapper<T> value, bool fatal = false, object owner = null) => ChainAddStep(OpChainStep<T>.Add(value, fatal), owner);
    public OpChain<T> Subtract(IValueWrapper<T> value, bool fatal = false, object owner = null) => ChainAddStep(OpChainStep<T>.Subtract(value, fatal), owner);
    public OpChain<T> Multiply(IValueWrapper<T> value, bool fatal = false, object owner = null) => ChainAddStep(OpChainStep<T>.Multiply(value, fatal), owner);
    public OpChain<T> Divide(IValueWrapper<T> value, bool fatal = false, object owner = null) => ChainAddStep(OpChainStep<T>.Divide(value, fatal), owner);
    public OpChain<T> Modulo(IValueWrapper<T> value, bool fatal = false, object owner = null) => ChainAddStep(OpChainStep<T>.Modulo(value, fatal), owner);
    public OpChain<T> ToPower(IValueWrapper<T> value, bool fatal = false, object owner = null) => ChainAddStep(OpChainStep<T>.ToPower(value, fatal), owner);
    public OpChain<T> AsPower(IValueWrapper<T> value, bool fatal = false, object owner = null) => ChainAddStep(OpChainStep<T>.AsPower(value, fatal), owner);
    public OpChain<T> ToRoot(IValueWrapper<T> value, bool fatal = false, object owner = null) => ChainAddStep(OpChainStep<T>.ToRoot(value, fatal), owner);
    public OpChain<T> AsRoot(IValueWrapper<T> value, bool fatal = false, object owner = null) => ChainAddStep(OpChainStep<T>.AsRoot(value, fatal), owner);
    public OpChain<T> Minimum(IValueWrapper<T> value, bool fatal = false, object owner = null) => ChainAddStep(OpChainStep<T>.Minimum(value, fatal), owner);
    public OpChain<T> Maximum(IValueWrapper<T> value, bool fatal = false, object owner = null) => ChainAddStep(OpChainStep<T>.Maximum(value, fatal), owner);
    public OpChain<T> Round(bool fatal = false, object owner = null) => ChainAddStep(OpChainStep<T>.Round(fatal), owner);
    public OpChain<T> Floor(bool fatal = false, object owner = null) => ChainAddStep(OpChainStep<T>.Floor(fatal), owner);
    public OpChain<T> Ceiling(bool fatal = false, object owner = null) => ChainAddStep(OpChainStep<T>.Ceiling(fatal), owner);
    public OpChain<T> Truncate(bool fatal = false, object owner = null) => ChainAddStep(OpChainStep<T>.Truncate(fatal), owner);
    public OpChain<T> Absolute(bool fatal = false, object owner = null) => ChainAddStep(OpChainStep<T>.Absolute(fatal), owner);
    public OpChain<T> AsNegative(bool fatal = false, object owner = null) => ChainAddStep(OpChainStep<T>.AsNegative(fatal), owner);
    public OpChain<T> FlipSign(bool fatal = false, object owner = null) => ChainAddStep(OpChainStep<T>.FlipSign(fatal), owner);
    public OpChain<T> Reciprocal(bool fatal = false, object owner = null) => ChainAddStep(OpChainStep<T>.Reciprocal(fatal), owner);



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
    protected abstract bool CheckValidOperation(OperationType op);

    private bool IsEditAllowed(object owner)
    {
      if(IsDestroyed) { return false; }
      if(!IsSealed) { return true; }
      return CompareToOwner(owner);
    }

    private OpChain<T> ChainAddStep(OpChainStep<T> step, object owner)
    {
      AddStep(step, owner);
      return this;
    }

  }
}