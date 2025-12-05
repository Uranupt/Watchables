using System.Collections.Generic;
using System;

namespace Watchables
{
	/// <summary>
	/// A <see cref="WatchableBase{bool}"/> implementation of Type <see cref="bool"/> which allows objects to request an inversion of its default state.
	/// So long as there are any requests, the value will be inverted.
	/// </summary>
	public sealed class Requestable : WatchableBase<bool>
	{

		private readonly HashSet<object> _requesters = new();

		/// <summary> The state of this instance when there are no requests. </summary>
		public readonly bool DefaultState;

		public Requestable(bool defaultState)
		{
			DefaultState = defaultState;
			_value = DefaultState;
		}

		public bool AddRequest(object requester) => MutationGuard(() => _requesters.Add(requester));
		public bool RemoveRequest(object requester) => MutationGuard(() => _requesters.Remove(requester));

		public bool Clear(object owner = null)
		{
			if(IsOwned && !CompareToOwner(owner)) { return false; }
			return MutationGuard(_requesters.Clear);
		}

		protected override void BeforeChanged()
		{
			Evaluate();
		}

		private void Evaluate()
		{
      _value = DefaultState ? _requesters.Count == 0 : _requesters.Count > 0;
    }

	}
}