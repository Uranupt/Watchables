

namespace Watchables
{
  /// <summary>
  /// Extension interface of <see cref="IOwnable"/> which further allows for explicit sealing of mutation alongside ownership.
  /// </summary>
  public interface ISealable : IOwnable
  {

    /// <summary> Whether this instance is currently sealed. </summary>
    bool IsSealed { get; }

    /// <summary> Attempts to set the sealed state of this instance. Requires the current owner if one exists. </summary>
    bool SetSealed(bool sealedState, object owner = null);

  }
}