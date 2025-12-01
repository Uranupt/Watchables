using System;
using System.Collections.Generic;


namespace Watchables
{
  public class FlagRequestable<T> : WatchableBase<T> where T : struct, Enum
  {

    private ReadOnlyWatchable<T> _readOnlyWrapper;
    private readonly Dictionary<object, ulong> _requests = new();
    private event Action Changed;

    public override WatchableBase<T> ReadOnlyWrapper
    {
      get
      {
        _readOnlyWrapper ??= new ReadOnlyWatchable<T>(this);
        return _readOnlyWrapper;
      }
    }

    private void Evaluate()
    {
      ulong bits = 0;
      foreach(ulong value in _requests.Values)
      {
        bits |= value;
      }
      _value = (T)Enum.ToObject(typeof(T), bits);
      Changed?.Invoke();
    }

    public void AddRequest(object obj, T value)
    {
      ulong bitValue = Convert.ToUInt64(value);
      if(bitValue == 0) { return; }
      _requests[obj] = _requests.TryGetValue(obj, out ulong bits)
        ? bits | bitValue
        : bitValue;
      Evaluate();
    }

    public void RemoveRequest(object obj, T value)
    {
      ulong bitValue = Convert.ToUInt64(value);
      if(bitValue == 0) { return; }
      if(_requests.TryGetValue(obj, out ulong bits))
      {
        bits &= ~bitValue;
        if(bits == 0)
        {
          _requests.Remove(obj);
        }
        else
        {
          _requests[obj] = bits;
        }
        Evaluate();
      }
    }

    public override void AddListener(Action listener)
    {
      Changed -= listener;
      Changed += listener;
    }

    public override void RemoveListener(Action listener)
    {
      Changed -= listener;
    }
  }
}