using System.Collections.Generic;


namespace Watchables
{ 
  public readonly struct Tags<T> where T : Tag<T>
  {

    private readonly int _value;

    public bool IsNone => _value == 0;

    private Tags(int value)
    {
      _value = value;
    }

    public Tags(params T[] tags)
    {
      _value = 0;
      foreach(T tag in tags)
      {
        _value |= tag.Mask;
      }
    }

    public static Tags<T> operator +(Tags<T> tags, T tag) => tags.With(tag);
    public static Tags<T> operator +(Tags<T> tags, Tags<T> other) => tags.With(other);
    public static Tags<T> operator -(Tags<T> tags, T tag) => tags.Without(tag);
    public static Tags<T> operator -(Tags<T> tags, Tags<T> other) => tags.Without(other);
    public static Tags<T> operator ^(Tags<T> tags, T tag) => tags.Flip(tag);
    public static Tags<T> operator ^(Tags<T> tags, Tags<T> other) => tags.Flip(other);
    public static Tags<T> operator ~(Tags<T> tags) => tags.Invert();

    public static bool operator ==(Tags<T> tags, T tag) => tags._value == tag.Mask;
    public static bool operator !=(Tags<T> tags, T tag) => tags._value != tag.Mask;
    public static bool operator ==(Tags<T> tags, Tags<T> other) => tags.HasExactly(other);
    public static bool operator !=(Tags<T> tags, Tags<T> other) => !tags.HasExactly(other);

    public readonly Tags<T> With(T tag) => new Tags<T>(_value | tag.Mask);
    public readonly Tags<T> With(Tags<T> other) => new Tags<T>(_value | other._value);
    public readonly Tags<T> Without(T tag) => new Tags<T>(_value & ~tag.Mask);
    public readonly Tags<T> Without(Tags<T> other) => new Tags<T>(_value & ~other._value);
    public readonly Tags<T> Flip(T tag) => new Tags<T>(_value ^ tag.Mask);
    public readonly Tags<T> Flip(Tags<T> other) => new Tags<T>(_value ^ other._value);
    public readonly Tags<T> Invert() => new Tags<T>(_value ^ Tag<T>.AllMask);

    public readonly bool Has(T tag) => (_value & tag.Mask) == tag.Mask;
    public readonly bool HasAny(Tags<T> other) => (_value & other._value) != 0;
    public readonly bool HasAll(Tags<T> other) => (_value & other._value) == other._value;
    public readonly bool HasExactly(Tags<T> other) => _value == other._value;

    public readonly List<T> ToList()
    {
      List<T> resl = new();
      foreach(T tag in Tag<T>.AllTags)
      {
        if(Has(tag))
        {
          resl.Add(tag);
        }
      }
      return resl;
    }

    public override readonly bool Equals(object obj)
    {
      if(obj is Tags<T> other)
      {
        return other._value == _value;
      }
      else if(obj is T tag)
      {
        return tag.Mask == _value;
      }
      return false;
    }

    public override readonly int GetHashCode() => _value;

    public override string ToString()
    {
      string resl = "";
      foreach(T tag in Tag<T>.AllTags)
      {
        if(Has(tag))
        {
          resl += (tag + ", ");
        }
      }
      if(resl == "")
      {
        resl = "None";
      }
      else
      {
        resl.TrimEnd(',', ' ');
      }
      return resl;
    }

  }
}