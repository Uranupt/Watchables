using System;
using System.Collections.Generic;


namespace Watchables
{
  public sealed class FlagRequestable<T> : WatchableBase<T> where T : struct, Enum
  {

    private readonly Dictionary<object, T> _requests = new();
    
    public void AddRequest(object requester, T value)
    {
      MutationGuard(
        () =>
        {
          _requests[requester] = _requests.TryGetValue(requester, out T curr) ? curr.With(value) : value;
          Evaluate();
        }
      );
    }

    public void RemoveRequest(object requester, T value)
    {
      MutationGuard(
        () =>
        {
          if(!_requests.TryGetValue(requester, out T curr)) { return; }
          curr = curr.Without(value);
          if(curr.ToULong() == 0)
          {
            _requests.Remove(requester);
          }
          else
          {
            _requests[requester] = curr;
          }
          Evaluate();
        }
      );
    }

    protected override void DestroyProtected()
    {
      _requests.Clear();
      base.DestroyProtected();
    }

    private void Evaluate()
    {
      _value = default(T).With(_requests.Values);
    }
  }
}
