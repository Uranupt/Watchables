using System;


namespace Watchables
{ 
  /// <summary>
  /// Helper class for generic math overloads
  /// </summary>
  public static class NumericUtility
  {

    #region UShort

    /// <inheritdoc cref="Add{T}"/>
    public static ushort Add(this ushort value, ushort operand) => (ushort)(value + operand);
    /// <inheritdoc cref="Subtract{T}"/>
    public static ushort Subtract(this ushort value, ushort operand) => (ushort)(value - operand);
    /// <inheritdoc cref="Multiply{T}"/>
    public static ushort Multiply(this ushort value, ushort operand) => (ushort)(value * operand);
    /// <inheritdoc cref="Divide{T}"/>
    public static ushort Divide(this ushort value, ushort operand, bool guard = false) => (guard && operand == 0) ? (ushort)0 : (ushort)(value / operand);
    /// <inheritdoc cref="Modulo{T}"/>
    public static ushort Modulo(this ushort value, ushort operand, bool guard = false) => (guard && operand == 0) ? (ushort)0 : (ushort)(value % operand);
    /// <inheritdoc cref="Power{T}"/>
    public static ushort Power(this ushort value, ushort operand) => (ushort)UPow(value, operand);
    /// <inheritdoc cref="GreaterOf{T}"/>
    public static ushort GreaterOf(this ushort value, ushort operand) => value < operand ? operand : value;
    /// <inheritdoc cref="LesserOf{T}"/>
    public static ushort LesserOf(this ushort value, ushort operand) => value > operand ? operand : value;

    #endregion

    #region UInt

    /// <inheritdoc cref="Add{T}"/>
    public static uint Add(this uint value, uint operand) => value + operand;
    /// <inheritdoc cref="Subtract{T}"/>
    public static uint Subtract(this uint value, uint operand) => value - operand;
    /// <inheritdoc cref="Multiply{T}"/>
    public static uint Multiply(this uint value, uint operand) => value * operand;
    /// <inheritdoc cref="Divide{T}"/>
    public static uint Divide(this uint value, uint operand, bool guard = false) => (guard && operand == 0) ? 0 : value / operand;
    /// <inheritdoc cref="Modulo{T}"/>
    public static uint Modulo(this uint value, uint operand, bool guard = false) => (guard && operand == 0) ? 0 : value % operand;
    /// <inheritdoc cref="Power{T}"/>
    public static uint Power(this uint value, uint operand) => (uint)UPow(value, operand);
    /// <inheritdoc cref="GreaterOf{T}"/>
    public static uint GreaterOf(this uint value, uint operand) => value < operand ? operand : value;
    /// <inheritdoc cref="LesserOf{T}"/>
    public static uint LesserOf(this uint value, uint operand) => value > operand ? operand : value;

    #endregion

    #region ULong

    /// <inheritdoc cref="Add{T}"/>
    public static ulong Add(this ulong value, ulong operand) => value + operand;
    /// <inheritdoc cref="Subtract{T}"/>
    public static ulong Subtract(this ulong value, ulong operand) => value - operand;
    /// <inheritdoc cref="Multiply{T}"/>
    public static ulong Multiply(this ulong value, ulong operand) => value * operand;
    /// <inheritdoc cref="Divide{T}"/>
    public static ulong Divide(this ulong value, ulong operand, bool guard = false) => (guard && operand == 0) ? 0 : value / operand;
    /// <inheritdoc cref="Modulo{T}"/>
    public static ulong Modulo(this ulong value, ulong operand, bool guard = false) => (guard && operand == 0) ? 0 : value % operand;
    /// <inheritdoc cref="Power{T}"/>
    public static ulong Power(this ulong value, ulong operand) => UPow(value, operand);
    /// <inheritdoc cref="GreaterOf{T}"/>
    public static ulong GreaterOf(this ulong value, ulong operand) => value < operand ? operand : value;
    /// <inheritdoc cref="LesserOf{T}"/>
    public static ulong LesserOf(this ulong value, ulong operand) => value > operand ? operand : value;

    #endregion

    #region Short

