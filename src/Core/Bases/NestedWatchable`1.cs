using System;


namespace Watchables
{
	/// <summary>
	/// Extension of <see cref="WatchableBase{T}"/> for types which are (or have the potential to be) based on other <see cref="IWatchable"/> instances.
	/// </summary>
	public abstract class NestedWatchable<T> : WatchableBase<T>
	{

		/// <summary> Subscribes propagation callbacks to a dependency. </summary>
		protected void Register(IWatchable dependency)
		{
			Unregister(dependency);
			dependency.Changed += InvokeChanged;
			dependency.Destroyed += OnDependencyDestroyed;
		}

    /// <summary> Unsubscribes propagation callbacks from a dependency. </summary>
    protected void Unregister(IWatchable dependency)
		{
      dependency.Changed -= InvokeChanged;
      dependency.Destroyed -= OnDependencyDestroyed;
    }

    /// <inheritdoc/>
		protected override void BeforeChanged()
		{
      Evaluate();
    }

    /// <summary> Override this method to define behavior when a non-required dependency is destroyed. </summary>
		protected virtual void OnNonFatalDestruction(IWatchable dependency)
		{

		}

    /// <summary> 
    /// This method defines the behavior around setting the instance's value when an internal change occurs. 
    /// Never call <see cref="WatchableBase.InvokeChanged"/> from within this method as it will cause recursion. 
    /// </summary>
		protected abstract void Evaluate();

    /// <summary> This method detemines if a destroyed dependency was required for this instance to remain alive. </summary>
		protected abstract bool CheckFatalDestruction(IWatchable dependency);

    private void OnDependencyDestroyed(IWatchable dependency)
    {
      if(CheckFatalDestruction(dependency))
      {
        DestroyProtected();
      }
      else
      {
        Unregister(dependency);
        OnNonFatalDestruction(dependency);
        InvokeChanged();
      }
    }

  }
}