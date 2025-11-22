using System.Collections.Generic;

namespace Watchables.Stats
{
	public class StatBlock
	{

    private readonly Dictionary<string, FloatStat> _stats = new();

    public FloatStat this[string name] => GetStat(name);

		public FloatStat GetStat(string name)
		{
			if(!_stats.ContainsKey(name))
			{
        _stats[name] = new FloatStat();
			}
			return _stats[name];
		}

		public void AddEffect(string target, StatEffect effect)
		{
			this[target].AddEffect(effect);
		}

		public void RemoveEffect(string target, StatEffect effect)
		{
			this[target].RemoveEffect(effect);
		}

		public void AddEffects(string target, List<StatEffect> effects)
		{
			foreach(StatEffect effect in effects)
			{
				AddEffect(target, effect);
			}
		}

		public void AddEffects(List<(string, StatEffect)> effects)
		{
			foreach((string target, StatEffect effect) in effects)
			{
				AddEffect(target, effect);
			}
		}

		public bool Contains(string name)
		{
			return _stats.ContainsKey(name);
		}

	}
}