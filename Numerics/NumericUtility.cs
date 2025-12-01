using System;


namespace Watchables
{ 
  public static class NumericUtility
  {

    #region Short

    public static short Add(this short value, short operand) => value + operand;
    public static short Subtract(this short value, short operand) => value - operand;
    public static short Multiply(this short value, short operand) => value * operand;
    public static short Divide(this short value, short operand, bool guard = false) => (guard && operand == 0) ? 0 : value / operand;
    public static short Modulo(this short value, short operand, bool guard = false) => (guard && operand == 0) ? 0 : value % operand;
    public static short Power(this short value, short operand) => (short)Pow(value, operand);
    public static short Minimum(this short value, short operand) => value < operand ? operand : value;
    public static short Maximum(this short value, short operand) => value > operand ? operand : value;
    public static short Absolute(this short value) => value < 0 ? value * -1 : value;
    public static short AsNegative(this short value) => value > 0 ? value * -1 : value;
    public static short FlipSign(this short value) => value * -1;

    #endregion

    #region Int

    public static int Add(this int value, int operand) => value + operand;
    public static int Subtract(this int value, int operand) => value - operand;
    public static int Multiply(this int value, int operand) => value * operand;
    public static int Divide(this int value, int operand, bool guard = false) => (guard && operand == 0) ? 0 : value / operand;
    public static int Modulo(this int value, int operand, bool guard = false) => (guard && operand == 0) ? 0 : value % operand;
    public static int Power(this int value, int operand) => (int)Pow(value, operand);
    public static int Minimum(this int value, int operand) => value < operand ? operand : value;
    public static int Maximum(this int value, int operand) => value > operand ? operand : value;
    public static int Absolute(this int value) => value < 0 ? value * -1 : value;
    public static int AsNegative(this int value) => value > 0 ? value * -1 : value;
    public static int FlipSign(this int value) => value * -1;

    #endregion

    #region Long

    public static long Add(this long value, long operand) => value + operand;
    public static long Subtract(this long value, long operand) => value - operand;
    public static long Multiply(this long value, long operand) => value * operand;
    public static long Divide(this long value, long operand, bool guard = false) => (guard && operand == 0) ? 0 : value / operand;
    public static long Modulo(this long value, long operand, bool guard = false) => (guard && operand == 0) ? 0 : value % operand;
    public static long Power(this long value, long operand) => Pow(value, operand);
    public static long Minimum(this long value, long operand) => value < operand ? operand : value;
    public static long Maximum(this long value, long operand) => value > operand ? operand : value;
    public static long Absolute(this long value) => value < 0 ? value * -1 : value;
    public static long AsNegative(this long value) => value > 0 ? value * -1 : value;
    public static long FlipSign(this long value) => value * -1;

    #endregion

    #region UShort

    public static ushort Add(this ushort value, ushort operand) => value + operand;
    public static ushort Subtract(this ushort value, ushort operand) => value - operand;
    public static ushort Multiply(this ushort value, ushort operand) => value * operand;
    public static ushort Divide(this ushort value, ushort operand, bool guard = false) => (guard && operand == 0) ? 0 : value / operand;
    public static ushort Modulo(this ushort value, ushort operand, bool guard = false) => (guard && operand == 0) ? 0 : value % operand;
    public static ushort Power(this ushort value, ushort operand) => (ushort)Pow(value, operand);
    public static ushort Minimum(this ushort value, ushort operand) => value < operand ? operand : value;
    public static ushort Maximum(this ushort value, ushort operand) => value > operand ? operand : value;
    public static ushort Absolute(this ushort value) => value < 0 ? value * -1 : value;
    public static ushort AsNegative(this ushort value) => value > 0 ? value * -1 : value;
    public static ushort FlipSign(this ushort value) => value * -1;

    #endregion

    #region UInt

    public static uint Add(this uint value, uint operand) => value + operand;
    public static uint Subtract(this uint value, uint operand) => value - operand;
    public static uint Multiply(this uint value, uint operand) => value * operand;
    public static uint Divide(this uint value, uint operand, bool guard = false) => (guard && operand == 0) ? 0 : value / operand;
    public static uint Modulo(this uint value, uint operand, bool guard = false) => (guard && operand == 0) ? 0 : value % operand;
    public static uint Power(this uint value, uint operand) => (uint)Pow(value, operand);
    public static uint Minimum(this uint value, uint operand) => value < operand ? operand : value;
    public static uint Maximum(this uint value, uint operand) => value > operand ? operand : value;
    public static uint Absolute(this uint value) => value < 0 ? value * -1 : value;
    public static uint AsNegative(this uint value) => value > 0 ? value * -1 : value;
    public static uint FlipSign(this uint value) => value * -1;

