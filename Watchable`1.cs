

namespace Watchables
{

	public abstract class Watchable<T> : Watchable
	{

		protected T _value;
		public abstract Watchable<T> ReadOnlyWrapper { get; }

		public static implicit operator T(Watchable<T> watchable) => watchable.ToValue();

		public virtual T ToValue()
		{
			return _value;
		}

		public override string ToString()
		{
			return ToValue().ToString();
		}
	}
}