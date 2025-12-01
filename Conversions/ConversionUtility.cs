using System;


namespace Watchables
{
  public static class ConversionUtility
  {

    #region Wrapper Conversion

    public static ConversionWrapper<TSource, TValue> GetConversion<TSource, TValue>(this IValueWrapper<TSource> source)
      where TSource : unmanaged
      where TValue : unmanaged
    {
      if(!CheckValidType(typeof(TSource)) || !CheckValidType(typeof(TValue)))
      {
        throw new ArgumentException($"Cannot create conversion between Types {typeof(TSource).Name} and {typeof(TValue).Name}. Only numerics are allowed.");
      }
      return new ConversionWrapper<TSource, TValue>(source);
    }

    public static ConversionWrapper<short> AsShort<TSource>(this IValueWrapper<TSource> source) where TSource : unmanaged
    {
      return GetConversion<TSource, short>(source);
    }

    public static ConversionWrapper<int> AsInt<TSource>(this IValueWrapper<TSource> source) where TSource : unmanaged
    {
      return GetConversion<TSource, int>(source);
    }

    public static ConversionWrapper<long> AsLong<TSource>(this IValueWrapper<TSource> source) where TSource : unmanaged
    {
      return GetConversion<TSource, long>(source);
    }

    public static ConversionWrapper<ushort> AsUShort<TSource>(this IValueWrapper<TSource> source) where TSource : unmanaged
    {
      return GetConversion<TSource, ushort>(source);
    }

    public static ConversionWrapper<uint> AsUInt<TSource>(this IValueWrapper<TSource> source) where TSource : unmanaged
    {
      return GetConversion<TSource, uint>(source);
    }

    public static ConversionWrapper<ulong> AsULong<TSource>(this IValueWrapper<TSource> source) where TSource : unmanaged
    {
      return GetConversion<TSource, ulong>(source);
    }

    public static ConversionWrapper<decimal> AsDecimal<TSource>(this IValueWrapper<TSource> source) where TSource : unmanaged
    {
      return GetConversion<TSource, decimal>(source);
    }

    public static ConversionWrapper<double> AsDouble<TSource>(this IValueWrapper<TSource> source) where TSource : unmanaged
    {
      return GetConversion<TSource, double>(source);
    }

    public static ConversionWrapper<float> AsFloat<TSource>(this IValueWrapper<TSource> source) where TSource : unmanaged
    {
      return GetConversion<TSource, float>(source);
    }

    #endregion

    #region Watchable Conversion

    public static ConversionWatchable<TSource, TValue> GetConversion<TSource, TValue>(this IWatchable<TSource> source)
      where TSource : unmanaged
      where TValue : unmanaged
    {
      if(!CheckValidType(typeof(TSource)) || !CheckValidType(typeof(TValue)))
      {
        throw new ArgumentException($"Cannot create conversion between Types {typeof(TSource).Name} and {typeof(TValue).Name}. Only numerics are allowed.");
      }
      return new ConversionWatchable<TSource, TValue>(source);
    }

    public static ConversionWatchable<short> AsShort<TSource>(this IWatchable<TSource> source) where TSource : unmanaged
    {
      return GetConversion<TSource, short>(source);
    }

    public static ConversionWatchable<int> AsInt<TSource>(this IWatchable<TSource> source) where TSource : unmanaged
    {
      return GetConversion<TSource, int>(source);
    }

    public static ConversionWatchable<long> AsLong<TSource>(this IWatchable<TSource> source) where TSource : unmanaged
    {
      return GetConversion<TSource, long>(source);
    }

    public static ConversionWatchable<ushort> AsUShort<TSource>(this IWatchable<TSource> source) where TSource : unmanaged
    {
      return GetConversion<TSource, ushort>(source);
    }

    public static ConversionWatchable<uint> AsUInt<TSource>(this IWatchable<TSource> source) where TSource : unmanaged
    {
      return GetConversion<TSource, uint>(source);
    }

    public static ConversionWatchable<ulong> AsULong<TSource>(this IWatchable<TSource> source) where TSource : unmanaged
    {
      return GetConversion<TSource, ulong>(source);
    }

    public static ConversionWatchable<decimal> AsDecimal<TSource>(this IWatchable<TSource> source) where TSource : unmanaged
    {
      return GetConversion<TSource, decimal>(source);
    }

    public static ConversionWatchable<double> AsDouble<TSource>(this IWatchable<TSource> source) where TSource : unmanaged
    {
      return GetConversion<TSource, double>(source);
    }

    public static ConversionWatchable<float> AsFloat<TSource>(this IWatchable<TSource> source) where TSource : unmanaged
    {
      return GetConversion<TSource, float>(source);
    }

    #endregion

    private static bool CheckValidType(Type type)
    {
      return Type.GetTypeCode(type) switch
      {
        TypeCode.Decimal or
        TypeCode.Double or
        TypeCode.Int16 or
        TypeCode.Int32 or
        TypeCode.Int64 or
        TypeCode.Single or
        TypeCode.UInt16 or
        TypeCode.UInt32 or
        TypeCode.UInt64 => true,
        _ => false
      };
    }

  }
}