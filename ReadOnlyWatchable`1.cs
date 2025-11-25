using System;

namespace Watchables
{
	public class ReadOnlyWatchable<T> : Watchable<T>
	{

		private readonly Watchable<T> _baseValue;

		public override Watchable<T> ReadOnlyWrapper
		{
			get
			{
				return this;
			}
		}

		public ReadOnlyWatchable(Watchable<T> baseValue)
		{
			_baseValue = baseValue;
		}

		public override T ToValue()
		{
			return _baseValue;
		}

		public override void AddListener(Action listener)
		{
			_baseValue.AddListener(listener);
		}

		public override void RemoveListener(Action listener)
		{
			_baseValue.RemoveListener(listener);
		}

	}
}