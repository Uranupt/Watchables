using System;


namespace Watchables
{
  /// <summary>
  /// An implementation of <see cref="IWatchable{T}"/> which is directly mutable via <see cref="SetValue"/>.
  /// </summary>
  public sealed class BasicWatchable<T> : WatchableBase<T>, ISealable
	{

		/// <inheritdoc/>
    public bool IsSealed { get; private set; }

    public BasicWatchable()
		{

		}

		public BasicWatchable(T value)
		{
      Value = value;
		}

    /// <inheritdoc/>
    public bool SetSealed(bool sealedState, object owner = null)
    {
      if(IsOwned && !CompareToOwner(owner)) { return false; }
      IsSealed = sealedState;
      return true;
    }

    /// <summary> Attempts to set the <paramref name="value"/>. </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool SetValue(T value, object owner = null) => MutationGuard(() => Value = value, owner);

    /// <inheritdoc/>
    protected override bool MutationGuard(Action action, object owner = null)
		{
			if(IsSealed && !CompareToOwner(owner)) { return false; }
			return base.MutationGuard(action, owner);
		}

	}
}