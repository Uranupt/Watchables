using System.Collections.Generic;


namespace Watchables
{ 
  public readonly struct Tags<T> where T : Tag<T>
  {

    private readonly uint _value;

    /// <summary> Whether this instance does not represent any <typeparamref name="T"/> values. </summary>
    public bool IsNone => _value == 0;

    private Tags(uint value)
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

    /// <inheritdoc cref="With(T)"/>
    public static Tags<T> operator +(Tags<T> tags, T tag) => tags.With(tag);

    /// <inheritdoc cref="With(Tags{T})"/>
    public static Tags<T> operator +(Tags<T> tags, Tags<T> other) => tags.With(other);

    /// <inheritdoc cref="Without(T)"/>
    public static Tags<T> operator -(Tags<T> tags, T tag) => tags.Without(tag);

    /// <inheritdoc cref="Without(Tags{T})"/>
    public static Tags<T> operator -(Tags<T> tags, Tags<T> other) => tags.Without(other);

    /// <inheritdoc cref="Flip(T)"/>
    public static Tags<T> operator ^(Tags<T> tags, T tag) => tags.Flip(tag);

    /// <inheritdoc cref="Flip(Tags{T})"/>
    public static Tags<T> operator ^(Tags<T> tags, Tags<T> other) => tags.Flip(other);

    /// <inheritdoc cref="Invert()"/>
    public static Tags<T> operator ~(Tags<T> tags) => tags.Invert();

    /// <summary> Determines if the current {</summary>
    public static bool operator ==(Tags<T> tags, T tag) => tags._value == tag.Mask;
    public static bool operator !=(Tags<T> tags, T tag) => tags._value != tag.Mask;
    public static bool operator ==(Tags<T> tags, Tags<T> other) => tags.HasExactly(other);
    public static bool operator !=(Tags<T> tags, Tags<T> other) => !tags.HasExactly(other);

    /// <summary> Returns a version of the current <see cref="Tags{T}"/> which also contains the given <paramref name="tag"/>. </summary>
    public readonly Tags<T> With(T tag) => new Tags<T>(_value | tag.Mask);

    /// <summary> 
    /// Returns a version of the current <see cref="Tags{T}"/> which also contains the values in the <paramref name="other"/> <see cref="Tags{T}"/>. 
    /// </summary>
    public readonly Tags<T> With(Tags<T> other) => new Tags<T>(_value | other._value);

    /// <summary> Returns a version of the current <see cref="Tags{T}"/> which does not contain the given <paramref name="tag"/>. </summary>
    public readonly Tags<T> Without(T tag) => new Tags<T>(_value & ~tag.Mask);

    /// <summary> 
    /// Returns a version of the current <see cref="Tags{T}"/> which does not contain the values in the <paramref name="other"/> <see cref="Tags{T}"/>. 
    /// </summary>
    public readonly Tags<T> Without(Tags<T> other) => new Tags<T>(_value & ~other._value);

    /// <summary> Returns a version of the current <see cref="Tags{T}"/> which inverts whether it contains given <paramref name="tag"/>. </summary>
    public readonly Tags<T> Flip(T tag) => new Tags<T>(_value ^ tag.Mask);

    /// <summary> 
    /// Returns a version of the current <see cref="Tags{T}"/> which inverts whether it contains the values in the <paramref name="other"/> <see cref="Tags{T}"/>. 
    /// </summary>
    public readonly Tags<T> Flip(Tags<T> other) => new Tags<T>(_value ^ other._value);

    /// <summary> Returns a version of the current <see cref="Tags{T}"/> which reverses which <typeparamref name="T"/> values it does and doesn't contain. </summary>
    public readonly Tags<T> Invert() => new Tags<T>(_value ^ Tag<T>.AllMask);

    /// <summary> Determines if the current <see cref="Tags{T}"/> contains the given <paramref name="tag"/>. </summary>
    public readonly bool Has(T tag) => (_value & tag.Mask) == tag.Mask;

    /// <summary> Determines if the current <see cref="Tags{T}"/> contains any of the given <paramref name="tags"/>. </summary>
    public readonly bool HasAny(Tags<T> tags) => (_value & tags._value) != 0;

    /// <summary> Determines if the current <see cref="Tags{T}"/> contains all of the given <paramref name="tags"/>. </summary>
    public readonly bool HasAll(Tags<T> tags) => (_value & tags._value) == tags._value;

    /// <summary> Determines if the current <see cref="Tags{T}"/> is identical to the given <paramref name="tags"/>. </summary>
    public readonly bool HasExactly(Tags<T> tags) => _value == tags._value;

    /// <summary> Builds a <see cref="List{T}"/> of the <typeparamref name="T"/> values contained in this <see cref="Tags{T}"/>. </summary>
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

    /// <inheritdoc/>
    public override readonly bool Equals(object obj)
    {
      return obj is Tags<T> other && other._value == _value;
    }

    /// <inheritdoc/>
    public override readonly int GetHashCode() => (int)_value;

    /// <summary>
    /// Returns a <see cref="string"/> representing all of the <typeparamref name="T"/> values contained in this <see cref="Tags{T}"/>.
    /// </summary>
    public override string ToString()
    {
      if(IsNone) { return "None";  }
      string resl = "";
      foreach(T tag in Tag<T>.AllTags)
      {
        if(Has(tag))
        {
          resl += (tag + ", ");
        }
      }
      return resl.TrimEnd(',', ' ');
    }

  }
}