    /// <inheritdoc cref="Add{T}"/>
    public static short Add(this short value, short operand) => (short)(value + operand);
    /// <inheritdoc cref="Subtract{T}"/>
    public static short Subtract(this short value, short operand) => (short)(value - operand);
    /// <inheritdoc cref="Multiply{T}"/>
    public static short Multiply(this short value, short operand) => (short)(value * operand);
    /// <inheritdoc cref="Divide{T}"/>
    public static short Divide(this short value, short operand, bool guard = false) => (guard && operand == 0) ? (short)0 : (short)(value / operand);
    /// <inheritdoc cref="Modulo{T}"/>
    public static short Modulo(this short value, short operand, bool guard = false) => (guard && operand == 0) ? (short)0 : (short)(value % operand);
    /// <inheritdoc cref="Power{T}"/>
    public static short Power(this short value, short operand) => (short)Pow(value, operand);
    /// <inheritdoc cref="GreaterOf{T}"/>
    public static short GreaterOf(this short value, short operand) => value < operand ? operand : value;
    /// <inheritdoc cref="LesserOf{T}"/>
    public static short LesserOf(this short value, short operand) => value > operand ? operand : value;
    /// <inheritdoc cref="Absolute{T}"/>
    public static short Absolute(this short value) => value < 0 ? (short)-value : value;
    /// <inheritdoc cref="AsNegative{T}"/>
    public static short AsNegative(this short value) => value > 0 ? (short)-value : value;
    /// <inheritdoc cref="FlipSign{T}"/>
    public static short FlipSign(this short value) => (short)-value;

    #endregion

    #region Int

    /// <inheritdoc cref="Add{T}"/>
    public static int Add(this int value, int operand) => value + operand;
    /// <inheritdoc cref="Subtract{T}"/>
    public static int Subtract(this int value, int operand) => value - operand;
    /// <inheritdoc cref="Multiply{T}"/>
    public static int Multiply(this int value, int operand) => value * operand;
    /// <inheritdoc cref="Divide{T}"/>
    public static int Divide(this int value, int operand, bool guard = false) => (guard && operand == 0) ? 0 : value / operand;
    /// <inheritdoc cref="Modulo{T}"/>
    public static int Modulo(this int value, int operand, bool guard = false) => (guard && operand == 0) ? 0 : value % operand;
    /// <inheritdoc cref="Power{T}"/>
    public static int Power(this int value, int operand) => (int)Pow(value, operand);
    /// <inheritdoc cref="GreaterOf{T}"/>
    public static int GreaterOf(this int value, int operand) => value < operand ? operand : value;
    /// <inheritdoc cref="LesserOf{T}"/>
    public static int LesserOf(this int value, int operand) => value > operand ? operand : value;
    /// <inheritdoc cref="Absolute{T}"/>
    public static int Absolute(this int value) => value < 0 ? value * -1 : value;
    /// <inheritdoc cref="AsNegative{T}"/>
    public static int AsNegative(this int value) => value > 0 ? value * -1 : value;
    /// <inheritdoc cref="FlipSign{T}"/>
    public static int FlipSign(this int value) => value * -1;

    #endregion

    #region Long

    /// <inheritdoc cref="Add{T}"/>
    public static long Add(this long value, long operand) => value + operand;
    /// <inheritdoc cref="Subtract{T}"/>
    public static long Subtract(this long value, long operand) => value - operand;
    /// <inheritdoc cref="Multiply{T}"/>
    public static long Multiply(this long value, long operand) => value * operand;
    /// <inheritdoc cref="Divide{T}"/>
    public static long Divide(this long value, long operand, bool guard = false) => (guard && operand == 0) ? 0 : value / operand;
    /// <inheritdoc cref="Modulo{T}"/>
    public static long Modulo(this long value, long operand, bool guard = false) => (guard && operand == 0) ? 0 : value % operand;
    /// <inheritdoc cref="Power{T}"/>
    public static long Power(this long value, long operand) => Pow(value, operand);
    /// <inheritdoc cref="GreaterOf{T}"/>
    public static long GreaterOf(this long value, long operand) => value < operand ? operand : value;
    /// <inheritdoc cref="LesserOf{T}"/>
    public static long LesserOf(this long value, long operand) => value > operand ? operand : value;
    /// <inheritdoc cref="Absolute{T}"/>
    public static long Absolute(this long value) => value < 0 ? value * -1 : value;
    /// <inheritdoc cref="AsNegative{T}"/>
    public static long AsNegative(this long value) => value > 0 ? value * -1 : value;
    /// <inheritdoc cref="FlipSign{T}"/>
    public static long FlipSign(this long value) => value * -1;

    #endregion

    #region Decimal

