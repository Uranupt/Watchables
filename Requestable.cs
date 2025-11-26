using System.Collections.Generic;
using System;

namespace Watchables
{
	public class Requestable : Watchable<bool>
	{

		private readonly HashSet<object> _requesters = new();

		public readonly bool DefaultState;

		public Requestable(bool defaultState)
		{
			DefaultState = defaultState;
			_value = DefaultState;
		}

		public void AddRequest(object requester)
		{
			if(_requesters.Add(requester))
			{
        Evaluate();
      }			
		}

		public void RemoveRequest(object requester)
		{
      if(_requesters.Remove(requester))
      {
        Evaluate();
      }
    }

		public void Clear()
		{
			_requesters.Clear();
			_value = DefaultState;
			InvokeChanged();
		}

		private void Evaluate()
		{
			bool state = DefaultState ? _requesters.Count == 0 : _requesters.Count > 0;
			if(state == _value) { return; }
			_value = state;
      InvokeChanged();
    }

	}
}