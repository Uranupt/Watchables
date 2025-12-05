using System.Collections.Generic;
using System;


namespace Watchables
{
  public sealed class StringComposite : SealableNestedBase<string>
  {

    private readonly List<object> _inputs = new();
    private readonly HashSet<IWatchable> _requiredInputs = new();

    public StringComposite(params (object, bool)[] inputs)
    {
      foreach((object input, bool required) in inputs)
      {
        AddInputPrivate(input, required);
      }
    }

    public bool AddInput(object input, bool required = false, object owner = null)
    {
      return MutationGuard(() => AddInputPrivate(input, required), owner);
    }

    public bool AddInputs(IEnumerable<(object, bool)> inputs, object owner = null)
    {
      return MutationGuard(
        () =>
        {
          foreach((object input, bool required) in inputs)
          {
            AddInputPrivate(input, required);
          }
        },
        owner
      );
    }

    public bool RemoveInput(object input, object owner = null)
    {
      return MutationGuard(() => RemoveInputPrivate(input), owner);
    }

    public bool RemoveInputs(IEnumerable<object> inputs, object owner = null)
    {
      return MutationGuard(
        () =>
        {
          foreach(object input in inputs)
          {
            RemoveInputPrivate(input);
          }
        },
        owner
      );
    }

    public bool Insert(object input, int index, bool required = false, object owner = null)
    {
      return MutationGuard(() => InsertPrivate(input, index, required), owner);
    }

    public bool InsertBefore(object input, object target, bool required = false, object owner = null)
    {
      int index = _inputs.IndexOf(target);
      if(index < 0) { return false; }
      index = index == 0 ? 0 : index - 1;
      return Insert(input, index, required, owner);
    }

    public bool InsertAfter(object input, object target, bool required = false, object owner = null)
    {
      int index = _inputs.IndexOf(target);
      if(index < 0) { return false; }
      return Insert(input, index + 1, required, owner);
    }

    public bool Clear(object owner = null)
    {
      return MutationGuard(ClearPrivate, owner);
    }

    protected override bool CheckFatalDestruction(IWatchable dependency) => _requiredInputs.Contains(dependency);

    protected override void Evaluate()
    {
      _value = "";
      foreach(object input in _inputs)
      {
        _value += input.ToString();
      }
    }

    protected override void OnDestroyed(IWatchable self)
    {
      foreach(object input in _inputs)
      {
        if(input is IWatchable)
        {
          Unregister(input as IWatchable);
        }
      }
      _inputs.Clear();
      _requiredInputs.Clear();
    }

    protected override void OnNonFatalDestruction(IWatchable dependency)
    {
      _inputs.Remove(dependency);
    }

    private void AddInputPrivate(object input, bool required)
    {
      _inputs.Add(input);
      if(input is IWatchable watchable)
      {
        Register(watchable);
        if(required)
        {
          _requiredInputs.Add(watchable);
        }
      }
    }

    private void RemoveInputPrivate(object input)
    {
      _inputs.Remove(input);
      if(input is IWatchable watchable)
      {
        Unregister(watchable);
        _requiredInputs.Remove(watchable);
      }
    }

    private void InsertPrivate(object input, int index, bool required = false)
    {
      if(_inputs.Count >= index)
      {
        _inputs.Insert(index, input);
        if(input is IWatchable watchable)
        {
          Register(watchable);
          if(required)
          {
            _requiredInputs.Add(watchable);
          }
        }
      }
      else
      {
        AddInputPrivate(input, required);
      }
    }

    private void ClearPrivate()
    {
      int safety = _inputs.Count;
      while(_inputs.Count > 0)
      {
        RemoveInputPrivate(_inputs[0]);
        safety--;
        if(safety != _inputs.Count)
        {
          throw new StackOverflowException("Issue clearing inputs, count after removal does not match expected value. Exiting early to prevent potential infinite loop.");
        }
      }
    }

    //private bool CanEdit(object owner)
    //{
    //  if(IsDestroyed) { return false; }
    //  if(!IsSealed) { return true; }
    //  return CompareToOwner(owner);
    //}

  }
}