    /// <inheritdoc cref="Add{T}"/>
    public static decimal Add(this decimal value, decimal operand) => value + operand;
    /// <inheritdoc cref="Subtract{T}"/>
    public static decimal Subtract(this decimal value, decimal operand) => value - operand;
    /// <inheritdoc cref="Multiply{T}"/>
    public static decimal Multiply(this decimal value, decimal operand) => value * operand;
    /// <inheritdoc cref="Divide{T}"/>
    public static decimal Divide(this decimal value, decimal operand, bool guard = false) => (guard && operand == 0) ? 0 : value / operand;
    /// <inheritdoc cref="Modulo{T}"/>
    public static decimal Modulo(this decimal value, decimal operand, bool guard = false) => (guard && operand == 0) ? 0 : value % operand;
    /// <inheritdoc cref="Power{T}"/>
    public static decimal Power(this decimal value, decimal operand) => (decimal)Math.Pow((double)value, (double)operand);
    /// <inheritdoc cref="Root{T}"/>
    public static decimal Root(this decimal value, decimal operand, bool guard = false) => (guard && operand == 0) ? 0 : Power(value, 1 / operand);
    /// <inheritdoc cref="GreaterOf{T}"/>
    public static decimal GreaterOf(this decimal value, decimal operand) => value < operand ? operand : value;
    /// <inheritdoc cref="LesserOf{T}"/>
    public static decimal LesserOf(this decimal value, decimal operand) => value > operand ? operand : value;
    /// <inheritdoc cref="Round{T}"/>
    public static decimal Round(this decimal value) => Math.Round(value, MidpointRounding.AwayFromZero);
    /// <inheritdoc cref="Floor{T}"/>
    public static decimal Floor(this decimal value) => Math.Floor(value);
    /// <inheritdoc cref="Ceiling{T}"/>
    public static decimal Ceiling(this decimal value) => Math.Ceiling(value);
    /// <inheritdoc cref="Truncate{T}"/>
    public static decimal Truncate(this decimal value) => Math.Truncate(value);
    /// <inheritdoc cref="Absolute{T}"/>
    public static decimal Absolute(this decimal value) => value < 0 ? value * -1 : value;
    /// <inheritdoc cref="AsNegative{T}"/>
    public static decimal AsNegative(this decimal value) => value > 0 ? value * -1 : value;
    /// <inheritdoc cref="FlipSign{T}"/>
    public static decimal FlipSign(this decimal value) => value * -1;
    /// <inheritdoc cref="Reciprocal{T}"/>
    public static decimal Reciprocal(this decimal value, bool guard = false) => Divide(1, value, guard);

    #endregion

    #region Float

    /// <inheritdoc cref="Add{T}"/>
    public static float Add(this float value, float operand) => value + operand;
    /// <inheritdoc cref="Subtract{T}"/>
    public static float Subtract(this float value, float operand) => value - operand;
    /// <inheritdoc cref="Multiply{T}"/>
    public static float Multiply(this float value, float operand) => value * operand;
    /// <inheritdoc cref="Divide{T}"/>
    public static float Divide(this float value, float operand, bool guard = false) => (guard && operand == 0) ? 0 : value / operand;
    /// <inheritdoc cref="Modulo{T}"/>
    public static float Modulo(this float value, float operand, bool guard = false) => (guard && operand == 0) ? 0 : value % operand;
    /// <inheritdoc cref="Power{T}"/>
    public static float Power(this float value, float operand) => (float)Math.Pow(value, operand);
    /// <inheritdoc cref="Root{T}"/>
    public static float Root(this float value, float operand, bool guard = false) => (guard && operand == 0) ? 0 : Power(value, 1 / operand);
    /// <inheritdoc cref="GreaterOf{T}"/>
    public static float GreaterOf(this float value, float operand) => value < operand ? operand : value;
    /// <inheritdoc cref="LesserOf{T}"/>
    public static float LesserOf(this float value, float operand) => value > operand ? operand : value;
    /// <inheritdoc cref="Round{T}"/>
    public static float Round(this float value) => (float)Math.Round(value, MidpointRounding.AwayFromZero);
    /// <inheritdoc cref="Floor{T}"/>
    public static float Floor(this float value) => (float)Math.Floor(value);
    /// <inheritdoc cref="Ceiling{T}"/>
    public static float Ceiling(this float value) => (float)Math.Ceiling(value);
    /// <inheritdoc cref="Truncate{T}"/>
    public static float Truncate(this float value) => (float)Math.Truncate(value);
    /// <inheritdoc cref="Absolute{T}"/>
    public static float Absolute(this float value) => value < 0 ? value * -1 : value;
    /// <inheritdoc cref="AsNegative{T}"/>
    public static float AsNegative(this float value) => value > 0 ? value * -1 : value;
    /// <inheritdoc cref="FlipSign{T}"/>
    public static float FlipSign(this float value) => value * -1;
    /// <inheritdoc cref="Reciprocal{T}"/>
    public static float Reciprocal(this float value, bool guard = false) => Divide(1, value, guard);

    #endregion

    #region Double

