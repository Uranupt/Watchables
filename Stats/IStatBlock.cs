

namespace Watchables.Stats
{
  public interface IStatBlock
  {

    public Watchable<float> this[string name] { get; }

    public bool Contains(string name);

  }
}