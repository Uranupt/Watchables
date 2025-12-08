

namespace Watchables
{
  /// <summary>
  /// Interface for checking and controlling instance ownership.
  /// </summary>
  public interface IOwnable
  {

    /// <summary>  Whether this instance currently has a defined owner. </summary>
    bool IsOwned { get; }

    /// <summary> Attempts to set this instance's <paramref name="owner"/>, provided it does not already have one. </summary>
    /// <returns> Whether the operation was allowed. </returns>
    bool SetOwner(object owner);

    /// <summary> Attempts to clear this instance's <paramref name="owner"/>. </summary>
    /// <param name="owner"> The current owner. </param>
    /// <returns> Whether the operation was allowed. </returns>
    bool ClearOwner(object owner);

    /// <summary> Compares the given object to this instance's owner. </summary>
    /// <returns> <see langword="true"/> if the instance is owned and the <paramref name="owner"/> matches; otherwise <see langword="false"/>. </returns>
    bool CompareToOwner(object owner);

  }
}