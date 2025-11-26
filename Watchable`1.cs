

namespace Watchables
{

	/// <summary>
	/// Generic type extension base of <see cref="Watchable"/>. Implements <see cref="IWatchable{T}"/>. 
	/// Provides implicit casting to <typeparamref name="T"/> and access to a wrapper <see cref="ReadOnlyWatchable{T}"/>.
	/// </summary>
	public abstract class Watchable<T> : Watchable, IWatchable<T>
	{

		protected ReadOnlyWatchable<T> _readOnlyWrapper;
		protected T _value;

		/// <summary> A read-only version of this <see cref="Watchable{T}"/>. </summary>
		public ReadOnlyWatchable<T> ReadOnlyWrapper => _readOnlyWrapper ??= new(this);

		public static implicit operator T(Watchable<T> watchable) => watchable.ToValue();

		public virtual T ToValue() => _value;
		/// <summary> Returns a string representation of the underlying value. </summary>
		public override string ToString() => ToValue().ToString();

		protected override void ClearValue() => _value = default;

  }
}
