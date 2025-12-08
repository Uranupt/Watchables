using System.Collections.Generic;


namespace Watchables
{
  /// <summary>
  /// Allows for objects to request <typeparamref name="T"/> values be added to a <see cref="Tags{T}"/> value. As long as
  /// any requests exist for a given <typeparamref name="T"/> value, it will be present in the value of this instance.
  /// </summary>
  public sealed class TagsRequestable<T> : WatchableBase<Tags<T>> where T : Tag<T>
  {

    private readonly Dictionary<object, Tags<T>> _requests = new();


    /// <summary>
    /// Add the <paramref name="value"/> to a request associated with the <paramref name="requester"/>, or creates a new one.
    /// </summary>
    public void AddRequest(object requester, T value)
    {
      AddRequest(requester, new Tags<T>(value));
    }

    /// <inheritdoc cref="AddRequest(object, T)"/>
    public void AddRequest(object requester, Tags<T> value)
    {
      if(value.IsNone) { return; }
      MutationGuard(
        () =>
        {
          AddRequestPrivate(requester, value);
          Evaluate();
        }
      );
    }

    /// <summary>
    /// Remove the <paramref name="value"/> from the request associated with the <paramref name="requester"/>.
    /// </summary>
    public void RemoveRequest(object requester, T value)
    {
      RemoveRequest(requester, new Tags<T>(value));
    }

    /// <inheritdoc cref="RemoveRequest(object, T)"/>
    public void RemoveRequest(object requester, Tags<T> value)
    {
      if(value.IsNone) { return; }
      MutationGuard(
        () =>
        {
          RemoveRequestPrivate(requester, value);
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
      Value = default;
      foreach(Tags<T> request in  _requests.Values)
      {
        Value += request;
      }
    }

    private void AddRequestPrivate(object requester, Tags<T> value)
    {
      if(_requests.TryGetValue(requester, out Tags<T> curr))
      {
        value += curr;
      }
      _requests[requester] = value;
    }

    private void RemoveRequestPrivate(object requester, Tags<T> value)
    {
      if(!_requests.TryGetValue(requester, out Tags<T> curr)) { return; }
      curr -= value;
      if(curr.IsNone)
      {
        _requests.Remove(requester);
      }
      else
      {
        _requests[requester] = curr;
      }
    }

  }
}
