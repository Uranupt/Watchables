

namespace Watchables
{
  public sealed class FloatCompositeLibrary : CompositeLibrary<float>
  {

    public FloatCompositeLibrary() : base()
    {

    }

    public FloatCompositeLibrary(object owner) : base(owner)
    {

    }

    protected override CompositeWatchable<float> CreateNew() => new FloatComposite();

  }
}