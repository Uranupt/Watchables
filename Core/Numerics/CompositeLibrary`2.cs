using System.Collections.Generic;


namespace Watchables
{
  /// <summary>
  /// Extension of <see cref="CompositeLibrary{TKey, TComp, TPart}"/> for use with the numeric <see cref="CompositeWatchable{T}"/>. Provides no
  /// additional functionality, serving as a shorthand for a common usage of the base class.
  /// </summary>
  public sealed class CompositeLibrary<TKey, TValue> 
    : CompositeLibrary<TKey, CompositeWatchable<TValue>, CompositePart<TValue>>, IEnforceNumeric<TValue> 
    where TValue : unmanaged
  {

    public CompositeLibrary(object owner = null) : base(owner)
    {
      NumericUtility.ValidateType(typeof(TValue));
    }

  }
}