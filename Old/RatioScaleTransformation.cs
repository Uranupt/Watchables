using System;


namespace Watchables.Transformations
{
	public class RatioScaleTransformation : NestedWatchable<float>
	{

		private readonly WatchableBase<float> _refValue;
		private readonly WatchableBase<float> _ratio;

		public RatioScaleTransformation(WatchableBase<float> refValue, WatchableBase<float> maximum, WatchableBase<float> current,
			bool inverse = false, bool clamp = false)
		{
			_refValue = refValue;
			_ratio = new PercentageTransformation(maximum, current, inverse, clamp);
		}

		protected override void Evaluate()
		{
			_value = _refValue * _ratio;
		}

		public override void AddListener(Action listener)
		{
			_refValue.AddListener(listener);
			_ratio.AddListener(listener);
		}

		public override void RemoveListener(Action listener)
		{
			_refValue.RemoveListener(listener);
			_ratio.RemoveListener(listener);
		}
	}
}