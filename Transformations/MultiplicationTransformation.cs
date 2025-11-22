using System;


namespace Watchables.Transformations
{
	public class MultiplicationTransformation : NestedWatchable<float>
	{

		private readonly Watchable<float> _value1;
		private readonly Watchable<float> _value2;
		private readonly float _value1Offset;
		private readonly float _value2Offset;

		public MultiplicationTransformation(Watchable<float> value1, Watchable<float> value2, 
			float value1Offset = 0f, float value2Offset = 0f)
		{
			_value1 = value1;
			_value2 = value2;
			_value1Offset = value1Offset;
			_value2Offset = value2Offset;
		}

		public MultiplicationTransformation(Watchable<float> value1, float value2, float value1Offset = 0f)
		{
			_value1 = value1;
			_value2 = new SimpleWatchable<float>(value2);
			_value1Offset = value1Offset;
			_value2Offset = 0f;
		}

		protected override void Evaluate()
		{
			_value = (_value1 + _value1Offset) * (_value2 + _value2Offset);
		}

		public override void AddListener(Action listener)
		{
			_value1.AddListener(listener);
			_value2.AddListener(listener);
		}

		public override void RemoveListener(Action listener)
		{
			_value1.RemoveListener(listener);
			_value2.RemoveListener(listener);
		}
	}
}