using System;


namespace Watchables
{
	/// <summary>
	/// Base class for the Watchable system. Implements <see cref="IWatchable"/>.
	/// </summary>
	public abstract class Watchable : IWatchable
	{

		protected object _owner;
    protected bool _isUpdating;

		/// <inheritdoc/>
		public bool IsOwned => _owner != null;
    /// <inheritdoc/>
    public bool IsDestroyed { get; private set; } = false;
    /// <inheritdoc/>
    public event Action Changed;
    /// <inheritdoc/>
    public event Action<IWatchable> Destroyed;

    /// <inheritdoc/>
    public bool Destroy(object owner = null)
		{
			if((_owner != null && owner != _owner) || IsDestroyed) { return false; }
			DestroyProtected();
			return true;
		}

    /// <inheritdoc/>
    public bool SetOwner(object owner)
		{
			if(_owner != null || owner == null || IsDestroyed){ return false; }
			_owner = owner;
			return true;
		}

    /// <inheritdoc/>
    public bool ClearOwner(object owner)
		{
			if(_owner != null && (_owner != owner || IsDestroyed)) { return false; }
			_owner = null;
			return true;
		}

    /// <inheritdoc/>
    public bool CompareToOwner(object owner) => IsOwned && _owner == owner;

		protected abstract void ClearValue();

		protected virtual bool CanEdit(object owner) => !IsDestroyed && (!IsOwned || _owner == owner);

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
