using System;
using System.Collections.Generic;


namespace Watchables.Stats
{
	public class FlagStat<T> : WatchableBase<T> where T : struct, Enum
	{

		private ReadOnlyWatchable<T> _readOnly;
		private event Action Changed;

		private readonly Dictionary<string, List<FlagEffect<T>>> _values = new();

		public FlagStat()
		{
			foreach(string name in Enum.GetNames(typeof(T)))
			{
				_values[name] = new List<FlagEffect<T>>();
			}
		}

		public override WatchableBase<T> ReadOnlyWrapper
		{
			get
			{
				_readOnly ??= new ReadOnlyWatchable<T>(this);
				return _readOnly;
			}
		}

		public void AddEffect(FlagEffect<T> effect)
		{
			foreach(string name in effect.Names)
			{
				if(_values[name].Contains(effect)) { return; }
				_values[name].Add(effect);
				_values[name].Sort(FlagEffect<T>.Comparer);
			}
			effect.Updated += Evaluate;
			effect.Removed += RemoveEffect;
			Evaluate();
		}

		public void RemoveEffect(FlagEffect<T> effect)
		{
			effect.Removed -= RemoveEffect;
			effect.Updated -= Evaluate;
			foreach(string name in effect.Names)
			{
				if(!_values[name].Contains(effect)) { return; }
				_values[name].Remove(effect);
				_values[name].Sort(FlagEffect<T>.Comparer);
			}
			Evaluate();
		}

		private void Evaluate()
		{
			string names = "";
			foreach(KeyValuePair<string, List<FlagEffect<T>>> pair in _values)
			{
				if(pair.Value.Count == 0)
				{
					continue;
				}
				if(pair.Value[^1].Operation == FlagOperation.On
					|| pair.Value[^1].Operation == FlagOperation.ForceOn)
				{
					names += pair.Key + ", ";
				}
			}
			if(names == "")
			{
				names = "None";
			}
			names = names.TrimEnd(new char[] { ' ', ',' });
			if(Enum.TryParse(names, out T value))
			{
				_value = value;
				Changed?.Invoke();
			}
			else
			{
				string msg = "Invalid names supplied for Enum: " + typeof(T).Name;
				msg += ". Names were: " + names;
				throw new InvalidOperationException(msg);
			}
		}

		public override void AddListener(Action listener)
		{
			Changed -= listener;
			Changed += listener;
		}

		public override void RemoveListener(Action listener)
		{
			Changed -= listener;
		}
	}
}