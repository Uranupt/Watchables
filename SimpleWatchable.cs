using System;


namespace Watchables
{
	public class SimpleWatchable<T> : Watchable<T>
	{


		public SimpleWatchable()
		{

		}

		public SimpleWatchable(T value)
		{
      _value = value;
		}

		public bool SetValue(T value, object owner = null)
		{
			if(IsDestroyed || (IsOwned && !CompareToOwner(owner)){ return false; }
			_value = value;
			InvokeChanged();
			return true;
		}

	}
}