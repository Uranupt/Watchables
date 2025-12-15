

namespace Watchables
{
  /// <summary>
  /// Analyzer marker interface for generics that should only accept handled numeric types.
  /// </summary>
  /// <remarks>
  /// Allows: <see langword="uint"/>, <see langword="ulong"/>, <see langword="int"/>, <see langword="long"/>,
  /// <see langword="float"/>, <see langword="double"/>, <see langword="decimal"/>
  /// </remarks>
  public interface IEnforceNumeric<T> where T : unmanaged
  {

  }
}