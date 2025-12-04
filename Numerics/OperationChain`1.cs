using System.Collections.Generic;

namespace Watchables
{
  public sealed class OperationChain<T> : SealableNestedBase<T>, IEnforceNumeric<T> where T : unmanaged
  {

    private readonly List<OperationStep<T>> _steps = new();
    private IValueWrapper<T> _baseValue;

    internal OperationChain(IValueWrapper<T> baseValue)
    {
      _baseValue = baseValue;
      if(_baseValue is IWatchable)
      {
        Register(_baseValue as IWatchable);
      }
    }

    public bool AddStep(OperationStep<T> step, object owner = null)
    {
      if(!MutationGuard(() => AddStepPrivate(step), owner)) { return false; }
      Evaluate();
      return true;
    }

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
      Evaluate();
      return true;
    }

    public bool RemoveStep(OperationStep<T> step, object owner = null)
    {
      if(!MutationGuard(() => RemoveStepPrivate(step), owner)) { return false; }
      Evaluate();
      return true;
    }

    public bool RemoveSteps(IEnumerable<OperationStep<T>> steps, object owner = null)
    {
      if(!MutationGuard(
        () => { foreach(OperationStep<T> step in steps) { AddStepPrivate(step);  } },
        owner
      ))
      {
        return false;
      }
      Evaluate();
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
      InvokeChanged();
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

  public static class OperationChain
  {

    public static OperationChain<ushort> New(IValueWrapper<ushort> value) => new OperationChain<ushort>(value);
    public static OperationChain<uint> New(IValueWrapper<uint> value) => new OperationChain<uint>(value);
    public static OperationChain<ulong> New(IValueWrapper<ulong> value) => new OperationChain<ulong>(value);
    public static OperationChain<short> New(IValueWrapper<short> value) => new OperationChain<short>(value);
    public static OperationChain<int> New(IValueWrapper<int> value) => new OperationChain<int>(value);
    public static OperationChain<long> New(IValueWrapper<long> value) => new OperationChain<long>(value);
    public static OperationChain<decimal> New(IValueWrapper<decimal> value) => new OperationChain<decimal>(value);
    public static OperationChain<float> New(IValueWrapper<float> value) => new OperationChain<float>(value);
    public static OperationChain<double> New(IValueWrapper<double> value) => new OperationChain<double>(value);

    public static OperationChain<ushort> Then(this OperationChain<ushort> chain, UnsignedOperation operation,
     IValueWrapper<ushort> operand, bool required = false, object owner = null)
    {
      return chain.Then(OperationStep.New(operation, operand, required), owner);
    }

    public static OperationChain<uint> Then(this OperationChain<uint> chain, UnsignedOperation operation,
      IValueWrapper<uint> operand, bool required = false)
    {
      return chain.Then(OperationStep.New(operation, operand, required));
    }

    public static OperationChain<ulong> Then(this OperationChain<ulong> chain, UnsignedOperation operation,
      IValueWrapper<ulong> operand, bool required = false)
    {
      return chain.Then(OperationStep.New(operation, operand, required));
    }

    public static OperationChain<short> Then(this OperationChain<short> chain, SignedOperation operation,
      IValueWrapper<short> operand, bool required = false)
    {
      return chain.Then(OperationStep.New(operation, operand, required));
    }

    public static OperationChain<int> Then(this OperationChain<int> chain, SignedOperation operation,
      IValueWrapper<int> operand, bool required = false)
    {
      return chain.Then(OperationStep.New(operation, operand, required));
    }

    public static OperationChain<long> Then(this OperationChain<long> chain, SignedOperation operation,
      IValueWrapper<long> operand, bool required = false)
    {
      return chain.Then(OperationStep.New(operation, operand, required));
    }

    public static OperationChain<decimal> Then(this OperationChain<decimal> chain, RealOperation operation,
      IValueWrapper<decimal> operand, bool required = false)
    {
      return chain.Then(OperationStep.New(operation, operand, required));
    }

    public static OperationChain<float> Then(this OperationChain<float> chain, RealOperation operation,
      IValueWrapper<float> operand, bool required = false)
    {
      return chain.Then(OperationStep.New(operation, operand, required));
    }

    public static OperationChain<double> Then(this OperationChain<double> chain, RealOperation operation,
      IValueWrapper<double> operand, bool required = false)
    {
      return chain.Then(OperationStep.New(operation, operand, required));
    }

    public static bool AddStep(this OperationChain<ushort> chain, UnsignedOperation operation,
      IValueWrapper<ushort> operand, bool required = false, object owner = null)
    {
      return chain.AddStep(OperationStep.New(operation, operand, required), owner);
    }

    public static bool AddStep(this OperationChain<uint> chain, UnsignedOperation operation,
      IValueWrapper<uint> operand, bool required = false, object owner = null)
    {
      return chain.AddStep(OperationStep.New(operation, operand, required), owner);
    }
    public static bool AddStep(this OperationChain<ulong> chain, UnsignedOperation operation,
      IValueWrapper<ulong> operand, bool required = false, object owner = null)
    {
      return chain.AddStep(OperationStep.New(operation, operand, required), owner);
    }
    public static bool AddStep(this OperationChain<short> chain, SignedOperation operation,
      IValueWrapper<short> operand, bool required = false, object owner = null)
    {
      return chain.AddStep(OperationStep.New(operation, operand, required), owner);
    }

    public static bool AddStep(this OperationChain<int> chain, SignedOperation operation,
      IValueWrapper<int> operand, bool required = false, object owner = null)
    {
      return chain.AddStep(OperationStep.New(operation, operand, required), owner);
    }

    public static bool AddStep(this OperationChain<long> chain, SignedOperation operation,
      IValueWrapper<long> operand, bool required = false, object owner = null)
    {
      return chain.AddStep(OperationStep.New(operation, operand, required), owner);
    }

    public static bool AddStep(this OperationChain<decimal> chain, RealOperation operation,
      IValueWrapper<decimal> operand, bool required = false, object owner = null)
    {
      return chain.AddStep(OperationStep.New(operation, operand, required), owner);
    }

    public static bool AddStep(this OperationChain<float> chain, RealOperation operation,
      IValueWrapper<float> operand, bool required = false, object owner = null)
    {
      return chain.AddStep(OperationStep.New(operation, operand, required), owner);
    }

    public static bool AddStep(this OperationChain<double> chain, RealOperation operation,
      IValueWrapper<double> operand, bool required = false, object owner = null)
    {
      return chain.AddStep(OperationStep.New(operation, operand, required), owner);
    }

    public static OperationChain<ushort> Clamp(IValueWrapper<ushort> value, IValueWrapper<ushort> min, IValueWrapper<ushort> max)
      => new OperationChain<ushort>(value).Then(UnsignedOperation.Minimum, min).Then(UnsignedOperation.Maximum, max);

  }
}