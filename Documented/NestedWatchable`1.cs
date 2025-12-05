using System;


namespace Watchables
{
	/// <summary>
	/// Extension of <see cref="WatchableBase{T}"/> for types which are (or have the potential to be) based on other <see cref="IWatchable"/> instances.
	/// </summary>
	public abstract class NestedWatchable<T> : WatchableBase<T>
	{

		protected void Register(IWatchable dependency)
		{
			Unregister(dependency);
			dependency.Changed += InvokeChanged;
			dependency.Destroyed += OnDependencyDestroyed;
		}

		protected void Unregister(IWatchable dependency)
		{
      dependency.Changed -= InvokeChanged;
      dependency.Destroyed -= OnDependencyDestroyed;
    }

		protected void OnDependencyDestroyed(IWatchable dependency)
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

    protected sealed override void DestroyProtected()
    {
			Destroyed += OnDestroyed;
      base.DestroyProtected();
    }

		protected override void BeforeChanged()
		{
      Evaluate();
    }

		protected virtual void OnNonFatalDestruction(IWatchable dependency)
		{

		}

		protected abstract void Evaluate();
		protected abstract bool CheckFatalDestruction(IWatchable dependency);
		protected abstract void OnDestroyed(IWatchable self);

	}
}