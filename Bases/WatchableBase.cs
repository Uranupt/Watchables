using System;


namespace Watchables
{
	/// <summary>
	/// Base class for the Watchable system. Implements <see cref="IWatchable"/>.
	/// </summary>
	public abstract class WatchableBase : OwnableBase, IWatchable
	{

    protected bool _isUpdating;

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
    public override bool SetOwner(object owner)
		{
			if(IsDestroyed) { return false; }
			return base.SetOwner(owner);
		}

    /// <inheritdoc/>
    public override bool ClearOwner(object owner)
		{
			if(IsDestroyed) { return false; }
			return base.ClearOwner(owner);
		}

		protected abstract void ClearValue();

		protected void InvokeChanged()
		{
			if(IsDestroyed) { return; }
			if(_isUpdating)
			{
				throw new InvalidOperationException("Update feedback loop found.");
			}
			try
			{
				_isUpdating = true;
				BeforeChanged();
				Changed?.Invoke();
			}
			finally
			{
				_isUpdating = false;
			}
		}

		protected virtual void DestroyProtected()
		{
			_owner = null;
			IsDestroyed = true;
			ClearValue();
			Destroyed?.Invoke(this);
			Destroyed = null;
      ClearListeners();
    }

		protected virtual bool MutationGuard(Action action, object owner = null)
		{
			if(IsDestroyed) { return false; }
			action.Invoke();
			InvokeChanged();
			return true;
		}

		protected virtual void BeforeChanged()
		{

		}

		protected void ClearListeners()
		{
			Changed = null;
			Destroyed = null;
		}

	}
}
