using System;


namespace Watchables
{

	public abstract class Watchable<T>
	{

		protected T _value;
		public abstract Watchable<T> ReadOnlyWrapper { get; }

		public static implicit operator T(Watchable<T> watchable) => watchable.ToValue();

		public abstract void AddListener(Action listener);

		public abstract void RemoveListener(Action listener);

		public virtual T ToValue()
		{
			return _value;
		}

		public new virtual string ToString()
		{
			return ToValue().ToString();
		}
	}
}