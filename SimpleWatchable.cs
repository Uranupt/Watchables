using System;


namespace Watchables
{
	public class SimpleWatchable<T> : Watchable<T>
	{

		private ReadOnlyWatchable<T> _readOnly;
		private event Action Changed;

		public override Watchable<T> ReadOnlyWrapper
		{
			get
			{
				_readOnly ??= new ReadOnlyWatchable<T>(this);
				return _readOnly;
			}
		}

		public T Value
		{
			get { return _value; }
			set
			{
				_value = value;
				Changed?.Invoke();
			}
		}

		public SimpleWatchable()
		{

		}

		public SimpleWatchable(T value)
		{
			Value = value;
		}

		public override void AddListener(Action listener)
		{
			Changed -= listener;
			Changed += listener;
		}

		public override void RemoveListener(Action listener)
		{
			Changed -= listener;
		}
	}
}