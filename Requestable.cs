using System.Collections.Generic;
using System;

namespace Watchables
{
	public class Requestable : Watchable<bool>
	{

		private readonly List<object> _requesters = new();
		private ReadOnlyWatchable<bool> _readOnly;
		private event Action Changed;

		public readonly bool DefaultState;

		public override Watchable<bool> ReadOnlyWrapper
		{
			get
			{
				_readOnly ??= new ReadOnlyWatchable<bool>(this);
				return _readOnly;
			}
		}

		public Requestable(bool defaultState)
		{
			DefaultState = defaultState;
			_value = DefaultState;
		}

		public void AddRequest(object requester)
		{
			if(_requesters.Contains(requester)) return;
			_requesters.Add(requester);
			Evaluate();
		}

		public void RemoveRequest(object requester)
		{
			if(!_requesters.Contains(requester)) return;
			_requesters.Remove(requester);
			Evaluate();
		}

		public void Clear()
		{
			_requesters.Clear();
			_value = DefaultState;
			Changed?.Invoke();
		}

		private void Evaluate()
		{
			bool state = DefaultState ? _requesters.Count == 0 : _requesters.Count > 0;
			if(state == _value) { return; }

			_value = state;
			Changed?.Invoke();
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