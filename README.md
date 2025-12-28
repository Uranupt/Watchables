# Watchables
Watchables is a C# library providing a framework of observable value types with explicit ownership and mutation semantics. Designed with Unity in mind, but usable with any .NET project.

## Compatibility
- .NET Standard 2.1+
- C# 9.0 or later
- Unity Version 2021.3 LTS or later

## Installation

### Standard .NET
1. Download the contents of the release branch.
2. Extract and place the contents somewhere accessible to your project.
3. Add the following includes to your `.csproj` file, supplying relevant local path
```xml
<ItemGroup>
  <Reference Include="Watchables">
    <HintPath>{PATH}/dist/Watchables.dll</HintPath>
  </Reference>

  <Analyzer Include="{PATH}/dist/Watchables.Analyzers.dll"/>
</ItemGroup>
```

### Unity
1. Download the contents of the release branch and extract them.
2. Place `Watchables.dll` and `Watchables.xml` **together** anywhere in the Assets folder, but usually in Assets/Plugins
3. Place `Watchables.Analyzers.dll` in Assets/RoslynAnalyzers

**Note:** Analyzers will not show up in Unity Editor console, only your IDE.

## Usage

### Interfaces
The Watchables system is built on a series of interfaces that lock together to create modular or expansive implementations.

- `IOwnable` provides ownership semantics through `IsOwned`, `SetOwner`, `ClearOwner`, and `CompareToOwner`
- `ISealable` extends `IOwnable` to add mutation sealing semantics with `IsSealed` and `SetSealed`
- `IWrapper<T>` provides access to a value of Type `T` through the property `Value`
- `IWatchable` extends `IOwnable` and adds core update and lifetime semantics with the `Changed` and `Destroyed` events, as well as `IsDestroyed` and `Destroy`
- `IWatchable<T>` combines `IWatchable` and `IWrapper<T>`
- `IAsReadOnly<T>` extends `IWatchable<T>` and adds the `AsReadOnly` method to retrieve a `ReadOnlyWatchable<T>` mirroring the instance. (see below)

### Basics
Core, simple implementations

- `BasicWrapper<T>` and `BasicWatchable<T>` allow direct mutation of their values through `SetValue`
- `ReadOnlyWrapper<T>` wraps a value with no method to mutate. Useful for passing raw values into the Watchable system
- `ReadOnlyWatchable<T>` wraps another `IWatchable<T>` instance, mirroring its updates, values, and lifetime without exposing mutation
- `ConversionWrapper<TSource, TValue>` and `ConversionWatchable<TSource, TValue>` allow conversion between any `IConvertible` Types
- `Requestable` allows objects to 'request' an inversion of the default `bool` state provided at construction.
- `StringChain` builds a `string` from provided inputs in a set order. Will automatically update from any provided `IWatchable` inputs
- `FormatStringWatchable` builds a `string` from a provided format string and an array of inputs. Will automatically update from any provided `IWatchable` inputs

**Tip:** You can easily wrap any value with the extension methods `Wrap` and `WrapMutable` called on the value, returning a new `ReadOnlyWrapper<T>` or `BasicWrapper<T>` respectively.

### Collections
Watchable versions of C# collection Types, built with abstraction in mind, split between directly mutable and composite versions.

- `WatchableCollection` is the base class for all collections, implements `ISealable` and `IEnumerable`
- `WatchableCollection<T>` is the base class for directly mutable collections, implements `IEnumerable<T>`
- `CompositeCollection<T>` is the base class for composite collections (those built from the contents of other `IEnumerable<T>` instances), implements `IEnumerable<T>`

Currently supports the following collection Types, with directly mutable and composite implementations:

- `IWatchableHashSet<T>`
- `IWatchableList<T>`
- `IWatchableDictionary<TKey, TValue>`

### Flags and Tags
Implementations for use with Flags Enums and enum-analogous Tags.

