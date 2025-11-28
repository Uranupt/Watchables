using System;


namespace Watchables.Transformations
{
	public class RatioScaleTransformation : NestedWatchable<float>
	{

		private readonly Watchable<float> _refValue;
		private readonly Watchable<float> _ratio;

		public RatioScaleTransformation(Watchable<float> refValue, Watchable<float> maximum, Watchable<float> current,
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