using System;


namespace Watchables
{
  public static class Composites
  {

    public static CompositeWatchable<ushort> NewUShort() => new CompositeWatchable<ushort>();
    public static CompositeWatchable<ushort> NewUShort(IValueWrapper<ushort> value, StepPriority priority = StepPriority.None)
      => NewWithBase(value, priority);
    public static CompositeWatchable<ushort> New(IValueWrapper<ushort> value, StepPriority priority = StepPriority.None)
      => NewWithBase(value, priority);

    public static CompositeStep<ushort> NewStep(CompositeOperation operation, IValueWrapper<ushort> value, StepPriority priority = StepPriority.None)
      => new CompositeStep<ushort>(operation, value, priority);

    public static CompositeLibrary<ushort> UShortLibrary() => new CompositeLibrary<ushort>();


    private static CompositeStep<T> NewStep<T>(CompositeOperation operation, IValueWrapper<T> value,
      StepPriority priority = StepPriority.None, object owner = null) where T : unmanaged
    {
      throw new ArgumentException($"Invalid CompositeStep type: {typeof(T).Name}. Only numerics are allowed.");
    }

    private static CompositeWatchable<T> NewWithBase<T>(IValueWrapper<T> value, StepPriority priority) where T : unmanaged
    {
      CompositeWatchable<T> resl = new();
      resl.AddStep(NewStep(CompositeOperation.SetBase, value));
      return resl;
    }

  }
}
