using System;
using System.Collections.Generic;


namespace Watchables
{
  /// <summary>
  /// Allows for objects to request bits of the <see cref="FlagsAttribute"/> marked <see cref="Enum"/> type <typeparamref name="T"/>
  /// be set. The value of this instance will be the combination of all currently requested values.
  /// </summary>
  public sealed class FlagRequestable<T> : WatchableBase<T> where T : struct, Enum
  {

    private readonly Dictionary<object, T> _requests = new();
    
    /// <summary> 
    /// Add the <paramref name="value"/>'s bits to a request associated with the <paramref name="requester"/>, or creates a new one. 
    /// </summary>
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

    /// <summary> Remove the <paramref name="value"/>'s bits from the request associated with the <paramref name="requester"/>. </summary>
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

    /// <inheritdoc/>
    protected override void BeforeDestroyed()
    {
      _requests.Clear();
    }

    private void Evaluate()
    {
      Value = default(T).With(_requests.Values);
    }
  }
}
