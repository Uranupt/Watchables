

namespace Watchables
{
	public abstract class NestedWatchable<T> : Watchable<T>
	{

		protected NestedWatchable()
		{
			Destroyed += OnDestroy;
		}

		protected void Register(IWatchable dependency)
		{
			Unregister(dependency);
			dependency.Changed += OnDependencyChanged;
			dependency.Destroyed += OnDependencyDestroyed;
		}

		protected void Unregister(IWatchable dependency)
		{
      dependency.Changed -= OnDependencyChanged;
      dependency.Destroyed -= OnDependencyDestroyed;
    }

		protected virtual void OnDependencyChanged()
		{
			if(IsDestroyed) { return; }
			Evaluate();
			InvokeChanged();
		}

		protected virtual void OnDependencyDestroyed(IWatchable dependency)
		{
			if(CheckFatalDestruction(dependency))
			{
				DestroyProtected();
			}
			else
			{
				Unregister(dependency);
        OnNonFatalDestruction(dependency);
			}
		}

		protected virtual void OnNonFatalDestruction(IWatchable dependency)
		{

		}

		protected abstract void Evaluate();
		protected abstract bool CheckFatalDestruction(IWatchable dependency);
		protected abstract void OnDestroy(IWatchable self);

	}
}