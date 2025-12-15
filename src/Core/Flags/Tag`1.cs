using System;
using System.Collections.Generic;


namespace Watchables
{
  /// <summary>
  /// A strongly-typed, extensible 32-bit <see cref="Enum"/> alternative that provides named singleton identities
  /// with stable indices and bitmask values for use in generic and composable systems.
  /// The <see cref="Tags{T}"/> companion struct can provide <see cref="FlagsAttribute"/> behavior for implementations of this type.
  /// </summary>
  /// <remarks>Derived types must declare a protected or private constructor.</remarks>
  /// <typeparam name="TSelf"> The self-referential type of this Tag. </typeparam>
  public abstract class Tag<TSelf> where TSelf : Tag<TSelf>
  {

    private static readonly Dictionary<string, TSelf> _tags = new();
    private static bool _initialized;

    /// <summary> All of the defined tags of this type. </summary>
    public static IReadOnlyCollection<TSelf> AllTags => _tags.Values;

    /// <summary> All of the defined names of the tags of this type. </summary>
    public static IReadOnlyCollection<string> AllNames => _tags.Keys;

    /// <summary> A bitmask which represents the combination of the <see cref="Mask"/> of every defined tag. </summary>
    public static uint AllMask { get; private set; } = 0;

    /// <summary> The name of this <typeparamref name="TSelf"/>. </summary>
    public string Name { get; private set; }

    /// <summary> The bit index of this <typeparamref name="TSelf"/>. </summary>
    public int Index { get; private set; }

    /// <summary> The bitmask of this <typeparamref name="TSelf"/>. </summary>
    public uint Mask { get; private set; }

    static Tag()
    {
      EnsureInitialized();
    }

    /// <summary> Tags should not declare any logic and must not be publicly constructible. </summary>
    protected Tag()
    {

    }

    /// <summary>
    /// Retrieves the <typeparamref name="TSelf"/> instance with the given <paramref name="name"/>.
    /// </summary>
    /// <exception cref="KeyNotFoundException"></exception>
    public static TSelf Get(string name)
    {
      if(!TryGet(name, out TSelf tag))
      {
        throw new KeyNotFoundException($"The Tag type {typeof(TSelf).Name} has no tag with name '{name}'.");
      }
      return tag;
    }

    /// <summary>
    /// Attempts to retrieve the <typeparamref name="TSelf"/> instance with the given <paramref name="name"/>.
    /// </summary>
    /// <returns> If the <typeparamref name="TSelf"/> was found. </returns>
    public static bool TryGet(string name, out TSelf tag)
    {
      return _tags.TryGetValue(name, out tag);
    }

    /// <summary>
    /// Retrieves the <typeparamref name="TSelf"/> instance with the given bit <paramref name="index"/>.
    /// </summary>
    /// <exception cref="KeyNotFoundException"></exception>
    public static TSelf FromIndex(int index)
    {
      if(!TryGetFromIndex(index, out TSelf tag))
      {
        throw new KeyNotFoundException($"The Tag type {typeof(TSelf).Name} has no tag at index {index}.");
      }
      return tag;
    }

    /// <summary>
    /// Attempts to retrieve the <typeparamref name="TSelf"/> instance with the given bit <paramref name="index"/>.
    /// </summary>
    /// <returns> If the <typeparamref name="TSelf"/> was found. </returns>
    public static bool TryGetFromIndex(int index, out TSelf tag)
    {
      tag = null;
      foreach(TSelf instance in _tags.Values)
      {
        if(instance.Index == index)
        {
          tag = instance;
          return true;
        }
      }
      return false;
    }

    /// <summary>
    /// Retrieves the <typeparamref name="TSelf"/> instance with the given bit<paramref name="mask"/>.
    /// </summary>
    /// <exception cref="KeyNotFoundException"></exception>
    public static TSelf FromMask(uint mask)
    {
      if(!TryGetFromMask(mask, out TSelf tag))
      {
        throw new KeyNotFoundException($"The Tag type {typeof(TSelf).Name} has no tag with mask value {mask}.");
      }
      return tag;
    }

    /// <summary>
    /// Attempts to retrieve the <typeparamref name="TSelf"/> instance with the given bit<paramref name="mask"/>.
    /// </summary>
    /// <returns> If the <typeparamref name="TSelf"/> was found. </returns>
    public static bool TryGetFromMask(uint mask, out TSelf tag)
    {
      tag = null;
      foreach(TSelf instance in _tags.Values)
      {
        if(instance.Mask == mask)
        {
          tag = instance;
          return true;
        }
      }
      return false;
    }

    private static void EnsureInitialized()
    {
      if(_initialized) { return; }
      _initialized = true;
      TSelf dummy = CreateInstance();
      Span<string> names = new string[32];
      int count = dummy.GetDefinedNames(names);
      if(count > 32)
      {
        throw new IndexOutOfRangeException($"The Tag type {typeof(TSelf).Name} returned a defined name count larger than 32, this is not allowed.");
      }
      for(int i = 0; i < count && i < 32; i++)
      {
        if(names[i] == null)
        {
          throw new NullReferenceException($"Missing Tag name for type {typeof(TSelf).Name} at index {i}. " +
            $"Count given was {count}, ensure count was correct and there are no gaps in Tag names.");
        }
        string name = names[i];
        TSelf tag = CreateInstance();
        tag.Name = name;
        tag.Index = i;
        tag.Mask = 1u << i;
        AllMask |= tag.Mask;
        _tags[name] = tag;
      }
    }

    private static TSelf CreateInstance() => (TSelf)Activator.CreateInstance(typeof(TSelf), true);

    /// <summary> Gets a string containing the name of this <typeparamref name="TSelf"/> instance. </summary>
    /// <returns> The name of this <typeparamref name="TSelf"/>. </returns>
    public override string ToString() => Name;

    /// <summary>
    /// This method is used to define the implementation's defined names, in the intended order, and then return the number of names.
    /// May not exceed 32 values. It is recommended to create static
    /// <typeparamref name="TSelf"/> properties in the implementation which call <see cref="Get"/> matching each of these names.
    /// </summary>
    /// <param name="names"> The <see cref="Span{T}"/> to fill with defined names. </param>
    /// <returns> The number of defined names. </returns>
    protected abstract int GetDefinedNames(Span<string> names);

  }
}