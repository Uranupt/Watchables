

namespace Watchables
{
  /// <summary>
  /// Analyzer marker interface for generics that should only accept unmanaged numeric types.
  /// </summary>
  public interface IEnforceNumeric<T> where T : unmanaged
  {

  }
}