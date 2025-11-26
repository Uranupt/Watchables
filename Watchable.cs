using System;


namespace Watchables
{
	/// <summary>
	/// Base class for the Watchable system. Implements <see cref="IWatchable"/>.
	/// </summary>
	public abstract class Watchable : IWatchable
	{

		private object _owner;
		private bool _isUpdating;

		public bool IsOwned => _owner != null;
		public bool IsDestroyed { get; private set; } = false;
    public event Action Changed;
    public event Action<IWatchable> Destroyed;

		public bool Destroy(object owner = null)
		{
			if((_owner != null && owner != _owner) || IsDestroyed) { return false; }
			DestroyProtected();
			return true;
		}

		public bool SetOwner(object owner)
		{
			if(_owner != null || owner == null || IsDestroyed){ return false; }
			_owner = owner;
			return true;
		}

		public bool ClearOwner(object owner)
		{
			if(_owner != owner || IsDestroyed) { return false; }
			_owner = null;
			return true;
		}

		protected abstract void ClearValue();

		protected void InvokeChanged()
		{
			if(IsDestroyed) { return; }
			if(_isUpdating)
			{
				throw new InvalidOperationException("Update feeback loop found.");
			}
			try
			{
				_isUpdating = true;
				Changed?.Invoke();
			}
			finally
			{
				_isUpdating = false;
			}
		}

		protected void DestroyProtected()
		{
			_owner = null;
			IsDestroyed = true;
			ClearValue();
			Destroyed?.Invoke(this);
			Destroyed = null;
      ClearListeners();
    }

		protected void ClearListeners()
		{
			Changed = null;
			Destroyed = null;
		}

	}
}
