using System;


namespace Watchables
{
  public sealed class FloatOpWatchable : NestedWatchable<float>
  {

    private readonly IValueWrapper<float> _base;
    private readonly IValueWrapper<float> _operand;
    private readonly Operation _operation;

    public FloatOpWatchable(IValueWrapper<float> baseValue, IValueWrapper<float> operand, Operation operation) : base()
    {
      _base = baseValue;
      _operand = operand;
      _operation = operation;
      if(_base is IWatchable)
      {
        Register(_base as IWatchable);
      }
      if(_operand is IWatchable)
      {
        Register(_operand as IWatchable);
      }
    }

    protected override bool CheckFatalDestruction(IWatchable dependency)
    {
      return (_base is IWatchable && dependency == (_base as IWatchable)) 
        || (_operand is IWatchable && dependency == (_operand as IWatchable));
    }

    protected override void Evaluate()
    {
      _value = _operation switch
      {
        Operation.Add => _base.ToValue() + _operand.ToValue(),
        Operation.Subtract => _base.ToValue() - _operand.ToValue(),
        Operation.Multiply => _base.ToValue() * _operand.ToValue(),
        Operation.Divide => _operand.ToValue() != 0 ? _base.ToValue() / _operand.ToValue() : 0,
        Operation.Exponent => (float)Math.Pow(_base.ToValue(), _operand.ToValue()),
        _ => _value
      };
    }

    protected override void OnDestroy(IWatchable self)
    {
      if(_base is IWatchable)
      {
        Unregister(_base as IWatchable);
      }
      if(_operand is IWatchable)
      {
        Unregister(_operand as IWatchable);
      }
    }

  }
}