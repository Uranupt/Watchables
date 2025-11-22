

namespace Watchables
{
	public abstract class NestedWatchable<T> : Watchable<T>
	{

		private ReadOnlyWatchable<T> _readOnly;

		public override Watchable<T> ReadOnlyWrapper
		{
			get
			{
				_readOnly ??= new ReadOnlyWatchable<T>(this);
				return _readOnly;
			}
		}

		protected abstract void Evaluate();

		public override T ToValue()
		{
			Evaluate();
			return _value;
		}

	}
}