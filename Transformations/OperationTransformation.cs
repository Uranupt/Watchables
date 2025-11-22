using System;
using UnityEngine;


namespace Watchables.Transformations
{
  public sealed class OperationTransformation : NestedWatchable<float>
  {

    private readonly Watchable<float> _base;
    private readonly Watchable<float> _operand;
    private readonly Operation _operation;
    private readonly int _roundingPlaces;

    public OperationTransformation(Watchable<float> baseValue, Watchable<float> operand, Operation operation, int roundingPlaces = -1)
    {
      _base = baseValue;
      _operand = operand;
      _operation = operation;
      _roundingPlaces = roundingPlaces;
    }

    protected override void Evaluate()
    {
      switch(_operation)
      {
        case Operation.Add:
        {
          _value = _base + _operand;
          break;
        }
        case Operation.Subtract:
        {
          _value = _base - _operand;
          break;
        }
        case Operation.Multiply:
        {
          _value = _base * _operand;
          break;
        }
        case Operation.Divide:
        {
          _value = _operand != 0 ? _base / _operand : 0;
          break;
        }
        case Operation.Exponent:
        {
          _value = Mathf.Pow(_base, _operand);
          break;
        }
      }
      if(_roundingPlaces >= 0)
      {
        float power = Mathf.Pow(10, _value);
        _value = Mathf.Round(_value * power) / power;
      }
    }

    public override void AddListener(Action listener)
    {
      _base.AddListener(listener);
      _operand.AddListener(listener);
    }

    public override void RemoveListener(Action listener)
    {
      _base.RemoveListener(listener);
      _operand.RemoveListener(listener);
    }

  }
}