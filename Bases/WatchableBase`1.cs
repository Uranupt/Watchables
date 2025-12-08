

namespace Watchables
{

  /// <summary>
  /// Generic type extension base of <see cref="WatchableBase"/>.Provides implicit casting to <typeparamref name="T"/>.
  /// </summary>
  /// <remarks> Implements <see cref="IWatchable{T}"/> and <see cref="IAsReadOnly{T}"/> </remarks>
  public abstract class WatchableBase<T> : WatchableBase, IAsReadOnly<T>
	{

		private ReadOnlyWatchable<T> _readOnlyWrapper;

    /// <inheritdoc/>
    public T Value { get; protected set; }

		public static implicit operator T(WatchableBase<T> watchable) => watchable.Value;

		/// <summary> Returns a string representation of the underlying value. </summary>
		public override string ToString() => Value.ToString();

    /// <inheritdoc/>
		public ReadOnlyWatchable<T> AsReadOnly() => _readOnlyWrapper ??= new(this);

    /// <inheritdoc/>
    protected override void ClearValue() => Value = default;

  }
}
