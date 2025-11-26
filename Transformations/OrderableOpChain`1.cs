using System;
using System.Collections.Generic;


namespace Watchables
{
  public abstract class OrderableOpChain<T> : OpChain<T>
  {

    public bool InsertStep(OpChainStep<T> step, int index, object owner = null)
    {
      if(!CanEdit(owner)) { return false; }
    }

    public bool MoveStep(OpChainStep<T> step, int dest, bool swapWithDest, object owner = null)
    {
      if(!CanEdit(owner)) { return false; }
    }

    public bool MoveStep(int index, int dest, bool swapWithDest, object owner = null)
    {
      if(!CanEdit(owner)) { return false; }
    }

    public bool Sort(IComparer<OpChainStep<T>> comparer, object owner = null)
    {
      if(!CanEdit(owner)) { return false; }
    }

    public bool Sort(Comparison<OpChainStep<T>> comparison, object owner = null)
    {
      if(!CanEdit(owner)) { return false; }
    }

    public bool Sort(Func<int, OpChainStep<T>> sortFunc, object owner = null)
    {
      if(!CanEdit(owner)) { return false; }
    }

  }
}