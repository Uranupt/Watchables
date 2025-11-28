using System;


namespace Watchables
{
	public class ProxyWatchable<T> : Watchable<T>
	{

		private ReadOnlyWatchable<T> _readOnlyWrapper;
		private event Action Changed;
		private Watchable<T> _wrappedValue;

		public override Watchable<T> ReadOnlyWrapper
		{
			get
			{
				_readOnlyWrapper ??= new ReadOnlyWatchable<T>(this);
				return _readOnlyWrapper;
			}
		}

		public ProxyWatchable(Watchable<T> value)
		{
			_wrappedValue = value;
			_wrappedValue.AddListener(Update);
		}

		public void ChangeValue(Watchable<T> value)
		{
			_wrappedValue.RemoveListener(Update);
			_wrappedValue = value;
			_wrappedValue.AddListener(Update);
			Changed?.Invoke();
		}

		private void Update()
		{
			Changed?.Invoke();
		}

		public override void AddListener(Action listener)
		{
			if(listener == Update)
			{
				string msg = "An update loop was found for a ProxyWatchable.";
				throw new StackOverflowException(msg);
			}
			Changed -= listener;
			Changed += listener;
		}

		public override void RemoveListener(Action listener)
		{
			Changed -= listener;
		}

		public override T ToValue()
		{
			return _wrappedValue;
		}

	}

}