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
		public virtual bool TryDestroy(object owner = null)
		{
			if((_owner != null && owner != _owner) || IsDestroyed) { return false; }
			_owner = null;
			IsDestroyed = true;
			ClearValue();
			TryClearListeners(owner);
			Destroyed?.Invoke(this);
			Destroyed = null;
			return true;
		}

		/// <summary>
		/// Attempts to set the <see cref="Watchable"/>'s owner. Only succeeds if there is no current owner.
		/// </summary>
		public virtual bool TrySetOwner(object owner)
		{
			if(_owner != null || owner == null){ return false; }
			_owner = owner;
			return true;
		}

		/// <summary>
		/// Attempts to clear the <see cref="Watchable"/>'s owner. Requires the current owner to be passed.
		/// </summary>
		public virtual bool TryClearOwner(object owner)
		{
			if(_owner != owner) { return false; }
			_owner = null;
			return true;
		}

    /// <summary>
    /// Attempts to clear all listeners from <see cref="Changed"/>. Requires the owner to be passed if <see cref="IsOwned"/> is true.
    /// </summary>
    public virtual bool TryClearListeners(object owner = null)
		{
      if(_owner != null && _owner != owner) { return false; }
			Changed = null;
			return true;
    }

		/// <summary> Converts the underlying value to a string. </summary>
		public new abstract string ToString();

		protected abstract void ClearValue();

		protected void InvokeChanged() => Changed?.Invoke();

	}
}