    /// <inheritdoc cref="Add{T}"/>
    public static double Add(this double value, double operand) => value + operand;
    /// <inheritdoc cref="Subtract{T}"/>
    public static double Subtract(this double value, double operand) => value - operand;
    /// <inheritdoc cref="Multiply{T}"/>
    public static double Multiply(this double value, double operand) => value * operand;
    /// <inheritdoc cref="Divide{T}"/>
    public static double Divide(this double value, double operand, bool guard = false) => (guard && operand == 0) ? 0 : value / operand;
    /// <inheritdoc cref="Modulo{T}"/>
    public static double Modulo(this double value, double operand, bool guard = false) => (guard && operand == 0) ? 0 : value % operand;
    /// <inheritdoc cref="Power{T}"/>
    public static double Power(this double value, double operand) => Math.Pow(value, operand);
    /// <inheritdoc cref="Root{T}"/>
    public static double Root(this double value, double operand, bool guard = false) => (guard && operand == 0) ? 0 : Power(value, 1 / operand);
    /// <inheritdoc cref="GreaterOf{T}"/>
    public static double GreaterOf(this double value, double operand) => value < operand ? operand : value;
    /// <inheritdoc cref="LesserOf{T}"/>
    public static double LesserOf(this double value, double operand) => value > operand ? operand : value;
    /// <inheritdoc cref="Round{T}"/>
    public static double Round(this double value) => Math.Round(value, MidpointRounding.AwayFromZero);
    /// <inheritdoc cref="Floor{T}"/>
    public static double Floor(this double value) => Math.Floor(value);
    /// <inheritdoc cref="Ceiling{T}"/>
    public static double Ceiling(this double value) => Math.Ceiling(value);
    /// <inheritdoc cref="Truncate{T}"/>
    public static double Truncate(this double value) => Math.Truncate(value);
    /// <inheritdoc cref="Absolute{T}"/>
    public static double Absolute(this double value) => value < 0 ? value * -1 : value;
    /// <inheritdoc cref="AsNegative{T}"/>
    public static double AsNegative(this double value) => value > 0 ? value * -1 : value;
    /// <inheritdoc cref="FlipSign{T}"/>
    public static double FlipSign(this double value) => value * -1;
    /// <inheritdoc cref="Reciprocal{T}"/>
    public static double Reciprocal(this double value, bool guard = false) => Divide(1, value, guard);

    #endregion

    /// <summary> Returns the sum of the <paramref name="value"/> and <paramref name="operand"/>. </summary>
    internal static T Add<T>(this T value, T operand) where T : unmanaged => throw UnsupportedException("Add", typeof(T));

    /// <summary> Returns the difference of the <paramref name="value"/> and <paramref name="operand"/>. </summary>
    internal static T Subtract<T>(this T value, T operand) where T : unmanaged => throw UnsupportedException("Subtract", typeof(T));

    /// <summary> Returns the product of the <paramref name="value"/> and <paramref name="operand"/>. </summary>
    internal static T Multiply<T>(this T value, T operand) where T : unmanaged => throw UnsupportedException("Multiply", typeof(T));

    /// <summary> Returns the quotient of the <paramref name="value"/> and <paramref name="operand"/>. </summary>
    /// <param name="guard"> Whether to return 0 instead of throwing an error if the <paramref name="operand"/> is 0. </param>
    internal static T Divide<T>(this T value, T operand, bool guard = false) where T : unmanaged => throw UnsupportedException("Divide", typeof(T));

    /// <summary> Returns the remainder of the division between the <paramref name="value"/> and <paramref name="operand"/>. </summary>
    /// <param name="guard"> Whether to return 0 instead of throwing an error if the <paramref name="operand"/> is 0. </param>
    internal static T Modulo<T>(this T value, T operand, bool guard = false) where T : unmanaged => throw UnsupportedException("Modulo", typeof(T));

    /// <summary> Returns the result of the <paramref name="value"/> raised to the <paramref name="operand"/>. </summary>
    internal static T Power<T>(this T value, T operand) where T : unmanaged => throw UnsupportedException("Power", typeof(T));

    /// <summary> Returns the root of the <paramref name="value"/> with the <paramref name="operand"/> as the degree of the root. </summary>
    /// <param name="guard"> Whether to return 0 instead of throwing an error if the <paramref name="operand"/> is 0. </param>
    internal static T Root<T>(this T value, T operand, bool guard = false) where T : unmanaged => throw UnsupportedException("Root", typeof(T));

    /// <summary> Returns larger of <paramref name="value"/> and <paramref name="operand"/>. </summary>
    internal static T GreaterOf<T>(this T value, T operand) where T : unmanaged => throw UnsupportedException("GreaterOf", typeof(T));

