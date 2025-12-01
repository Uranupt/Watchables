using System;


namespace Watchables.Stats
{
	public class FlagEffect<T> where T : Enum
	{

		private readonly WatchableBase<T> _baseValue;
		public string[] Names { get; private set; }
		public FlagOperation Operation { get; private set; }
		public event Action Updated;
		public event Action<FlagEffect<T>> Removed;

		public FlagEffect(WatchableBase<T> baseValue, FlagOperation operation)
		{
			_baseValue = baseValue;
			Operation = operation;
			_baseValue.AddListener(Update);
			Names = _baseValue.ToString().Split(", ");
		}

		public FlagEffect(T value, FlagOperation operation)
		{
			Operation = operation;
			Names = value.ToString().Split(", ");
		}

		public static int Comparer(FlagEffect<T> x, FlagEffect<T> y)
		{
			if(x == null || y == null) return 0;
			return x.Operation - y.Operation;
		}

		public void Update()
		{
			Names = _baseValue.ToString().Split(", ");
			Updated?.Invoke();
		}

		public void Remove()
		{
			_baseValue?.RemoveListener(Update);
			Removed?.Invoke(this);
		}

	}
}