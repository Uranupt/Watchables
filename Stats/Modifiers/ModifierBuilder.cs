using System;
using UnityEngine;
using System.Collections.Generic;
using Watchables.Transformations;
//using Battle.Actors;
//using ResourceManagement.Database;
//using Battle.Entities.Conditions;
//using Battle.Actors.Skills;

namespace Watchables.Stats.Modifiers
{
	//TODO: Full Re-evaluation of ModifierBuilder
	//[Serializable]
	//public class ModifierBuilder
	//{

	//	public enum ScalingType
	//	{
	//		Flat,
	//		Multiplied,
	//		Ratio,
	//		NormalizedRatio,
	//		InverseRatio,
	//		NormalizedInverseRatio
	//	}

	//	[Serializable]
	//	public class AutoEffect
	//	{

	//		private enum Location
	//		{
	//			Stats,
	//			ConditionModifier,
	//			SkillModifier,
	//			Bucket,
	//			Resource
	//		}

	//		[SerializeField] private Location _location;
	//		[SerializeField] private string _value;
	//		[SerializeField] private OperationType _operation;
	//		[SerializeField] private ScalingType _scaling;
	//		[SerializeField] private float _scale;
	//		[SerializeField] private ResourceObject<Condition> _condition;
	//		[SerializeField] private ResourceObject<Skill> _skill;
	//		[SerializeField] private ResourceName _resourceName;

	//		public StatEffect GetEffect(string name, Actor actor)
	//		{
	//			Watchable<float> value;
	//			switch(_location)
	//			{
	//				case Location.Stats:
	//				{
	//					value = actor.Stats[_value];
	//					break;
	//				}
	//				case Location.ConditionModifier:
	//				{
	//					value = actor.ConditionModifiers[_condition.ID, _value];
	//					break;
	//				}
	//				case Location.SkillModifier:
	//				{
	//					value = actor.SkillModifiers[_skill.ID, _value];
	//					break;
	//				}
	//				case Location.Bucket:
	//				{
	//					if(!actor.Bucket.Fetch(_value, out value))
	//					{
	//						value = new SimpleWatchable<float>();
	//						actor.Bucket.Store(_value, value);
	//					}
	//					break;
	//				}
	//				case Location.Resource:
	//				{
	//					value = actor.Resources[_resourceName];
	//					break;
	//				}
	//				default:
	//				{
	//					value = new SimpleWatchable<float>(0);
	//					break;
	//				}
	//			}
	//			switch(_scaling)
	//			{
	//				case ScalingType.Multiplied:
	//				{
	//					value = new MultiplicationTransformation(value, _scale);
	//					break;
	//				}
	//				case ScalingType.Ratio:
	//				{
	//					value = new PercentageTransformation(new SimpleWatchable<float>(_scale), value);
	//					break;
	//				}
	//				case ScalingType.NormalizedRatio:
	//				{
	//					value = new PercentageTransformation(new SimpleWatchable<float>(_scale), value, false, true);
	//					break;
	//				}
	//				case ScalingType.InverseRatio:
	//				{
	//					value = new PercentageTransformation(value, new SimpleWatchable<float>(_scale));
	//					break;
	//				}
	//				case ScalingType.NormalizedInverseRatio:
	//				{
	//					value = new PercentageTransformation(value, new SimpleWatchable<float>(_scale), false, true);
	//					break;
	//				}
	//			}
	//			return new StatEffect(name, value, _operation);
	//		}

	//	}

	//	[SerializeField] private string _name;
	//	[SerializeField] private float _baseValue;
	//	[SerializeField] private ModifierTargets _modifierTargets;
	//	[SerializeField] private List<AutoEffect> _effects;

	//	public List<StatEffect> GetEffects(Actor actor)
	//	{
	//		List<StatEffect> effects = new()
	//		{
	//			new StatEffect(_name, _baseValue, OperationType.SetBase)
	//		};
	//		foreach(AutoEffect effect in _effects)
	//		{
	//			effects.Add(effect.GetEffect(_name, actor));
	//		}
	//		return effects;
	//	}

	//	public void BuildLocalModifier(Actor actor, StatBlock stats, FlagStat<ModifierTags> parentTags)
	//	{
	//		LocalModifier localModifier = new(stats[_name], _modifierTargets, parentTags);
	//		actor.GlobalModifiers.AddLocalModifier(localModifier);
	//	}

	//}
}