- `Tag<TSelf>` is a base class you can use to create a 32-bit enum alternative. Derive it in a new Type and define your value names with the `GetDefinedNames` method
- `Tags<T>` provides Flags-like behavior for a `Tag<T>` Type, as well as operators such as `+` and `-` to easily assign and mutate value
- `FlagsRequestable<T>` and `TagsRequestable<T>` allow objects to 'request' specific values to be added to a `FlagsAttribute` marked enum or `Tags<T>` value. Value will always be the combination of all requested values
- `FlagsComposite<T>` and `TagsComposite<T>` allow for more precise targeting of value setting (on or off) using composite parts instead of requests. For more information, see the Composites section

### Numerics
Implementations for performing operations on numeric values. Uses `IEnforceNumeric<T>` to ensure only handled Types are declared. Currently handled Types are: `uint`, `ulong`, `int`, `long`, `float`, `double`, `decimal`.

- `OperationWatchable<T>` performs a single operation on a base value. Must be constructed via `OperationBuilder.Watchable` to enforce operations types, such as preventing negating an unsigned integer
- `OperationChain<T>` performs a series of operations defined by `OperationStep<T>` in a set order against a base value. `OperationStep<T>` is constructed from `OperationBuilder.Step`
- `ClampedWatchable<T>` wraps an `OperationChain<T>` that bounds an internal value between a defined `Minimum` and `Maximum`. Allows direct mutation of the value through `Add`, `Subtract`, `Set`, `Fill`, and `Empty`
- `CompositeWatchable<T>` allows for composing a numeric value via a collection of `CompositePart<T>`. Sorts by `CompositeOperation`, always setting value, then translating, then scaling, then clamping. For more information, see the Composites section
- `CompositeLibrary<TKey, TValue>` is a signature shortening version of `CompositeLibrary<TKey, TComp, TPart>` for `CompositeWatchable<T>`. For more information, see the Composite section

### Composites
Composites are abstract bases for a pattern of Composite and Part Type pairs, where the Composite value is defined by the Parts it contains.

- `CompositePartBase<TSelf>` and `CompositePartBase<TValue, TSelf>` are the unvalued and valued base classes for the Part half of the Type pair. They are not `IWatchable` themselves, but the `ValueSource` of the valued version can be, and `CompositeBase<TValue, TPart>` accounts for this, updating or removing automatically
- `CompositeBase<TPart>` and `CompositeBase<TValue, TPart>` are the unvalued and valued base classes for the Composite half of the Type pair. You must define sorting behavior yourself via `Sort` when you derive, including if you want to respect the `CompositePartPriority` values of the Parts
- `CompositeLibrary<TKey, TComp, TPart>` is a collection of lazy-created `TComp` Type composites, stored via `TKey`. You cannot directly add to or remove from the library, only adding / removing `TPart` instances, or calling `Clear` to reset the library

### Custom Types
The Watchables system is built to be extensible, flexible, and easy to abstract. In addition to the core interfaces, there are some base classes that provide common implementation which you can use to create your own custom Types.

- `OwnableBase` provides the common implementation for `IOwnable`
- `WatchableBase` extends `OwnableBase` and provides common implementation for `IWatchable`
- `WatchableBase<T>` extends `WatchableBase` and provides common implementation for `IWatchable<T>` and `IAsReadOnly<T>`
- `NestedWatchable<T>` extends `WatchableBase<T>` and provides helpful methods and logic for Types which base their values on other `IWatchable` instances
- `SealableBase` extends `OwnableBase` and provides common implementation for `ISealable`
- `SealableNestedBase<T>` extends `NestedWatchable<T>` and provides implementation for `ISealable`

## License
[MIT](https://choosealicense.com/licenses/mit/)

## Project Future
I don't currently have time to make major updates to the project, but will likely add periodic small tweaks and updates when something occurs to me. When / if I have the time, there's some features I'm interested in adding, such as:
- Debouncing layers
- Async framework
- Lazy nested types (won't listen for or supply updates, only evaluated on retrieval)
- Debuging tools
- Serialization tools

Project is entirely open source, I'm not possesive of it; use it, change it, redistribute it as you like.
