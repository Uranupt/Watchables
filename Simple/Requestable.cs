using System.Collections.Generic;


namespace Watchables
{
	/// <summary>
	/// An <see cref="IWatchable{T}"/> implementation which allows objects to request an inversion of a <see cref="bool"/> value.
	/// So long as there are any requests, the value will be inverted.
	/// </summary>
	public sealed class Requestable : WatchableBase<bool>
	{

		private readonly HashSet<object> _requesters = new();

		/// <summary> The value of this instance when there are no requests. </summary>
		public readonly bool DefaultValue;

		public Requestable(bool defaultValue)
		{
			DefaultValue = defaultValue;
			Value = DefaultValue;
		}

		/// <summary> Attempts to add a request to invert the <see cref="bool"/> value. </summary>
		/// <returns> Whether the operation was allowed. </returns>
		public bool AddRequest(object requester)
    {
			if(IsDestroyed) { return false; }
			if(_requesters.Add(requester))
			{
				Evaluate();
			}
			return true;
    }

    /// <summary> Attempts to remove a request to invert the <see cref="bool"/> value. </summary>
    /// <returns> Whether the operation was allowed. </returns>
    public bool RemoveRequest(object requester)
    {
      if(IsDestroyed) { return false; }
      if(_requesters.Remove(requester))
      {
        Evaluate();
      }
      return true;
    }

    /// <summary> Attempts to remove all requests. </summary>
		/// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool Clear(object owner = null)
		{
			if(IsOwned && !CompareToOwner(owner)) { return false; }
			return MutationGuard(
				() =>
				{
					_requesters.Clear();
					Value = DefaultValue;
        }
			);
		}

		private void Evaluate()
		{
      bool value = DefaultValue ? _requesters.Count == 0 : _requesters.Count > 0;
			if(value == Value) { return; }
			Value = value;
			InvokeChanged();
    }

	}
}