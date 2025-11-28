

namespace Watchables
{
  public static class WatchablesUtility
  {

    public static ReadOnlyWrapper<T> Wrap<T>(this T value) => new ReadOnlyWrapper<T>(value);

    public static BasicWrapper<T> WrapMutable<T>(this T value, object owner = null)
    {
      BasicWrapper<T> resl = new(value);
      if(owner != null)
      {
        resl.SetOwner(owner);
      }
      return resl;
    }

  }
}