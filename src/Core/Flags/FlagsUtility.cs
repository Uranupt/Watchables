using System;
using System.Collections.Generic;


namespace Watchables
{
  internal static class FlagsUtility
  {

    internal static T With<T>(this T value, T toWith) where T : struct, Enum => ToEnum<T>(value.ToULong() |  toWith.ToULong());

    internal static T With<T>(this T value, IEnumerable<T> toWith) where T : struct, Enum
    {
      ulong bits = value.ToULong();
      foreach(T item in toWith)
      {
        bits |= item.ToULong();
      }
      return bits.ToEnum<T>();
    }

    internal static T Without<T>(this T value, T toWithout) where T : struct, Enum => ToEnum<T>(value.ToULong() & ~toWithout.ToULong());

    internal static T Without<T>(this T value, IEnumerable<T> toWithout) where T : struct, Enum
    {
      ulong bits = value.ToULong();
      foreach(T item in toWithout)
      {
        bits &= ~item.ToULong();
      }
      return bits.ToEnum<T>();
    }

    internal static ulong ToULong<T>(this T value) where T : struct, Enum => Convert.ToUInt64(value);
    internal static T ToEnum<T>(this ulong value) where T : struct, Enum => (T)Enum.ToObject(typeof(T), value);

  }
}