    #endregion

    #region ULong

    public static ulong Add(this ulong value, ulong operand) => value + operand;
    public static ulong Subtract(this ulong value, ulong operand) => value - operand;
    public static ulong Multiply(this ulong value, ulong operand) => value * operand;
    public static ulong Divide(this ulong value, ulong operand, bool guard = false) => (guard && operand == 0) ? 0 : value / operand;
    public static ulong Modulo(this ulong value, ulong operand, bool guard = false) => (guard && operand == 0) ? 0 : value % operand;
    public static ulong Power(this ulong value, ulong operand) => Pow(value, operand);
    public static ulong Minimum(this ulong value, ulong operand) => value < operand ? operand : value;
    public static ulong Maximum(this ulong value, ulong operand) => value > operand ? operand : value;
    public static ulong Absolute(this ulong value) => value < 0 ? value * -1 : value;
    public static ulong AsNegative(this ulong value) => value > 0 ? value * -1 : value;
    public static ulong FlipSign(this ulong value) => value * -1;

    #endregion

    #region Decimal

    public static decimal Add(this decimal value, decimal operand) => value + operand;
    public static decimal Subtract(this decimal value, decimal operand) => value - operand;
    public static decimal Multiply(this decimal value, decimal operand) => value * operand;
    public static decimal Divide(this decimal value, decimal operand, bool guard = false) => (guard && operand == 0) ? 0 : value / operand;
    public static decimal Modulo(this decimal value, decimal operand, bool guard = false) => (guard && operand == 0) ? 0 : value % operand;
    public static decimal Power(this decimal value, decimal operand) => (decimal)Math.Pow(value, operand);
    public static decimal Root(this decimal value, decimal operand, bool guard = false) => (guard && operand == 0) ? 0 : (decimal)Math.Pow(value, 1 / operand);
    public static decimal Minimum(this decimal value, decimal operand) => value < operand ? operand : value;
    public static decimal Maximum(this decimal value, decimal operand) => value > operand ? operand : value;
    public static decimal Round(this decimal value) => Math.Round(value, MidpointRounding.AwayFromZero);
    public static decimal Floor(this decimal value) => Math.Floor(value);
    public static decimal Ceiling(this decimal value) => Math.Ceiling(value);
    public static decimal Truncate(this decimal value) => Math.Truncate(value);
    public static decimal Absolute(this decimal value) => value < 0 ? value * -1 : value;
    public static decimal AsNegative(this decimal value) => value > 0 ? value * -1 : value;
    public static decimal FlipSign(this decimal value) => value * -1;
    public static decimal Reciprocal(this decimal value, bool guard = false) => Divide(1, value, guard);

    #endregion

    #region Float

    public static float Add(this float value, float operand) => value + operand;
    public static float Subtract(this float value, short operand) => value - operand;
    public static float Multiply(this float value, float operand) => value * operand;
    public static float Divide(this float value, float operand, bool guard = false) => (guard && operand == 0) ? 0 : value / operand;
    public static float Modulo(this float value, float operand, bool guard = false) => (guard && operand == 0) ? 0 : value % operand;
    public static float Power(this float value, float operand) => (float)Math.Pow(value, operand);
    public static float Root(this float value, float operand, bool guard = false) => (guard && operand == 0) ? 0 : (float)Math.Pow(value, 1 / operand);
    public static float Minimum(this float value, float operand) => value < operand ? operand : value;
    public static float Maximum(this float value, float operand) => value > operand ? operand : value;
    public static float Round(this float value) => (float)Math.Round(value, MidpointRounding.AwayFromZero);
    public static float Floor(this float value) => (float)Math.Floor(value);
    public static float Ceiling(this float value) => (float)Math.Ceiling(value);
    public static float Truncate(this float value) => (float)Math.Truncate(value);
    public static float Absolute(this float value) => value < 0 ? value * -1 : value;
    public static float AsNegative(this float value) => value > 0 ? value * -1 : value;
    public static float FlipSign(this float value) => value * -1;
    public static float Reciprocal(this float value, bool guard = false) => Divide(1, value, guard);

    #endregion

    #region Double

