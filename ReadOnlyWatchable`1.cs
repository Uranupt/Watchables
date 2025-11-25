using System;

namespace Watchables
{
	public class ReadOnlyWatchable<T> : Watchable<T>
	{

		private readonly Watchable<T> _baseValue;

		public override IsOwned => _baseValue.IsOwned;
		public override IsDestroyed => _baseValue.IsDestroyed;

		public ReadOnlyWatchable(Watchable<T> baseValue)
		{
			_baseValue = baseValue;
			_readOnlyWrapper = this;
		}

		public override T ToValue() => baseValue.ToValue();

		private void OnChanged() => InvokeChanged();
		
		private void OnDestroyed()
		{
			_baseValue.Changed -= OnChanged();
			_baseValue.Destroyed -= OnDestroyed();
			
		}

	}
}
