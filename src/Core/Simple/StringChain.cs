using System.Collections.Generic;
using System;


namespace Watchables
{
  /// <summary>
  /// An <see cref="IWatchable{T}"/> implementation which constructs a single <see cref="string"/> from inputs provided in a set order.
  /// </summary>
  public sealed class StringChain : SealableNestedBase<string>
  {

    private readonly List<object> _inputs = new();
    private readonly HashSet<IWatchable> _requiredInputs = new();

    public StringChain(params object[] inputs)
    {
      foreach(object input in inputs)
      {
        AddInputPrivate(input, false);
      }
      Evaluate();
    }

    public StringChain(bool required, params object[] inputs)
    {
      foreach(object input in inputs)
      {
        AddInputPrivate(input, required);
      }
      Evaluate();
    }

    public StringChain(params (object, bool)[] inputs)
    {
      foreach((object input, bool required) in inputs)
      {
        AddInputPrivate(input, required);
      }
      Evaluate();
    }

    /// <summary> Attempts to add an <paramref name="input"/> to the <see cref="StringChain"/>. </summary>
    /// <param name="required"> Whether the input is required for this chain. Only relevant for <see cref="IWatchable"/> inputs. </param>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool AddInput(object input, bool required = false, object owner = null)
    {
      return MutationGuard(() => AddInputPrivate(input, required), owner);
    }

    /// <summary> Attempts to add a range of <paramref name="inputs"/> and their required status to the <see cref="StringChain"/>. </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
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

    /// <summary> Attempts to add a range of <paramref name="inputs"/> to the <see cref="StringChain"/>. </summary>
    /// <param name="required"> Whether the inputs are required for this chain. Only relevant for <see cref="IWatchable"/> inputs. </param>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool AddInputs(IEnumerable<object> inputs, bool required = false, object owner = null)
    {
      return MutationGuard(
        () =>
        {
          foreach(object input in inputs)
          {
            AddInputPrivate(input, required);
          }
        },
        owner
      );
    }

    /// <summary> Attempts to remove an <paramref name="input"/> from the <see cref="StringChain"/>. </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool RemoveInput(object input, object owner = null)
    {
      return MutationGuard(() => RemoveInputPrivate(input), owner);
    }


    /// <summary> Attempts to remove a range of <paramref name="inputs"/> from the <see cref="StringChain"/>. </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
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

    /// <summary> Attempts to insert an <paramref name="input"/> in the <see cref="StringChain"/> at the given <paramref name="index"/>. </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <param name="required"> Whether the input is required for this chain. Only relevant for <see cref="IWatchable"/> inputs. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool Insert(object input, int index, bool required = false, object owner = null)
    {
      return MutationGuard(() => InsertPrivate(input, index, required), owner);
    }

    /// <summary> Attempts to insert an <paramref name="input"/> in the <see cref="StringChain"/> before the given <paramref name="target"/>. </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <param name="required"> Whether the input is required for this chain. Only relevant for <see cref="IWatchable"/> inputs. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool InsertBefore(object input, object target, bool required = false, object owner = null)
    {
      int index = _inputs.IndexOf(target);
      if(index < 0) { return false; }
      index = index == 0 ? 0 : index - 1;
      return Insert(input, index, required, owner);
    }

    /// <summary> Attempts to insert an <paramref name="input"/> in the <see cref="StringChain"/> after the given <paramref name="target"/>. </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <param name="required"> Whether the input is required for this chain. Only relevant for <see cref="IWatchable"/> inputs. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool InsertAfter(object input, object target, bool required = false, object owner = null)
    {
      int index = _inputs.IndexOf(target);
      if(index < 0) { return false; }
      return Insert(input, index + 1, required, owner);
    }

    /// <summary> Attempts to clear all inputs from the <see cref="StringChain"/>. </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool Clear(object owner = null)
    {
      return MutationGuard(ClearPrivate, owner);
    }

    /// <inheritdoc/>
    protected override bool CheckFatalDestruction(IWatchable dependency) => _requiredInputs.Contains(dependency);

    /// <inheritdoc/>
    protected override void Evaluate()
    {
      Value = "";
      foreach(object input in _inputs)
      {
        Value += input.ToString();
      }
    }

    /// <inheritdoc/>
    protected override void BeforeDestroyed()
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

    protected override void ClearValue() => Value = "";

    /// <inheritdoc/>
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

  }
}