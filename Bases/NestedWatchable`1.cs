

namespace Watchables
{
	public abstract class NestedWatchable<T> : WatchableBase<T>
	{

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

		protected void OnDependencyChanged()
		{
			if(IsDestroyed) { return; }
			Evaluate();
			InvokeChanged();
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
				OnDependencyChanged();
      }
		}

    protected sealed override void DestroyProtected()
    {
			Destroyed += OnDestroyed;
      base.DestroyProtected();
    }

		protected virtual void OnNonFatalDestruction(IWatchable dependency)
		{

		}

		protected abstract void Evaluate();
		protected abstract bool CheckFatalDestruction(IWatchable dependency);
		protected abstract void OnDestroyed(IWatchable self);

	}
}