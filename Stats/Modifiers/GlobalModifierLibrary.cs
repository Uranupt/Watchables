using System;
using System.Collections.Generic;


namespace Watchables.Stats.Modifiers
{
	public class GlobalModifierLibrary
	{

		private readonly Dictionary<ModifierTarget, List<GlobalModifier>> _modifiers = new();
		private readonly Dictionary<ModifierTarget, List<LocalModifier>> _localModifiers = new();

		public GlobalModifierLibrary()
		{
			foreach(ModifierTarget target in Enum.GetValues(typeof(ModifierTarget)))
			{
				_modifiers[target] = new List<GlobalModifier>();
				_localModifiers[target] = new List<LocalModifier>();
			}
		}

		public void AddModifier(GlobalModifier modifier)
		{
			foreach(ModifierTarget target in modifier.Targets.GetTargets())
			{
				if(_modifiers[target].Contains(modifier)) { continue; }
				_modifiers[target].Add(modifier);
				foreach(LocalModifier local in _localModifiers[target])
				{
					if(local.Tags.MultiStateCheck(modifier.FullMatch, modifier.PartialMatch, modifier.NoMatch))
					{
						local.AddGlobalModifier(modifier.Effect);
					}
				}
			}
			modifier.Removed += RemoveModifier;
		}

		public void RemoveModifier(GlobalModifier modifier)
		{
			modifier.Removed -= RemoveModifier;
			foreach(ModifierTarget target in modifier.Targets.GetTargets())
			{
				_modifiers[target].Remove(modifier);
			}
		}

		public void AddLocalModifier(LocalModifier modifier)
		{
			foreach(ModifierTarget target in modifier.Targets.GetTargets())
			{
				if(_localModifiers[target].Contains(modifier)) { continue; }
				_localModifiers[target].Add(modifier);
				foreach(GlobalModifier global in _modifiers[target])
				{
					if(modifier.Tags.MultiStateCheck(global.FullMatch, global.PartialMatch, global.NoMatch))
					{
						modifier.AddGlobalModifier(global.Effect);
					}
				}
			}
			modifier.TagsChanged += OnLocalTagsChanged;
		}

		private void OnLocalTagsChanged(LocalModifier modifier)
		{
			List<StatEffect> globals = new();
			foreach(ModifierTarget target in modifier.Targets.GetTargets())
			{
				foreach(GlobalModifier global in _modifiers[target])
				{
					if(!globals.Contains(global.Effect)
						&& modifier.Tags.MultiStateCheck(global.FullMatch, global.PartialMatch, global.NoMatch))
					{
						globals.Add(global.Effect);
					}
				}
			}
			modifier.UpdateGlobalModifiers(globals);
		}

	}
}
