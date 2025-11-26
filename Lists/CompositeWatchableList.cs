using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.ObjectModel;

namespace Watchables
{
	public class CompositeWatchableList<T> : WatchableList<T>
	{

		private readonly List<Action> _listeners = new();
		private readonly List<WatchableList<T>> _lists = new();
		private readonly bool _allowDuplicates;
		private event Action Changed;

		public CompositeWatchableList(bool allowDuplicates = true)
		{
			_allowDuplicates = allowDuplicates;
			Evaluate();
		}

		public CompositeWatchableList(bool allowDuplicates, params WatchableList<T>[] lists)
		{
			_allowDuplicates = allowDuplicates;
			_lists = new List<WatchableList<T>>(lists);
			Evaluate();
		}

		private void Evaluate()
		{
			List<T> newValue = new();
			foreach(WatchableList<T> list in _lists)
			{
				if(_allowDuplicates)
				{
					newValue.AddRange(list);
				}
				else
				{
					newValue.Union(list);
				}
			}
			_value = newValue;
			Changed?.Invoke();
		}

		public void Add(WatchableList<T> list)
		{
			if(_lists.Contains(list)) { return; }
			_lists.Add(list);
			if(_listeners.Count > 0)
			{
				list.AddListener(Evaluate);
			}
			Evaluate();
		}

		public void Remove(WatchableList<T> list)
		{
			if(!_lists.Contains(list)) { return; }
			_lists.Remove(list);
			list.RemoveListener(Evaluate);
			Evaluate();
		}

		public void Clear()
		{
			foreach(WatchableList<T> list in _lists)
			{
				list.RemoveListener(Evaluate);
			}
			_lists.Clear();
			Evaluate();
		}

		public override void AddListener(Action listener)
		{
			if(_listeners.Contains(listener)) { return; }
			_listeners.Add(listener);
			if(_listeners.Count == 1)
			{
				Evaluate();
				foreach(WatchableList<T> list in _lists)
				{
					list.AddListener(Evaluate);
				}
			}
			Changed -= listener;
			Changed += listener;
		}

		public override void RemoveListener(Action listener)
		{
			if(!_listeners.Contains(listener)) { return; }
			_listeners.Remove(listener);
			Changed -= listener;
			if(_listeners.Count == 0)
			{
				foreach(WatchableList<T> list in _lists)
				{
					list.RemoveListener(Evaluate);
				}
			}
		}

		public override List<T> ToValue()
		{
			if(_listeners.Count == 0)
			{
				Evaluate();
			}
			return new List<T>(_value);
		}

		public override IEnumerator<T> GetEnumerator()
		{
			if(_listeners.Count == 0)
			{
				Evaluate();
			}
			return _value.GetEnumerator();
		}


		public override ReadOnlyCollection<T> AsReadOnly()
		{
			if(_listeners.Count == 0)
			{
				Evaluate();
			}
			return _value.AsReadOnly();
		}

	}
}