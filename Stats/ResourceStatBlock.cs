using System.Collections.Generic;
using System;

namespace Watchables.Stats
{

  public class ResourceStatBlock
  {

    private readonly Dictionary<string, ResourceStat> _resources = new();

    public ResourceStat this[string name] => GetResource(name);

		public ResourceStat GetResource(string name)
		{
			if(!_resources.ContainsKey(name))
			{
				_resources[name] = new ResourceStat();
			}
      return _resources[name];
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
      return _resources.ContainsKey(name);
    }

  }
}