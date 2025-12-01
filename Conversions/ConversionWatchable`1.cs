using System;


namespace Watchables
{ 
  public abstract class ConversionWatchable<T> : NestedWatchable<T>
    where T : unmanaged
  {

    public abstract Type SourceType { get; }

    protected override bool CheckFatalDestruction(IWatchable dependency) => true;

  }
}