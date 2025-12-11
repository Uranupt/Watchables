using System;


namespace Watchables
{
	/// <summary>
	/// Extension of <see cref="OwnableBase"/> which provides common implementation for <see cref="IWatchable"/>.
	/// </summary>
	public abstract class WatchableBase : OwnableBase, IWatchable
	{

    private bool _isUpdating;

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

		/// <summary> This method clears and cleans up any contained values of this instance. </summary>
		protected abstract void ClearValue();

    /// <summary> 
    /// Attempts to call <see cref="BeforeChanged"/> and then invoke the <see cref="Changed"/> event.
    /// Will fail if this instance is destroyed. 
    /// Automatically detects and will throw an error on recurrsion.
    /// </summary>
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

		/// <summary> Authoritatively marks the instance as destroyed and cleans up any values and listeners. </summary>
		protected void DestroyProtected()
		{
			_owner = null;
			IsDestroyed = true;
			ClearValue();
			BeforeDestroyed();
			Destroyed?.Invoke(this);
			Destroyed = null;
      Changed = null;
      Destroyed = null;
    }

		/// <summary> 
		/// Attempt to perform a provided mutation and then call <see cref="InvokeChanged"/>. 
		/// Derived types can override this to define additional mutation-locked states. 
		/// </summary>
		/// <param name="action"> The mutation to perform. </param>
		/// <param name="owner"> The instance's current owner. </param>
		/// <returns> If the mutation was successful. </returns>
		protected virtual bool MutationGuard(Action action, object owner = null)
		{
			if(IsDestroyed) { return false; }
			action?.Invoke();
			InvokeChanged();
			return true;
		}

		/// <summary> Override this method to define behavior directly before <see cref="Changed"/> is invoked. </summary>
		protected virtual void BeforeChanged()
		{

		}

    /// <summary> Override this method to define behavior directly before <see cref="Destroyed"/> is invoked. </summary>
    protected virtual void BeforeDestroyed()
		{

		}

	}
}
