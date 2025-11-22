using System;
using System.Collections.Generic;

namespace Watchables.Stats.Modifiers
{
	[Flags]
	public enum ModifierTargets
	{
		None = 0,
		Damage = 1,
		Healing = 1 << 1,
		Range = 1 << 2,
		TargetCount = 1 << 3,
		Cooldown = 1 << 4,
		Charges = 1 << 5,
		Duration = 1 << 6,
		Stacks = 1 << 7,
		EnergyCost = 1 << 8,
		HealthCost = 1 << 9,
	}

	public static class ModifierTargetsExtensions
	{

		public static bool HasTarget(this ModifierTargets targets, ModifierTargets check)
		{
			return (targets & check) == check;
		}

		public static bool HasTarget(this ModifierTargets targets, ModifierTarget check)
		{
			return targets.HasTarget((ModifierTargets)(1 << (int)check));
		}

		public static List<ModifierTarget> GetTargets(this ModifierTargets targets)
		{
			List<ModifierTarget> resl = new();
			if(targets == ModifierTargets.None)
			{
				return resl;
			}
			foreach(string name in targets.ToString().Split(", "))
			{
				resl.Add(Enum.Parse<ModifierTarget>(name));
			}
			return resl;
		}

	}
}