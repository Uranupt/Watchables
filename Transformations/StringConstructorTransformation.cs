using System;


namespace Watchables.Transformations
{
	public class StringConstructorTransformation : NestedWatchable<string>
	{

		private readonly string _content;
		private readonly Watchable<float>[] _refValues;
		private readonly string[] _inserts;

		public StringConstructorTransformation(string content, params Watchable<float>[] refValues)
		{
			_content = content;
			_refValues = refValues;
			_inserts = new string[refValues.Length];
		}

		protected override void Evaluate()
		{
			for(int i = 0; i < _refValues.Length; i++)
			{
				_inserts[i] = _refValues[i].ToString();
			}
			_value = string.Format(_content, _inserts);
		}

		public override void AddListener(Action listener)
		{
			foreach(Watchable<float> refValue in _refValues)
			{
				refValue.AddListener(listener);
			}
		}

		public override void RemoveListener(Action listener)
		{
			foreach(Watchable<float> refValue in _refValues)
			{
				refValue.RemoveListener(listener);
			}
		}
	}
}