using System;


namespace Watchables
{
	public sealed class BasicWatchable<T> : WatchableBase<T>, ISealable
	{

		/// <inheritdoc/>
    public bool IsSealed { get; private set; }

    public BasicWatchable()
		{

		}

		public BasicWatchable(T value)
		{
      _value = value;
		}

    /// <inheritdoc/>
    public bool SetSealed(bool sealedState, object owner = null)
    {
      if(IsOwned && !CompareToOwner(owner)) { return false; }
      IsSealed = sealedState;
      return true;
    }

    /// <summary> </summary>
    public bool SetValue(T value, object owner = null) => MutationGuard(() => _value = value, owner);

		protected override bool MutationGuard(Action action, object owner = null)
		{
			if(IsSealed && !CompareToOwner(owner)) { return false; }
			return base.MutationGuard(action, owner);
		}

	}
}