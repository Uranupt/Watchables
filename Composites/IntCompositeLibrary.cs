

namespace Watchables
{
  public sealed class IntCompositeLibrary : CompositeLibrary<int>
  {

    public IntCompositeLibrary() : base()
    {

    }

    public IntCompositeLibrary(object owner) : base(owner)
    {

    }

    protected override CompositeWatchable<int> CreateNew() => new IntComposite();

  }
}