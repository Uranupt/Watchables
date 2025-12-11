using Microsoft.CodeAnalysis;


namespace Watchables.Analyzers
{
  internal static class Rules
  {

    internal static readonly DiagnosticDescriptor NumericUsage = new(
      id: "WAT001",
      title: "Invalid type argument for IEnforceNumeric<T>",
      messageFormat: $"Type '{0}' is not supported by IEnforceNumeric<T>. Only numeric unmanaged types are allowed.",
      category: "Usage",
      defaultSeverity: DiagnosticSeverity.Error,
      isEnabledByDefault: true
    );

    internal static readonly DiagnosticDescriptor NumericInheritance = new(
      id: "WAT002",
      title: "IEnforceNumeric<T> inheritance not preserved",
      messageFormat: $"Generic Type '{0}' declares an open generic field of Type '{1}' which implements IEnforceNumeric<T>. " +
      $"'{0}' must also implement IEnforceNumeric<T> with a matching Type parameter.",
      category: "Usage",
      defaultSeverity: DiagnosticSeverity.Error,
      isEnabledByDefault: true
    );

    internal static readonly DiagnosticDescriptor NumericMethod = new(
      id: "WAT003",
      title: "IEnforceNumeric<T> Type in generic method.",
      messageFormat: $"The generic method '{0}' in Type '{1}' declares an IEnforceNumeric<T> implenting Type '{2}' with a matching Type parameter. " +
      $"This is not allowed",
      category: "Usage",
      defaultSeverity: DiagnosticSeverity.Error,
      isEnabledByDefault: true
    );

    internal static readonly DiagnosticDescriptor MissingFlags = new(
      id: "WAT004",
      title: "Enum Type not marked with FlagsAttribute",
      messageFormat: $"The Type '{0}' is intended for use with FlagsAttribute marked Enums. The supplied type '{1}' is missing the FlagsAttribute." +
      $" This may result in invalid values and is not recommended.",
      category: "Usage",
      defaultSeverity: DiagnosticSeverity.Warning,
      isEnabledByDefault: true
    );

    internal static readonly DiagnosticDescriptor TagConstuctor = new(
      id: "WAT005",
      title: "Tag Type with public constructor.",
      messageFormat: $"The Type '{0}' derived from Tag<TSelf> contains a public or internal creator. This is not allowed.",
      category: "Usage",
      defaultSeverity: DiagnosticSeverity.Error,
      isEnabledByDefault: true
    );

  }
}