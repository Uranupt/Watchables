using System.Collections.Generic;
using System.Collections;
using System;
using System.Collections.ObjectModel;

namespace Watchables
{
	public abstract class WatchableList<T> : Watchable<List<T>>, IEnumerable<T>
	{

		private ReadOnlyWatchable<List<T>> _readOnly;

		public override Watchable<List<T>> ReadOnlyWrapper
		{
			get
			{
				_readOnly ??= new ReadOnlyWatchable<List<T>>(this);
				return _readOnly;
			}
		}

		public T this[int index]
		{
			get
			{
				return _value[index];
			}
		}

		public virtual int Count => _value.Count;

		public void Sort(Comparison<T> comparison)
		{
			_value.Sort(comparison);
		}

		public void Sort(IComparer<T> comparer)
		{
			_value.Sort(comparer);
		}

		public bool Contains(T value)
		{
			return _value.Contains(value);
		}

		public override List<T> ToValue()
		{
			return new List<T>(_value);
		}

		public virtual IEnumerator<T> GetEnumerator()
		{
			// if(_value is null)
			// {
			//   throw new InvalidOperationException("List value failed to be set");
			// }
			return _value.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _value.GetEnumerator();
		}

		public virtual ReadOnlyCollection<T> AsReadOnly()
		{
			return _value.AsReadOnly();
		}

	}
}