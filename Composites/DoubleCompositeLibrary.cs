

namespace Watchables
{
  public sealed class DoubleCompositeLibrary : CompositeLibrary<double>
  {

    public DoubleCompositeLibrary() : base()
    {

    }

    public DoubleCompositeLibrary(object owner) : base(owner)
    {

    }

    protected override CompositeWatchable<double> CreateNew() => new DoubleComposite();

  }
}