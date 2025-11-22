using System;


namespace Watchables.Stats.Modifiers
{
	public static class ModifierParser
	{
		public static Watchable<float> Parse(string value, StatBlock modifiers)
		{
			if(value is null)
			{
				return new SimpleWatchable<float>(0);
			}
			if(float.TryParse(value, out float resl))
			{
				return new SimpleWatchable<float>(resl).ReadOnlyWrapper;
			}
			else if(modifiers.Contains(value))
			{
				return modifiers[value].ReadOnlyWrapper;
			}
			else
			{
				throw new ArgumentException("Invalid Modifier Name: " + value);
			}
		}
	}
}