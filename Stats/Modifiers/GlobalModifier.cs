using System;


namespace Watchables.Stats.Modifiers
{
	public class GlobalModifier
	{

		public StatEffect Effect { get; private set; }
		public ModifierTargets Targets { get; private set; }
		public ModifierTags FullMatch { get; private set; }
		public ModifierTags PartialMatch { get; private set; }
		public ModifierTags NoMatch { get; private set; }
		public event Action<GlobalModifier> Removed;

		public GlobalModifier(Watchable<float> value, ModifierTargets targets, bool addition, ModifierTags fullMatch, ModifierTags partialMatch,
			ModifierTags noMatch)
		{
			Effect = new StatEffect(value, addition ? OperationType.Add : OperationType.Multiply);
			Targets = targets;
			FullMatch = fullMatch;
			PartialMatch = partialMatch;
			NoMatch = noMatch;
		}

		public void Remove()
		{
			Effect.Remove();
			Removed?.Invoke(this);
		}

	}
}