    public static double Add(this double value, double operand) => value + operand;
    public static double Subtract(this double value, double operand) => value - operand;
    public static double Multiply(this double value, double operand) => value * operand;
    public static double Divide(this double value, double operand, bool guard = false) => (guard && operand == 0) ? 0 : value / operand;
    public static double Modulo(this double value, double operand, bool guard = false) => (guard && operand == 0) ? 0 : value % operand;
    public static double Power(this double value, double operand) => Math.Pow(value, operand);
    public static double Root(this double value, double operand, bool guard = false) => (guard && operand == 0) ? 0 : Math.Pow(value, 1 / operand);
    public static double Minimum(this double value, double operand) => value < operand ? operand : value;
    public static double Maximum(this double value, double operand) => value > operand ? operand : value;
    public static double Round(this double value) => Math.Round(value, MidpointRounding.AwayFromZero);
    public static double Floor(this double value) => Math.Floor(value);
    public static double Ceiling(this double value) => Math.Ceiling(value);
    public static double Truncate(this double value) => Math.Truncate(value);
    public static double Absolute(this double value) => value < 0 ? value * -1 : value;
    public static double AsNegative(this double value) => value > 0 ? value * -1 : value;
    public static double FlipSign(this double value) => value * -1;
    public static double Reciprocal(this double value, bool guard = false) => Divide(1, value, guard);

    #endregion

    internal static T Add<T>(this T value, T operand) where T : unmanaged => throw UnsupportedException("Add", typeof(T));
    internal static T Subtract<T>(this T value, T operand) where T : unmanaged => throw UnsupportedException("Subtract", typeof(T));
    internal static T Multiply<T>(this T value, T operand) where T : unmanaged => throw UnsupportedException("Multiply", typeof(T));
    internal static T Divide<T>(this T value, T operand, bool guard = false) where T : unmanaged => throw UnsupportedException("Divide", typeof(T));
    internal static T Modulo<T>(this T value, T operand, bool guard = false) where T : unmanaged => throw UnsupportedException("Modulo", typeof(T));
    internal static T Power<T>(this T value, T operand) where T : unmanaged => throw UnsupportedException("Power", typeof(T));
    internal static T Root<T>(this T value, T operand, bool guard = false) where T : unmanaged => throw UnsupportedException("Root", typeof(T));
    internal static T Minimum<T>(this T value, T operand) where T : unmanaged => throw UnsupportedException("Minimum", typeof(T));
    internal static T Maximum<T>(this T value, T operand) where T : unmanaged => throw UnsupportedException("Maximum", typeof(T));
    internal static T Round<T>(this T value) where T : unmanaged => throw UnsupportedException("Round", typeof(T));
    internal static T Floor<T>(this T value) where T : unmanaged => throw UnsupportedException("Floor", typeof(T));
    internal static T Ceiling<T>(this T value) where T : unmanaged => throw UnsupportedException("Ceiling", typeof(T));
    internal static T Truncate<T>(this T value) where T : unmanaged => throw UnsupportedException("Truncate", typeof(T));
    internal static T Absolute<T>(this T value) where T : unmanaged => throw UnsupportedException("Absolute", typeof(T));
    internal static T AsNegative<T>(this T value) where T : unmanaged => throw UnsupportedException("AsNegative", typeof(T));
    internal static T FlipSign<T>(this T value) where T : unmanaged => throw UnsupportedException("FlipSign", typeof(T));
    internal static T Reciprocal<T>(this T value, bool guard = false) where T : unmanaged => throw UnsupportedException("Reciprocal", typeof(T));

    internal static T Operate<T>(this T value, NumericOperation operation, T operand = default, bool guard = false) where T : unmanaged
    {
      return operation switch
      {
        Add => Add(value, operand),
        Subtract => Subtract(value, operand),
        Multiply => Multiply(value, operand),
        Divide => Divide(value, operand, guard),
        Modulo => Modulo(value, operand, guard),
        Power => Power(value, operand),
        Root => Root(value, operand, guard),
        Minimum => Minimum(value, operand),
        Maximum => Maximum(value, operand),
        Round => Round(value, operand),
        Floor => Floor(value, operand),
        Ceiling => Ceiling(value, operand),
        Truncate => Truncate(value, operand),
        Absolute => Absolute(value, operand),
        AsNegative => AsNegative(value, operand),
        FlipSign => FlipSign(value, operand),
        Reciprocal => Reciprocal(value, operand, guard),
        _ => throw UnsupportedException(operation.ToString(), typeof(T))
      };
    }

    private static Exception UnsupportedException(string operation, Type type)
    {
      return new ArgumentException($"Unhandled operation {operation} on type: '{type.Name}', only unmanaged numeric types are allowed.");
    }

    private static long Pow(long x, long y)
    {
      if(y < 0) { return 0; }
      if(y == 0) { return 1; }
      if(y == 1) { return x; }
      int resl = x;
      while(y > 1)
      {
        resl *= x;
        y--;
      }
      return resl;
    }

    private static ulong Pow(ulong x, ulong y)
    {
      if(y < 0) { return 0; }
      if(y == 0) { return 1; }
      if(y == 1) { return x; }
      int resl = x;
      while(y > 1)
      {
        resl *= x;
        y--;
      }
      return resl;
    }

  }
}