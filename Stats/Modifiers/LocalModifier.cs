using System;
using System.Collections.Generic;


namespace Watchables.Stats.Modifiers
{
	public class LocalModifier
	{

		private readonly FlagStat<ModifierTags> _tags;
		private readonly FloatStat _stat;
		private List<StatEffect> _currentEffects = new();

		public ModifierTargets Targets { get; private set; }
		public ModifierTags Tags => _tags;
		public event Action<LocalModifier> TagsChanged;


		public LocalModifier(FloatStat stat, ModifierTargets targets, FlagStat<ModifierTags> tags)
		{
			Targets = targets;
			_tags = tags;
			_stat = stat;
		}

		public void UpdateGlobalModifiers(List<StatEffect> effects)
		{
			_stat.RemoveEffects(_currentEffects);
			foreach(StatEffect effect in _currentEffects)
			{
				effect.Removed -= RemoveGlobalModifier;
			}
			_currentEffects = effects;
			_stat.AddEffects(_currentEffects);
			foreach(StatEffect effect in _currentEffects)
			{
				effect.Removed += RemoveGlobalModifier;
			}
		}

		public void AddGlobalModifier(StatEffect effect)
		{
			if(_currentEffects.Contains(effect)) { return; }
			_currentEffects.Add(effect);
			_stat.AddEffect(effect);
			effect.Removed += RemoveGlobalModifier;
		}

		public void RemoveGlobalModifier(StatEffect effect)
		{
			if(!_currentEffects.Contains(effect)) { return; }
			_currentEffects.Remove(effect);
			_stat.RemoveEffect(effect);
			effect.Removed -= RemoveGlobalModifier;
		}

		private void OnTagsChanged()
		{
			TagsChanged?.Invoke(this);
		}

	}
}