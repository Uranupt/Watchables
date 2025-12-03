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

    public static CompositeLibrary<ushort> NewUShortLibrary() => new CompositeLibrary<ushort>();


    internal static CompositeWatchable<T> New<T>() where T : unmanaged
    {
      return (Type.GetTypeCode(typeof(T))) switch
      {
        TypeCode.Decimal or
        TypeCode.Double or
        TypeCode.Int16 or
        TypeCode.Int32 or
        TypeCode.Int64 or
        TypeCode.Single or
        TypeCode.UInt16 => NewUShort()
        TypeCode.UInt32 or
        TypeCode.UInt64 => true,
        _ => throw new ArgumentException($"Invalid Composite type: {typeof(T).Name}. Only numerics are allowed.")
      };
    }

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
