using System;


namespace Watchables.Transformations
{
	public class PercentageTransformation : NestedWatchable<float>
	{

		private readonly bool _inverse;
		private readonly bool _clamp;
		private readonly Watchable<float> _maximum;
		private readonly Watchable<float> _current;

		public PercentageTransformation(Watchable<float> maximum, Watchable<float> current, bool inverse = false, bool clamp = false)
		{
			_maximum = maximum;
			_current = current;
			_inverse = inverse;
			_clamp = clamp;
		}

		protected override void Evaluate()
		{
			if(_inverse)
			{
				_value = (_maximum - _current) / _maximum;
			}
			else
			{
				_value = _current / _maximum;
			}
			if(_clamp)
			{
				if(_value < 0)
				{
					_value = 0;
				}
				else if(_value > 1)
				{
					_value = 1;
				}
			}
		}

		public override void AddListener(Action listener)
		{
			_maximum.AddListener(listener);
			_current.AddListener(listener);
		}

		public override void RemoveListener(Action listener)
		{
			_maximum.RemoveListener(listener);
			_current.RemoveListener(listener);
		}
	}
}