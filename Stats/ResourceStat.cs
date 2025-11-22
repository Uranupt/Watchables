using System;
using System.Collections.Generic;
using Watchables.Transformations;

namespace Watchables.Stats
{
	public class ResourceStat : Watchable<float>
	{

		private ReadOnlyWatchable<float> _readonlyWrapper;
		private readonly FloatStat _maximum;
		private event Action Changed;

		public Watchable<float> Maximum => _maximum.ReadOnlyWrapper;
		public Watchable<float> Ratio { get; private set; }

		public override Watchable<float> ReadOnlyWrapper
		{
			get
			{
				_readonlyWrapper ??= new ReadOnlyWatchable<float>(this);
				return _readonlyWrapper;
			}
		}

		public ResourceStat()
		{
			_maximum = new FloatStat();
			Ratio = new PercentageTransformation(_maximum, this);
			_maximum.AddListener(Evaluate);
		}

		public void AddEffect(StatEffect effect)
		{
			_maximum.AddEffect(effect);
		}

		public void AddEffects(List<StatEffect> effects)
		{
			_maximum.AddEffects(effects);
		}

		public void RemoveEffect(StatEffect effect)
		{
			_maximum.RemoveEffect(effect);
		}

		public void Add(float value)
		{
			_value += value;
			Evaluate();
		}

		public void Remove(float value)
		{
			_value -= value;
			Evaluate();
		}

		public void Fill()
		{
			_value = (int)Maximum;
      Changed?.Invoke();
    }

		public void Empty()
		{
			_value = 0f;
      Changed?.Invoke();
    }

		private void Evaluate()
		{
			if(_value < 0f)
			{
				_value = 0f;
			}
			else if(_value > _maximum)
			{
				_value = _maximum;
			}

			_value = (int)_value;
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