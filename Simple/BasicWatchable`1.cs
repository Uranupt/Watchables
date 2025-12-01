using System;


namespace Watchables
{
	public sealed class BasicWatchable<T> : WatchableBase<T>, ISealable
	{

		/// <inheritdoc/>
    public bool IsSealed { get; private set; }

    public BasicWatchable()
		{

		}

		public BasicWatchable(T value)
		{
      _value = value;
		}

    /// <inheritdoc/>
    public bool SetSealed(bool sealedState, object owner = null)
    {
      if(IsOwned && !CompareToOwner(owner)) { return false; }
      IsSealed = sealedState;
      return true;
    }

		public bool SetValue(T value)
		{
      if(IsSealed || IsDestroyed) { return false; }
      _value = value;
      return true;
    }

    /// <summary> </summary>
    public bool SetValue(T value, object owner = null)
		{
			if((IsSealed && !IsOwned) || !CanEdit(owner) { return false; }
			_value = value;
			InvokeChanged();
			return true;
		}

	}
}