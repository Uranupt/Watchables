using System.Collections.Generic;
using System;


namespace Watchables
{
	public class SimpleWatchableList<T> : WatchableList<T>
	{

		private event Action Changed;

		public SimpleWatchableList()
		{
			_value = new List<T>();
		}

		public SimpleWatchableList(IEnumerable<T> values)
		{
			_value = new List<T>(values);
		}

		public void Add(T value)
		{
			_value.Add(value);
			Changed?.Invoke();
		}

		public void AddRange(IEnumerable<T> values)
		{
			_value.AddRange(values);
			Changed?.Invoke();
		}

		public void Remove(T value)
		{
			_value.Remove(value);
			Changed?.Invoke();
		}

		public void RemoveRange(IEnumerable<T> values)
		{
			foreach(T value in values)
			{
				_value.Remove(value);
			}
			Changed?.Invoke();
		}

		public void Clear()
		{
			_value.Clear();
			Changed?.Invoke();
		}

		public void Replace(IEnumerable<T> values)
		{
			_value.Clear();
			_value.AddRange(values);
			Changed?.Invoke();
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