    /// <summary> Returns smaller of <paramref name="value"/> and <paramref name="operand"/>. </summary>
    internal static T LesserOf<T>(this T value, T operand) where T : unmanaged => throw UnsupportedException("LesserOf", typeof(T));

    /// <summary> Returns the <paramref name="value"/> rounded to the nearest integer. </summary>
    internal static T Round<T>(this T value) where T : unmanaged => throw UnsupportedException("Round", typeof(T));

    /// <summary> Returns the <paramref name="value"/> rounded downward. </summary>
    internal static T Floor<T>(this T value) where T : unmanaged => throw UnsupportedException("Floor", typeof(T));

    /// <summary> Returns the <paramref name="value"/> rounded upward. </summary>
    internal static T Ceiling<T>(this T value) where T : unmanaged => throw UnsupportedException("Ceiling", typeof(T));

    /// <summary> Returns the <paramref name="value"/> with any fractional portion removed. </summary>
    internal static T Truncate<T>(this T value) where T : unmanaged => throw UnsupportedException("Truncate", typeof(T));

    /// <summary> Returns the non-negative magnitude of the <paramref name="value"/> (distance from 0). </summary>
    internal static T Absolute<T>(this T value) where T : unmanaged => throw UnsupportedException("Absolute", typeof(T));

    /// <summary> Returns the <paramref name="value"/> as negative with the same magnitude. </summary>
    internal static T AsNegative<T>(this T value) where T : unmanaged => throw UnsupportedException("AsNegative", typeof(T));

    /// <summary> Returns the <paramref name="value"/> with its sign flipped. </summary>
    internal static T FlipSign<T>(this T value) where T : unmanaged => throw UnsupportedException("FlipSign", typeof(T));

    /// <summary> Returns quotient of 1 divided by the <paramref name="value"/>. </summary>
    /// <param name="guard"> Whether to return 0 instead of throwing an error if the <paramref name="value"/> is 0. </param>
    internal static T Reciprocal<T>(this T value, bool guard = false) where T : unmanaged => throw UnsupportedException("Reciprocal", typeof(T));

    internal static T Operate<T>(this T value, NumericOperation operation, T operand = default, bool guard = false) where T : unmanaged
    {
      return operation switch
      {
        NumericOperation.Add => Add(value, operand),
        NumericOperation.Subtract => Subtract(value, operand),
        NumericOperation.Multiply => Multiply(value, operand),
        NumericOperation.Divide => Divide(value, operand, guard),
        NumericOperation.Modulo => Modulo(value, operand, guard),
        NumericOperation.Power => Power(value, operand),
        NumericOperation.Root => Root(value, operand, guard),
        NumericOperation.GreaterOf => GreaterOf(value, operand),
        NumericOperation.LesserOf => LesserOf(value, operand),
        NumericOperation.Round => Round(value),
        NumericOperation.Floor => Floor(value),
        NumericOperation.Ceiling => Ceiling(value),
        NumericOperation.Truncate => Truncate(value),
        NumericOperation.Absolute => Absolute(value),
        NumericOperation.AsNegative => AsNegative(value),
        NumericOperation.FlipSign => FlipSign(value),
        NumericOperation.Reciprocal => Reciprocal(value, guard),
        _ => throw UnsupportedException(operation.ToString(), typeof(T))
      };
    }

    internal static void ValidateType(Type type)
    {
      switch (Type.GetTypeCode(type))
      {
        case TypeCode.UInt16:
        case TypeCode.UInt32:
        case TypeCode.UInt64:
        case TypeCode.Int16:
        case TypeCode.Int32:
        case TypeCode.Int64:
        case TypeCode.Decimal:
        case TypeCode.Single:
        case TypeCode.Double:
        {
          return;
        }
        default:
        {
          throw new ArgumentException($"Type of {type.Name} not valid. Only numeric unmanaged types are allowed.");
        }
      }
    }

    private static Exception UnsupportedException(string operation, Type type)
    {
      return new ArgumentException($"Unhandled operation {operation} on type: '{type.Name}'.");
    }

    private static long Pow(long x, long y)
    {
      if(y < 0) { return 0; }
      if(y == 0) { return 1; }
      if(y == 1) { return x; }
      long resl = x;
      while(y > 1)
      {
        resl *= x;
        y--;
      }
      return resl;
    }

    private static ulong UPow(ulong x, ulong y)
    {
      if(y < 0) { return 0; }
      if(y == 0) { return 1; }
      if(y == 1) { return x; }
      ulong resl = x;
      while(y > 1)
      {
        resl *= x;
        y--;
      }
      return resl;
    }

  }
}