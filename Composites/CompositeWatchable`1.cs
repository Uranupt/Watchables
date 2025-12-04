using System.Collections.Generic;


namespace Watchables
{
  public class CompositeWatchable<T> : SealableNestedBase<T>, IEnforceNumeric<T> where T : unmanaged
  {

    private readonly List<CompositeStep<T>> _steps = new();

    public bool AddStep(CompositeStep<T> step, object owner = null)
    {
      if(!MutationGuard(() => AddStepPrivate(step), owner))
      {
        return false;
      }
      Sort();
      Evaluate();
      return true;
    }

    public bool AddSteps(IEnumerable<CompositeStep<T>> steps, object owner = null)
    {
      if(!MutationGuard(
        () => { foreach(CompositeStep<T> step in steps) { AddStepPrivate(step); } },
        owner
      ))
      {
        return false;
      }
      Sort();
      Evaluate();
      return true;
    }

    public bool RemoveStep(CompositeStep<T> step, object owner = null)
    {
      if(!MutationGuard(() => RemoveStepPrivate(step), owner))
      {
        return false;
      }
      Sort();
      Evaluate();
      return true;
    }

    public bool RemoveSteps(IEnumerable<CompositeStep<T>> steps, object owner = null)
    {
      if(!MutationGuard(
        () => { foreach(CompositeStep<T> step in steps) { RemoveStepPrivate(step); } },
        owner
      ))
      {
        return false;
      }
      Sort();
      Evaluate();
      return true;
    }

    protected override bool CheckFatalDestruction(IWatchable dependency) => false;

    protected override void Evaluate()
    {
      for(int i = 0; i < _steps.Count; i++)
      {
        CompositeStep<T> step = _steps[i];
        switch(step.Operation)
        {
          case CompositeOperation.Force:
          {
            _value = step;
            return;
          }
          case CompositeOperation.SetFinal:
          {
            _value = step;
            while(i < _steps.Count && _steps[i].Operation < CompositeOperation.Minimum)
            {
              i++;
            }
            break;
          }
          case CompositeOperation.SetBase:
          {
            _value = step;
            while(i < _steps.Count && _steps[i].Operation <= CompositeOperation.SetBase)
            {
              i++;
            }
            break;
          }
          case CompositeOperation.Add:
          {
            _value.Operate(NumericOperation.Add, step.ToValue(), true);
            break;
          }
          case CompositeOperation.Subtract:
          {
            _value.Operate(NumericOperation.Subtract, step.ToValue(), true);
            break;
          }
          case CompositeOperation.Multiply:
          {
            _value.Operate(NumericOperation.Multiply, step.ToValue(), true);
            break;
          }
          case CompositeOperation.Divide:
          {
            _value.Operate(NumericOperation.Divide, step.ToValue(), true);
            break;
          }
          case CompositeOperation.Minimum:
          {
            _value.Operate(NumericOperation.Minimum, step.ToValue(), true);
            break;
          }
          case CompositeOperation.Maximum:
          {
            _value.Operate(NumericOperation.Maximum, step.ToValue(), true);
            break;
          }
        }
      }
      InvokeChanged();
    }

    protected override void OnDestroyed(IWatchable self)
    {
      while(_steps.Count > 0)
      {
        RemoveStepPrivate(_steps[0]);
      }
    }

    protected override void OnNonFatalDestruction(IWatchable dependency)
    {
      for(int i = 0; i < _steps.Count; i++)
      {
        if(_steps[i].Value == dependency)
        {
          RemoveStepPrivate(_steps[i]);
          return;
        }
      }
    }

    private void AddStepPrivate(CompositeStep<T> step)
    {
      if(_steps.Contains(step)) { return; }
      _steps.Add(step);
      if(step.Value is IWatchable)
      {
        Register(step.Value as IWatchable);
      }
      step.Removed += RemoveStepPrivate;
    }

    private void RemoveStepPrivate(CompositeStep<T> step)
    {
      step.Removed -= RemoveStepPrivate;
      if(!_steps.Remove(step)) { return; }
      if(step.Value is IWatchable)
      {
        Unregister(step.Value as IWatchable);
      }
    }

    private void Sort()
    {
      _steps.Sort(
        (x, y) =>
        {
          int resl = x.Operation.CompareTo(y.Operation);
          return resl == 0 ? y.Priority.CompareTo(x.Priority) : resl;
        }
       );
    }

  }

  public static class CompositeWatchable
  {

    public static CompositeWatchable<ushort> UShort() => new CompositeWatchable<ushort>();
    public static CompositeWatchable<uint> UInt() => new CompositeWatchable<uint>();
    public static CompositeWatchable<ulong> ULong() => new CompositeWatchable<ulong>();
    public static CompositeWatchable<short> Short() => new CompositeWatchable<short>();
    public static CompositeWatchable<int> Int() => new CompositeWatchable<int>();
    public static CompositeWatchable<long> Long() => new CompositeWatchable<long>();
    public static CompositeWatchable<decimal> Decimal() => new CompositeWatchable<decimal>();
    public static CompositeWatchable<float> Float() => new CompositeWatchable<float>();
    public static CompositeWatchable<double> Double() => new CompositeWatchable<double>();

    public static CompositeWatchable<ushort> WithBase(IValueWrapper<ushort> value, StepPriority priority = StepPriority.None)
    {
      CompositeWatchable<ushort> resl = new();
      resl.AddStep(CompositeStep.New(CompositeOperation.SetBase, value, priority));
      return resl;
    }

    public static CompositeWatchable<uint> WithBase(IValueWrapper<uint> value, StepPriority priority = StepPriority.None)
    {
      CompositeWatchable<uint> resl = new();
      resl.AddStep(CompositeStep.New(CompositeOperation.SetBase, value, priority));
      return resl;
    }

    public static CompositeWatchable<ulong> WithBase(IValueWrapper<ulong> value, StepPriority priority = StepPriority.None)
    {
      CompositeWatchable<ulong> resl = new();
      resl.AddStep(CompositeStep.New(CompositeOperation.SetBase, value, priority));
      return resl;
    }

    public static CompositeWatchable<short> WithBase(IValueWrapper<short> value, StepPriority priority = StepPriority.None)
    {
      CompositeWatchable<short> resl = new();
      resl.AddStep(CompositeStep.New(CompositeOperation.SetBase, value, priority));
      return resl;
    }

    public static CompositeWatchable<int> WithBase(IValueWrapper<int> value, StepPriority priority = StepPriority.None)
    {
      CompositeWatchable<int> resl = new();
      resl.AddStep(CompositeStep.New(CompositeOperation.SetBase, value, priority));
      return resl;
    }

    public static CompositeWatchable<long> WithBase(IValueWrapper<long> value, StepPriority priority = StepPriority.None)
    {
      CompositeWatchable<long> resl = new();
      resl.AddStep(CompositeStep.New(CompositeOperation.SetBase, value, priority));
      return resl;
    }

    public static CompositeWatchable<decimal> WithBase(IValueWrapper<decimal> value, StepPriority priority = StepPriority.None)
    {
      CompositeWatchable<decimal> resl = new();
      resl.AddStep(CompositeStep.New(CompositeOperation.SetBase, value, priority));
      return resl;
    }

    public static CompositeWatchable<float> WithBase(IValueWrapper<float> value, StepPriority priority = StepPriority.None)
    {
      CompositeWatchable<float> resl = new();
      resl.AddStep(CompositeStep.New(CompositeOperation.SetBase, value, priority));
      return resl;
    }

    public static CompositeWatchable<double> WithBase(IValueWrapper<double> value, StepPriority priority = StepPriority.None)
    {
      CompositeWatchable<double> resl = new();
      resl.AddStep(CompositeStep.New(CompositeOperation.SetBase, value, priority));
      return resl;
    }

  }
}