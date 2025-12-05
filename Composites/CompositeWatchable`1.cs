using System.Collections.Generic;


namespace Watchables
{
  public sealed class CompositeWatchable<T> : CompositeBase<T, CompositePart<T>>, IEnforceNumeric<T> where T : unmanaged
  {

    public CompositeWatchable()
    {
      NumericUtility.ValidateType(typeof(T));
    }

    protected override void Evaluate()
    {
      for(int i = 0; i < _parts.Count; i++)
      {
        CompositePart<T> part = _parts[i];
        switch(part.Operation)
        {
          case CompositeOperation.Force:
          {
            _value = part;
            return;
          }
          case CompositeOperation.SetFinal:
          {
            _value = part;
            while(i < _parts.Count && _parts[i].Operation < CompositeOperation.Minimum)
            {
              i++;
            }
            break;
          }
          case CompositeOperation.SetBase:
          {
            _value = part;
            while(i < _parts.Count && _parts[i].Operation <= CompositeOperation.SetBase)
            {
              i++;
            }
            break;
          }
          case CompositeOperation.Add:
          case CompositeOperation.Subtract:
          case CompositeOperation.Multiply:
          case CompositeOperation.Divide:
          case CompositeOperation.Minimum:
          case CompositeOperation.Maximum:
          {
            _value = _value.Operate(part.Operation.ToNumeric(), part.ToValue(), true);
            break;
          }
        }
      }
    }

    protected override void Sort()
    {
      _parts.Sort(
        (x, y) =>
        {
          int resl = x.Operation.CompareTo(y.Operation);
          return resl == 0 ? y.Priority.CompareTo(x.Priority) : resl;
        }
      );
    }

  }
}