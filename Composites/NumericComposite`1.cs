using System.Collections.Generic;


namespace Watchables
{
  public sealed class NumericComposite<T> : NestedWatchable<T>, IEnforceNumeric<T> where T : unmanaged
  {

    private readonly List<NumericCompositePart<T>> _steps = new();

    public NumericComposite()
    {
      NumericUtility.ValidateType(typeof(T));
    }

    public void AddStep(NumericCompositePart<T> step)
    {
      MutationGuard(
        () =>
        {
          AddStepPrivate(step);
          Sort();
        }
      );
    }

    public void AddSteps(IEnumerable<NumericCompositePart<T>> steps)
    {
      MutationGuard(
        () =>
        {
          foreach(NumericCompositePart<T> step in steps)
          {
            AddStepPrivate(step);
          }
          Sort();
        }
      );
    }

    public void RemoveStep(NumericCompositePart<T> step)
    {
      MutationGuard(
        () =>
        {
          RemoveStepPrivate(step);
          Sort();
        }
      );
    }

    public void RemoveSteps(IEnumerable<NumericCompositePart<T>> steps)
    {
      MutationGuard(
        () =>
        {
          foreach(NumericCompositePart<T> step in steps)
          {
            RemoveStepPrivate(step);
          }
          Sort();
        }
      );
    }

    protected override bool CheckFatalDestruction(IWatchable dependency) => false;

    protected override void Evaluate()
    {
      for(int i = 0; i < _steps.Count; i++)
      {
        NumericCompositePart<T> step = _steps[i];
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

    private void AddStepPrivate(NumericCompositePart<T> step)
    {
      if(_steps.Contains(step)) { return; }
      _steps.Add(step);
      if(step.Value is IWatchable)
      {
        Register(step.Value as IWatchable);
      }
      step.Removed += OnPartRemoved;
    }

    private void RemoveStepPrivate(NumericCompositePart<T> step)
    {
      step.Removed -= OnPartRemoved;
      if(!_steps.Remove(step)) { return; }
      if(step.Value is IWatchable)
      {
        Unregister(step.Value as IWatchable);
      }
    }

    private void OnPartRemoved(CompositeValueBase<T> part)
    {
      if(part is NumericCompositePart<T> unboxed)
      {
        RemoveStepPrivate(unboxed);
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
}