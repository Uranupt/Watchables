using System;
using System.Collections.Generic;


namespace Watchables.Stats
{
	public class FloatStat : Watchable<float>
	{

		private ReadOnlyWatchable<float> _readonlyWrapper;
		private bool _preventRecalc;
		private event Action Changed;
		private readonly List<StatEffect> _effects = new();

		public override Watchable<float> ReadOnlyWrapper
		{
			get
			{
				_readonlyWrapper ??= new ReadOnlyWatchable<float>(this);
				return _readonlyWrapper;
			}
		}

    private static void ComputeEffect(StatEffect effect, float curValue, out float value)
    {
      switch(effect.Operation)
      {
        case OperationType.SetBase:
        case OperationType.SetFinal:
        case OperationType.Force:
        {
          value = effect;
          break;
        }
        case OperationType.Add:
        {
          value = curValue + effect;
          break;
        }
        case OperationType.Subtract:
        {
          value = curValue - effect;
          break;
        }
        case OperationType.Multiply:
        {
          value = curValue * effect;
          break;
        }
        case OperationType.Divide:
        {
          value = curValue / effect;
          break;
        }
        case OperationType.Minimum:
        {
          value = curValue < effect ? effect : curValue;
          break;
        }
        case OperationType.Maximum:
        {
          value = curValue > effect ? effect : curValue;
          break;
        }
        default:
        {
          value = curValue;
          return;
        }
      }
    }

    public static float Calculate(List<StatEffect> effects)
    {
      float resl = 0;
      foreach(StatEffect effect in effects)
      {
        ComputeEffect(effect, resl, out resl);
      }
      return resl;
    }

    private void Evaluate()
    {
      if(_preventRecalc) { return; }
      _value = 0;
      if(_effects.Count > 0)
      {
        _effects.Sort(StatEffect.Comparer);
        StatEffect lastEffect = _effects[^1];
        //if the last priority Effect is a setting operation, any calculation can be skipped
        if(lastEffect.Operation == OperationType.SetFinal
          || lastEffect.Operation == OperationType.Force
          || lastEffect.Operation == OperationType.SetBase)
        {
          _value = lastEffect;
        }
        else
        {
          foreach(StatEffect effect in _effects)
          {
            ComputeEffect(effect, _value, out _value);
          }
        }
      }
      Changed?.Invoke();
    }

    public void AddEffect(StatEffect effect)
		{
			if(_effects.Contains(effect)) { return; }
			_effects.Add(effect);
			effect.Removed += RemoveEffect;
			effect.AddListener(Evaluate);
			Evaluate();
		}

    public void AddEffects(List<StatEffect> effects)
    {
      foreach(StatEffect effect in effects)
      {
        if(_effects.Contains(effect)) { continue; }
        _effects.Add(effect);
        effect.Removed += RemoveEffect;
				effect.AddListener(Evaluate);
      }
			Evaluate();
    }

		public void RemoveEffect(StatEffect effect)
		{
			effect.Removed -= RemoveEffect;
			_effects.Remove(effect);
			effect.RemoveListener(Evaluate);
			Evaluate();
		}

    public void RemoveEffects(List<StatEffect> effects)
    {
      foreach(StatEffect effect in effects)
      {
        if(!_effects.Contains(effect)) { continue; }
        effect.Removed -= RemoveEffect;
        _effects.Remove(effect);
        effect.RemoveListener(Evaluate);
      }
			Evaluate();
    }

		public void Clear()
		{
			_preventRecalc = true;
			while(_effects.Count > 0)
			{
				_effects[0].Remove();
			}
			_preventRecalc = false;
			Evaluate();
		}

		public void ReplaceEffects(List<StatEffect> effects)
		{
			_preventRecalc = true;
			while(_effects.Count > 0)
			{
				_effects[0].Remove();
			}
			_effects.AddRange(effects);
			_preventRecalc = false;
			Evaluate();
		}

		public override void AddListener(Action listener)
		{
			if(listener == Evaluate)
			{
				string msg = "A Watchable update loop has been found for a FloatStat.";
				throw new OverflowException(msg);
			}
			Changed -= listener;
      Changed += listener;
		}

		public override void RemoveListener(Action listener)
		{
      Changed -= listener;
		}
	}
}