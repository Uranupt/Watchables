using System;


namespace Watchables
{
  public abstract class ConversionWrapper<T> : IValueWrapper<T>
    where T : unmanaged
  {

    public abstract Type SourceType { get; }

    public abstract T ToValue();

  }
}