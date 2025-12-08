

namespace Watchables
{
  /// <summary>
  /// A mutable and sealable implementation of <see cref="IWrapper{T}"/>. 
  /// Useful for stable value references without event and destruction semantics.
  /// </summary>
  public sealed class BasicWrapper<T> : SealableBase, IWrapper<T>
  {

    /// <inheritdoc/>
    public T Value { get; private set; }

    public BasicWrapper()
    {

    }

    public BasicWrapper(T value)
    {
      Value = value;
    }

    public static implicit operator T(BasicWrapper<T> wrapper) => wrapper.Value;

    /// <summary> Attempts to set the <paramref name="value"/>. </summary>
    /// <param name="owner"> The instance's current owner, used to bypass sealed state. </param>
    /// <returns> Whether the operation was allowed. </returns>
    public bool SetValue(T value, object owner = null)
    {
      if(IsSealed && !CompareToOwner(owner)) { return false; }
      Value = value;
      return true;
    }

  }
}