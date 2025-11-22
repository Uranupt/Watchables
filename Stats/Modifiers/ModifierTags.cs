using System;


namespace Watchables.Stats.Modifiers
{
	//When changing this or DamageTags ensure changes are mirrored
	[Flags]
	public enum ModifierTags
	{
		None = 0,
		Attack = 1,
		Spell = 1 << 1,
		Ranged = 1 << 2,
		Melee = 1 << 3,
		Physical = 1 << 4,
		Arcane = 1 << 5,
		Fire = 1 << 6,
		Ice = 1 << 7,
		Storm = 1 << 8,
		Holy = 1 << 9,
		Shadow = 1 << 10,
		Buff = 1 << 11,
		Debuff = 1 << 12,
		Ticking = 1 << 13
	}

	public static class ModifierTagsExtensions
	{
		
		public static bool HasAllOf(this ModifierTags tags, ModifierTags check)
		{
			return (tags & check) == check;
		}

		public static bool HasAtLeastOneOf(this ModifierTags tags, ModifierTags check)
		{
			if(check is ModifierTags.None)
			{
				return true;
			}
			return (tags & check) != 0;
		}

		public static bool HasNoneOf(this ModifierTags tags, ModifierTags check)
		{
			if(check == ModifierTags.None)
			{
				return tags != ModifierTags.None;
			}
			return (tags & check) == 0;
		}

		public static bool MultiStateCheck(this ModifierTags tags, ModifierTags allOf, ModifierTags atLeastOneOf, ModifierTags noneOf = ModifierTags.None)
		{
			return tags.HasAllOf(allOf) && tags.HasAtLeastOneOf(atLeastOneOf) && tags.HasNoneOf(noneOf);
		}

	}
}