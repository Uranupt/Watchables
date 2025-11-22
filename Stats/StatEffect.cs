using System;


namespace Watchables.Stats
{
	public class StatEffect : Watchable<float>
	{

    private readonly Watchable<float> _refValue;
    private ReadOnlyWatchable<float> _readOnlyWrapper;

    public OperationType Operation { get; private set; }
		public event Action<StatEffect> Removed;
		public override Watchable<float> ReadOnlyWrapper
		{
			get
			{
				_readOnlyWrapper ??= new ReadOnlyWatchable<float>(this);
				return _readOnlyWrapper;
			}
		}

    public static int Comparer(StatEffect x, StatEffect y)
    {
      if(x == null || y == null) return 0;
      return (int)x.Operation - (int)y.Operation;
    }

    public StatEffect(float value, OperationType operation)
		{
			_refValue = new SimpleWatchable<float>(value);
			Operation = operation;
		}

		public StatEffect(Watchable<float> refValue, OperationType operation)
		{
			_refValue = refValue;
			Operation = operation;
		}

		public override float ToValue()
		{
			return _refValue;
		}

		public void Remove()
		{
			Removed?.Invoke(this);
		}

		public override void AddListener(Action listener)
		{
			_refValue?.AddListener(listener);
		}

		public override void RemoveListener(Action listener)
		{
			_refValue?.RemoveListener(listener);
		}
	}
}