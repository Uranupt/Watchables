using System.Collections.Generic;


namespace Watchables
{
  public abstract class CompositeWatchable<T> : NestedWatchable<T>, ISealable where T : unmanaged
  {

    private readonly List<CompositeStep<T>> _steps = new();

    public bool IsSealed { get; private set; }

    public bool AddStep(CompositeStep<T> step, object owner = null)
    {
      if(!IsEditAllowed(owner)) { return false; }
      AddStepPrivate(step);
      Sort();
      Evaluate();
      return true;
    }

    public bool AddSteps(IEnumerable<CompositeStep<T>> steps, object owner = null)
    {
      if(!IsEditAllowed(owner)) { return false; }
      foreach(CompositeStep<T> step in steps)
      {
        AddStepPrivate(step);
      }
      Sort();
      Evaluate();
      return true;
    }

    public bool RemoveStep(CompositeStep<T> step, object owner = null)
    {
      if(!IsEditAllowed(owner)) { return false; }
      RemoveStepPrivate(step);
      Sort();
      Evaluate();
      return true;
    }

    public bool RemoveSteps(IEnumerable<CompositeStep<T>> steps, object owner = null)
    {
      if(!IsEditAllowed(owner)) { return false; }
      foreach(CompositeStep<T> step in steps)
      {
        RemoveStepPrivate(step);
      }
      Sort();
      Evaluate();
      return true;
    }

    public bool SetSealed(bool sealedState, object owner = null)
    {
      if(IsOwned && !CompareToOwner(owner)) { return false; }
      IsSealed = sealedState;
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
          case CompositeOperation.Subtract:
          {
            ApplyTranslation(step, step.Operation == CompositeOperation.Subtract);
            break;
          }
          case CompositeOperation.Multiply:
          case CompositeOperation.Divide:
          {
            ApplyScaling(step, step.Operation == CompositeOperation.Divide);
            break;
          }
          case CompositeOperation.Minimum:
          case CompositeOperation.Maximum:
          {
            ApplyClamp(step, step.Operation == CompositeOperation.Maximum);
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

    protected abstract void ApplyTranslation(T value, bool subtract);
    protected abstract void ApplyScaling(T value, bool divide);
    protected abstract void ApplyClamp(T bound, bool upperBound);

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

    private bool IsEditAllowed(object owner)
    {
      if(IsDestroyed) { return false; }
      if(!IsSealed) { return true; }
      return CompareToOwner(owner);
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