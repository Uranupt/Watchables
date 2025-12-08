using System.Collections.Generic;


namespace Watchables
{
  /// <summary>
  /// Numeric implementation of <see cref="CompositeBase{TValue, TPart}"/>. Will always evaluate operations by performing translation
  /// first, then scaling, and finally clamping. Setting operations will be sorted by <see cref="CompositePartPriority"/>, with the first encountered
  /// at the highest present priority being chosen.
  /// </summary>
  public sealed class CompositeWatchable<T> : CompositeBase<T, CompositePart<T>>, IEnforceNumeric<T> where T : unmanaged
  {

    public CompositeWatchable()
    {
      NumericUtility.ValidateType(typeof(T));
    }

    /// <inheritdoc/>
    protected override void Evaluate()
    {
      for(int i = 0; i < _parts.Count; i++)
      {
        CompositePart<T> part = _parts[i];
        switch(part.Operation)
        {
          case CompositeOperation.Force:
          {
            Value = part;
            return;
          }
          case CompositeOperation.SetFinal:
          {
            Value = part;
            while(i < _parts.Count && _parts[i].Operation < CompositeOperation.Minimum)
            {
              i++;
            }
            break;
          }
          case CompositeOperation.SetBase:
          {
            Value = part;
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
            Value = Value.Operate(part.Operation.ToNumeric(), part.Value, true);
            break;
          }
        }
      }
    }

    /// <inheritdoc/>
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