using System;


namespace Watchables.Transformations
{
	public class IntegerTransformation : NestedWatchable<float>
	{

		private readonly Watchable<float> _refValue;
		private readonly bool _truncate;

		public IntegerTransformation(Watchable<float> refValue, bool truncate = false)
		{
			_refValue = refValue;
			_truncate = truncate;
		}

		protected override void Evaluate()
		{
			_value = _truncate ? (int)(_refValue) : (float)Math.Round(_refValue.ToValue(), MidpointRounding.AwayFromZero);
		}

		public override void AddListener(Action listener)
		{
			_refValue.AddListener(listener);
		}

		public override void RemoveListener(Action listener)
		{
			_refValue.RemoveListener(listener);
		}
	}
}