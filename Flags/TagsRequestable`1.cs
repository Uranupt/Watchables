using System.Collections.Generic;


namespace Watchables
{
  public sealed class TagsRequestable<T> : WatchableBase<Tags<T>> where T : Tag<T>
  {

    private readonly Dictionary<object, Tags<T>> _requests;

    public TagsRequestable()
    {

    }

    public void AddRequest(object requester, T value)
    {
      AddRequest(requester, new Tags<T>(value));
    }

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

    public void RemoveRequest(object requester, T value)
    {
      RemoveRequest(requester, new Tags<T>(value));
    }

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

    protected override void DestroyProtected()
    {
      _requests.Clear();
      base.DestroyProtected();
    }

    private void Evaluate()
    {
      _value = default;
      foreach(Tags<T> request in  _requests.Values)
      {
        _value += request;
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
