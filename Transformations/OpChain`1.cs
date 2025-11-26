using System.Collections.Generic;


namespace Watchables
{
  public abstract class OpChain<T> : NestedWatchable<T> where T : unmanaged
  {

    protected readonly List<OpChainStep<T>> _steps = new();
    protected IValueWrapper<T> _baseValue;

    public OpChain(IValueWrapper<T> baseValue)
    {

    }

    public bool AddStep(OpChainStep<T> step, object owner = null)
    {
      if(!CanEdit(owner)) { return false; }
    }

    public bool RemoveStep(OpChainStep<T> step, object owner = null)
    {
      if(!CanEdit(owner)) { return false; }

    }

  }
}