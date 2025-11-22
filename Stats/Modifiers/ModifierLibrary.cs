//using Battle.Actors;
//using Battle.Entities;
//using ResourceManagement.Database;
using System.Collections.Generic;

namespace Watchables.Stats.Modifiers
{
	//TODO: Full re-evaluation of ModifierLibrary

	//public class ModifierLibrary<T> where T : IModifiers
	//{

	//	public FloatStat this[ResourceID<T> id, string name] => GetModifier(id, name);
	//	public StatBlock this[ResourceID<T> id] => GetStatBlock(id);

	//	private readonly Actor _actor;
	//	private readonly Dictionary<ResourceID<T>, StatBlock> _statBlocks = new();
	//	private readonly Dictionary<ResourceID<T>, FlagStat<ModifierTags>> _tags = new();

	//	public ModifierLibrary(Actor actor)
	//	{
	//		_actor = actor;
	//	}

	//	public StatBlock GetStatBlock(ResourceID<T> id)
	//	{
	//		if(!_statBlocks.ContainsKey(id))
	//		{
	//			IModifiers target = id.Build();
	//			_tags[id] = target.GetTags();
	//			_statBlocks[id] = new StatBlock();
	//			target.BuildModifiers(_actor);
	//		}
	//		return _statBlocks[id];
	//	}

	//	public FloatStat GetModifier(ResourceID<T> id, string name)
	//	{
	//		return GetStatBlock(id)[name];
	//	}

	//	public FlagStat<ModifierTags> GetTags(ResourceID<T> id)
	//	{
	//		if(!_tags.ContainsKey(id))
	//		{
	//			IModifiers target = id.Build();
	//			_tags[id] = target.GetTags();
	//			_statBlocks[id] = new StatBlock();
	//			target.BuildModifiers(_actor);
	//		}
	//		return _tags[id];
	//	}

	//	public void AddEffect(ResourceID<T> id, StatEffect effect)
	//	{
	//		this[id, effect.TargetName].AddEffect(effect);
	//	}

	//	public void AddEffect(ResourceID<T> id, FlagEffect<ModifierTags> effect)
	//	{
	//		GetTags(id).AddEffect(effect);
	//	}

	//}
}