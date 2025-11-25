using System;


namespace Watchables
{
	/// <summary>
	/// Base class for the Watchable system of subscribable classes. 
	/// Provides the core <see cref="Changed"/> and <see cref="Destroyed"/> events, as well as ownership logic.
	/// </summary>
	public abstract class Watchable
	{

		private object _owner;
		private bool _isUpdating;
		
		/// <summary> 
		/// Whether this <see cref="Watchable"/> currently has a defined owner. If this is true, the owner is required to
		/// call <see cref="TryDestroy"/> and <see cref="TryClearListeners"/>.
		/// </summary>
		public virtual bool IsOwned => _owner != null;
		/// <summary> Whether this <see cref="Watchable"/> has already been destroyed. </summary>
		public virtual bool IsDestroyed { get; private set; } = false;
    /// <summary> The event fired when the underlying value changes. </summary>
    public event Action Changed;
    /// <summary> The event fired when <see cref="TryDestroy"/> is successfully called. </summary>
    public event Action<Watchable> Destroyed;

		/// <summary>
		/// Attempts to clear the <see cref="Watchable"/>'s value and listeners and mark it destroyed. 
		/// Requires the owner to be passed if <see cref="IsOwned"/> is true. Calls <see cref="Destroyed"/> when done.
		/// </summary>
		public virtual bool Destroy(object owner = null)
		{
			if((_owner != null && owner != _owner) || IsDestroyed) { return false; }
			DestroyProtected();
			return true;
		}

		/// <summary>
		/// Attempts to set the <see cref="Watchable"/>'s owner. Only succeeds if there is no current owner.
		/// </summary>
		public virtual bool SetOwner(object owner)
		{
			if(_owner != null || owner == null || IsDestroyed){ return false; }
			_owner = owner;
			return true;
		}

		/// <summary>
		/// Attempts to clear the <see cref="Watchable"/>'s owner. Requires the current owner to be passed.
		/// </summary>
		public virtual bool ClearOwner(object owner)
		{
			if(_owner != owner || IsDestroyed) { return false; }
			_owner = null;
			return true;
		}

		/// <summary> Converts the underlying value to a string. </summary>
		public new abstract string ToString();

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
			ClearListenersProtected();
			_owner = null;
			IsDestroyed = true;
			ClearValue();
			Destroyed?.Invoke(this);
			Destroyed = null;
		}

		protected void ClearListeners()
		{
			Changed = null;
			Destroyed = null;
		}

	}
}
