

namespace Watchables
{
  /// <summary>
  /// Interface for checking and controlling instance ownership.
  /// </summary>
  public interface IOwnable
  {

    /// <summary>  Whether this instance currently has a defined owner. </summary>
    bool IsOwned { get; }

    /// <summary> Attempts to set this instance's owner. </summary>
    bool SetOwner(object owner);

    /// <summary> Attempts to clear this instance's owner. </summary>
    /// <param name="owner"> The current owner. </param>
    bool ClearOwner(object owner);

    /// <summary> Compares the given object to this instance's owner. Always returns false if there is no owner. </summary>
    bool CompareToOwner(object owner);

  }
}