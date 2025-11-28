using System;


namespace Watchables
{
  public enum OperationType
  {
    Add,
    Subtract,
    Multiply,
    Divide,
    Modulo,
    ToPower,
    AsPower,
    ToRoot,
    AsRoot,
    Minimum,
    Maximum,
    Round,
    Floor,
    Ceiling,
    Truncate,
    Absolute,
    AsNegative,
    FlipSign,
    Reciprocal
  }

  public static class OperationTypeExtensions
  {

    public static float Operate(this OperationType op, float x, float y)
    {
      return op switch
      {
        OperationType.Add => x + y,
        OperationType.Subtract => x - y,
        OperationType.Multiply => x * y,
        OperationType.Divide => y != 0f ? x / y : 0f,
        OperationType.Modulo => y != 0f ? x % y : 0f,
        OperationType.ToPower => (float)Math.Pow(x, y),
        OperationType.AsPower => (float)Math.Pow(y, x),
        OperationType.ToRoot => y != 0f ? (float)Math.Pow(x, 1f / y) : 0f,
        OperationType.AsRoot => x != 0f ? (float)Math.Pow(y, 1f / x) : 0f,
        OperationType.Minimum => y > x ? y : x,
        OperationType.Maximum => y < x ? y : x,
        OperationType.Round => (float)Math.Round(x),
        OperationType.Floor => (float)Math.Floor(x),
        OperationType.Ceiling => (float)Math.Ceiling(x),
        OperationType.Truncate => (float)Math.Truncate(x),
        OperationType.Absolute => (float)Math.Abs(x),
        OperationType.AsNegative => x > 0f ? x * -1f : x,
        OperationType.FlipSign => x * -1f,
        OperationType.Reciprocal => x != 0f ? 1f / x : 0f,
        _ => x
      };
    }

    public static int Operate(this OperationType op, int x, int y)
    {
      return op switch
      {
        OperationType.Add => x + y,
        OperationType.Subtract => x - y,
        OperationType.Multiply => x * y,
        OperationType.Divide => y != 0 ? x / y : 0,
        OperationType.Modulo => y != 0 ? x % y : 0,
        OperationType.ToPower => IntPow(x, y),
        OperationType.AsPower => IntPow(x, y),
        OperationType.ToRoot => 0, //Few predictable and meaningful answers, don't use
        OperationType.AsRoot => 0, //Few predictable and meaningful answers, don't use
        OperationType.Minimum => y > x ? y : x,
        OperationType.Maximum => y < x ? y : x,
        OperationType.Round or
        OperationType.Floor or
        OperationType.Ceiling or
        OperationType.Truncate => x,
        OperationType.Absolute => Math.Abs(x),
        OperationType.AsNegative => x > 0 ? x * -1 : x,
        OperationType.FlipSign => x * -1,
        OperationType.Reciprocal => 0, //Few predictable and meaningful answers, don't use
        _ => x
      };
    }

    public static double Operate(this OperationType op, double x, double y)
    {
      return op switch
      {
        OperationType.Add => x + y,
        OperationType.Subtract => x - y,
        OperationType.Multiply => x * y,
        OperationType.Divide => y != 0 ? x / y : 0,
        OperationType.Modulo => y != 0 ? x % y : 0,
        OperationType.ToPower => Math.Pow(x, y),
        OperationType.AsPower => Math.Pow(y, x),
        OperationType.ToRoot => y != 0 ? Math.Pow(x, 1 / y) : 0,
        OperationType.AsRoot => x != 0 ? Math.Pow(y, 1 / x) : 0,
        OperationType.Minimum => y > x ? y : x,
        OperationType.Maximum => y < x ? y : x,
        OperationType.Round => Math.Round(x),
        OperationType.Floor => Math.Floor(x),
        OperationType.Ceiling => Math.Ceiling(x),
        OperationType.Truncate => Math.Truncate(x),
        OperationType.Absolute => Math.Abs(x),
        OperationType.AsNegative => x > 0 ? x * -1 : x,
        OperationType.FlipSign => x * -1,
        OperationType.Reciprocal => x != 0 ? 1 / x : 0,
        _ => x
      };
    }

    private static int IntPow(int x, int y)
    {
      if(y < 0)
      {
        return 0;
      }
      if(y == 0)
      {
        return 1;
      }
      if(y == 1)
      {
        return x;
      }
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