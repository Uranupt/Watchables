using System;
using System.Collections.Generic;


namespace Watchables
{
  public abstract class Tag<TSelf> where TSelf : Tag<TSelf>
  {

    private static readonly Dictionary<string, TSelf> _tags = new();
    private static bool _initialized;

    public static IReadOnlyCollection<TSelf> AllTags => GetAllTags();
    public static IReadOnlyCollection<string> AllNames => GetAllNames();
    public static int AllMask { get; private set; } = 0;

    public string Name { get; private set; }
    public int Index { get; private set; }
    public int Mask { get; private set; }

    public static TSelf Get(string name)
    {
      if(!TryGet(name, out TSelf tag))
      {
        throw new KeyNotFoundException($"The Tag type {typeof(TSelf).Name} has no tag with name '{name}'.");
      }
      return tag;
    }

    public static bool TryGet(string name, out TSelf tag)
    {
      EnsureInitialized();
      return _tags.TryGetValue(name, out tag);
    }

    public static TSelf FromIndex(int index)
    {
      if(!TryGetFromIndex(index, out TSelf tag))
      {
        throw new KeyNotFoundException($"The Tag type {typeof(TSelf).Name} has no tag at index {index}.");
      }
      return tag;
    }

    public static bool TryGetFromIndex(int index, out TSelf tag)
    {
      EnsureInitialized();
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

    public static TSelf FromMask(int mask)
    {
      if(!TryGetFromMask(mask, out TSelf tag))
      {
        throw new KeyNotFoundException($"The Tag type {typeof(TSelf).Name} has no tag with mask value {mask}.");
      }
      return tag;
    }

    public static bool TryGetFromMask(int mask, out TSelf tag)
    {
      EnsureInitialized();
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
      TSelf dummy = (TSelf)Activator.CreateInstance(typeof(TSelf), true);
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
        TSelf tag = dummy.Create();
        tag.Name = name;
        tag.Index = i;
        tag.Mask = 1 << i;
        AllMask |= tag.Mask;
        _tags[name] = tag;
      }
    }

    private static IReadOnlyCollection<TSelf> GetAllTags()
    {
      EnsureInitialized();
      return _tags.Values;
    }

    private static IReadOnlyCollection<string> GetAllNames()
    {
      EnsureInitialized();
      return _tags.Keys;
    }

    public override string ToString() => Name;

    protected abstract int GetDefinedNames(Span<string> names);
    protected abstract TSelf Create();

  }
}