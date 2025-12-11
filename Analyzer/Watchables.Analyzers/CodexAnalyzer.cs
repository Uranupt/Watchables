using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace Watchables.Analyzers
{
  [DiagnosticAnalyzer(LanguageNames.CSharp)]
  public sealed class WatchablesAnalyzer : DiagnosticAnalyzer
  {
    private static readonly DiagnosticDescriptor NonNumericEnforceRule = new(
      id: "WATCH001",
      title: "IEnforceNumeric must be closed over numeric types",
      messageFormat: "Type parameter '{0}' for IEnforceNumeric must be a numeric primitive",
      category: "Usage",
      defaultSeverity: DiagnosticSeverity.Error,
      isEnabledByDefault: true
    );

    private static readonly DiagnosticDescriptor MissingEnforceOnContainerRule = new(
      id: "WATCH002",
      title: "Generic type containing numeric enforcers must implement IEnforceNumeric",
      messageFormat: "Type '{0}' contains a field enforcing numeric constraint for '{1}' but does not implement IEnforceNumeric<{1}>",
      category: "Usage",
      defaultSeverity: DiagnosticSeverity.Error,
      isEnabledByDefault: true
    );

    private static readonly DiagnosticDescriptor GenericMethodEnforcerRule = new(
      id: "WATCH003",
      title: "Generic methods cannot expose numeric enforcers",
      messageFormat: "Generic method '{0}' uses type parameter '{1}' with a type implementing IEnforceNumeric",
      category: "Usage",
      defaultSeverity: DiagnosticSeverity.Error,
      isEnabledByDefault: true
    );

    private static readonly DiagnosticDescriptor FlagsWithoutAttributeRule = new(
      id: "WATCH004",
      title: "Flags helpers should be used with [Flags] enums",
      messageFormat: "Enum type '{0}' should be marked with [Flags] when used with '{1}'",
      category: "Usage",
      defaultSeverity: DiagnosticSeverity.Warning,
      isEnabledByDefault: true
    );

    private static readonly DiagnosticDescriptor TagConstructorRule = new(
      id: "WATCH005",
      title: "Tag implementations must not expose public constructors",
      messageFormat: "Tag-derived type '{0}' should not declare public or internal constructors",
      category: "Usage",
      defaultSeverity: DiagnosticSeverity.Error,
      isEnabledByDefault: true
    );

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(
      NonNumericEnforceRule,
      MissingEnforceOnContainerRule,
      GenericMethodEnforcerRule,
      FlagsWithoutAttributeRule,
      TagConstructorRule
    );

    public override void Initialize(AnalysisContext context)
    {
      context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
      context.EnableConcurrentExecution();

      context.RegisterCompilationStartAction(StartAnalysis);
    }

    private static void StartAnalysis(CompilationStartAnalysisContext context)
    {
      INamedTypeSymbol enforceNumericSymbol = context.Compilation.GetTypeByMetadataName("Watchables.IEnforceNumeric`1");
      INamedTypeSymbol tagSymbol = context.Compilation.GetTypeByMetadataName("Watchables.Tag`1");
      INamedTypeSymbol flagRequestableSymbol = context.Compilation.GetTypeByMetadataName("Watchables.FlagRequestable`1");
      INamedTypeSymbol flagsCompositeSymbol = context.Compilation.GetTypeByMetadataName("Watchables.FlagsComposite`1");

      if(enforceNumericSymbol == null && tagSymbol == null && flagRequestableSymbol == null && flagsCompositeSymbol == null)
      {
        return;
      }

      if(enforceNumericSymbol != null || tagSymbol != null)
      {
        context.RegisterSymbolAction(
          c => AnalyzeNamedType(c, enforceNumericSymbol, tagSymbol),
          SymbolKind.NamedType
        );
      }

      if(enforceNumericSymbol != null)
      {
        context.RegisterSymbolAction(c => AnalyzeMethod(c, enforceNumericSymbol), SymbolKind.Method);
      }

      if(flagRequestableSymbol != null || flagsCompositeSymbol != null || enforceNumericSymbol != null)
      {
        context.RegisterSymbolAction(
          c => AnalyzeMember(c, flagRequestableSymbol, flagsCompositeSymbol, enforceNumericSymbol),
          SymbolKind.Field,
          SymbolKind.Property,
          SymbolKind.Parameter
        );

        context.RegisterOperationAction(
          c => AnalyzeVariableDeclarator(c, flagRequestableSymbol, flagsCompositeSymbol, enforceNumericSymbol),
          OperationKind.VariableDeclarator
        );
      }
    }

    private static void AnalyzeNamedType(SymbolAnalysisContext context, INamedTypeSymbol enforceNumericSymbol, INamedTypeSymbol tagSymbol)
    {
      INamedTypeSymbol namedType = (INamedTypeSymbol)context.Symbol;

      if(enforceNumericSymbol != null)
      {
        foreach(INamedTypeSymbol enforce in GetEnforceInterfaces(namedType, enforceNumericSymbol))
        {
          ITypeSymbol argument = enforce.TypeArguments[0];
          if(argument.TypeKind == TypeKind.TypeParameter)
          {
            continue;
          }

          if(!IsNumericPrimitive(argument))
          {
            context.ReportDiagnostic(Diagnostic.Create(
              NonNumericEnforceRule,
              namedType.Locations[0],
              argument.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat)
            ));
          }
        }

        if(namedType.TypeParameters.Length > 0)
        {
          foreach(IFieldSymbol field in namedType.GetMembers().OfType<IFieldSymbol>())
          {
            foreach(ITypeParameterSymbol parameter in namedType.TypeParameters)
            {
              if(ImplementsEnforcerForType(field.Type, parameter, enforceNumericSymbol) && !ImplementsEnforcerForType(namedType, parameter, enforceNumericSymbol))
              {
                context.ReportDiagnostic(Diagnostic.Create(
                  MissingEnforceOnContainerRule,
                  field.Locations[0],
                  namedType.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
                  parameter.Name
                ));
              }
            }
          }
        }
      }

      if(tagSymbol == null || !InheritsFrom(namedType, tagSymbol) || SymbolEqualityComparer.Default.Equals(namedType.OriginalDefinition, tagSymbol))
      {
        return;
      }

      foreach(IMethodSymbol constructor in namedType.Constructors)
      {
        if(constructor.IsStatic)
        {
          continue;
        }

        if(constructor.DeclaredAccessibility is Accessibility.Public or Accessibility.Internal or Accessibility.ProtectedOrInternal)
        {
          context.ReportDiagnostic(Diagnostic.Create(
            TagConstructorRule,
            constructor.Locations.Length > 0 ? constructor.Locations[0] : namedType.Locations[0],
            namedType.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat)
          ));
        }
      }
    }

    private static void AnalyzeMethod(SymbolAnalysisContext context, INamedTypeSymbol enforceNumericSymbol)
    {
      IMethodSymbol method = (IMethodSymbol)context.Symbol;
      if(method.TypeParameters.Length == 0)
      {
        return;
      }

      foreach(ITypeParameterSymbol parameter in method.TypeParameters)
      {
        if(ContainsEnforcer(method.ReturnType, parameter, enforceNumericSymbol))
        {
          ReportGenericMethodDiagnostic(context, method, parameter);
          return;
        }

        foreach(IParameterSymbol param in method.Parameters)
        {
          if(ContainsEnforcer(param.Type, parameter, enforceNumericSymbol))
          {
            ReportGenericMethodDiagnostic(context, method, parameter);
            return;
          }
        }
      }
    }

    private static void AnalyzeMember(SymbolAnalysisContext context, INamedTypeSymbol flagRequestableSymbol, INamedTypeSymbol flagsCompositeSymbol, INamedTypeSymbol enforceNumericSymbol)
    {
      ITypeSymbol type = context.Symbol switch
      {
        IFieldSymbol field => field.Type,
        IPropertySymbol property => property.Type,
        IParameterSymbol parameter => parameter.Type,
        _ => null
      };

      if(type == null)
      {
        return;
      }

      ReportFlagsIfNecessary(context, type, flagRequestableSymbol, flagsCompositeSymbol);

      if(enforceNumericSymbol == null || context.Symbol.Kind != SymbolKind.Parameter)
      {
        return;
      }

      IParameterSymbol parameterSymbol = (IParameterSymbol)context.Symbol;
      if(parameterSymbol.ContainingSymbol is not IMethodSymbol method || method.TypeParameters.Length == 0)
      {
        return;
      }

      foreach(ITypeParameterSymbol parameter in method.TypeParameters)
      {
        if(ContainsEnforcer(type, parameter, enforceNumericSymbol))
        {
          ReportGenericMethodDiagnostic(context, method, parameter);
          return;
        }
      }
    }

    private static void AnalyzeVariableDeclarator(OperationAnalysisContext context, INamedTypeSymbol flagRequestableSymbol, INamedTypeSymbol flagsCompositeSymbol, INamedTypeSymbol enforceNumericSymbol)
    {
      IVariableDeclaratorOperation declarator = (IVariableDeclaratorOperation)context.Operation;
      ILocalSymbol symbol = declarator.Symbol;

      ReportFlagsIfNecessary(context, symbol.Type, flagRequestableSymbol, flagsCompositeSymbol);

      if(enforceNumericSymbol == null || context.ContainingSymbol is not IMethodSymbol method || method.TypeParameters.Length == 0)
      {
        return;
      }

      foreach(ITypeParameterSymbol parameter in method.TypeParameters)
      {
        if(ContainsEnforcer(symbol.Type, parameter, enforceNumericSymbol))
        {
          ReportGenericMethodDiagnostic(context, method, parameter, declarator.Syntax.GetLocation());
          return;
        }
      }
    }

    private static void ReportFlagsIfNecessary(SymbolAnalysisContext context, ITypeSymbol type, INamedTypeSymbol flagRequestableSymbol, INamedTypeSymbol flagsCompositeSymbol)
    {
      Location fallbackLocation = context.Symbol.Locations.Length > 0 ? context.Symbol.Locations[0] : Location.None;
      ReportFlagsIfNecessary(type, flagRequestableSymbol, flagsCompositeSymbol, fallbackLocation, context.ReportDiagnostic);
    }

    private static void ReportFlagsIfNecessary(OperationAnalysisContext context, ITypeSymbol type, INamedTypeSymbol flagRequestableSymbol, INamedTypeSymbol flagsCompositeSymbol)
    {
      ReportFlagsIfNecessary(type, flagRequestableSymbol, flagsCompositeSymbol, context.Operation.Syntax.GetLocation(), context.ReportDiagnostic);
    }

    private static void ReportFlagsIfNecessary(ITypeSymbol type, INamedTypeSymbol flagRequestableSymbol, INamedTypeSymbol flagsCompositeSymbol, Location fallbackLocation, Action<Diagnostic> reportDiagnostic)
    {
      if(type is not INamedTypeSymbol namedType || namedType.IsUnboundGenericType)
      {
        return;
      }

      INamedTypeSymbol constructedFrom = namedType.ConstructedFrom;
      if(!SymbolEqualityComparer.Default.Equals(constructedFrom, flagRequestableSymbol) && !SymbolEqualityComparer.Default.Equals(constructedFrom, flagsCompositeSymbol))
      {
        return;
      }

      ITypeSymbol enumType = namedType.TypeArguments[0];
      if(enumType.TypeKind != TypeKind.Enum)
      {
        return;
      }

      if(!HasFlagsAttribute(enumType))
      {
        string helperName = constructedFrom.Name;
        reportDiagnostic(Diagnostic.Create(
          FlagsWithoutAttributeRule,
          fallbackLocation,
          enumType.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
          helperName
        ));
      }
    }

    private static void ReportGenericMethodDiagnostic(SymbolAnalysisContext context, IMethodSymbol method, ITypeParameterSymbol parameter)
    {
      context.ReportDiagnostic(Diagnostic.Create(
        GenericMethodEnforcerRule,
        method.Locations[0],
        method.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
        parameter.Name
      ));
    }

    private static void ReportGenericMethodDiagnostic(OperationAnalysisContext context, IMethodSymbol method, ITypeParameterSymbol parameter, Location location)
    {
      context.ReportDiagnostic(Diagnostic.Create(
        GenericMethodEnforcerRule,
        location,
        method.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
        parameter.Name
      ));
    }

    private static bool HasFlagsAttribute(ITypeSymbol enumType)
    {
      foreach(AttributeData attribute in enumType.GetAttributes())
      {
        INamedTypeSymbol attributeClass = attribute.AttributeClass;
        if(attributeClass != null && attributeClass.Name == nameof(FlagsAttribute) && attributeClass.ContainingNamespace.ToDisplayString() == typeof(FlagsAttribute).Namespace)
        {
          return true;
        }
      }

      return false;
    }

    private static bool ContainsEnforcer(ITypeSymbol type, ITypeParameterSymbol parameter, INamedTypeSymbol enforceNumericSymbol)
    {
      if(ImplementsEnforcerForType(type, parameter, enforceNumericSymbol))
      {
        return true;
      }

      if(type is INamedTypeSymbol namedType)
      {
        foreach(ITypeSymbol argument in namedType.TypeArguments)
        {
          if(ContainsEnforcer(argument, parameter, enforceNumericSymbol))
          {
            return true;
          }
        }
      }
      else if(type is IArrayTypeSymbol arrayType)
      {
        return ContainsEnforcer(arrayType.ElementType, parameter, enforceNumericSymbol);
      }
      else if(type is IPointerTypeSymbol pointerType)
      {
        return ContainsEnforcer(pointerType.PointedAtType, parameter, enforceNumericSymbol);
      }

      return false;
    }

    private static bool ImplementsEnforcerForType(ITypeSymbol type, ITypeSymbol parameter, INamedTypeSymbol enforceNumericSymbol)
    {
      if(type is INamedTypeSymbol namedType)
      {
        if(SymbolEqualityComparer.Default.Equals(namedType.OriginalDefinition, enforceNumericSymbol) && namedType.TypeArguments.Length == 1)
        {
          return SymbolEqualityComparer.Default.Equals(namedType.TypeArguments[0], parameter);
        }

        foreach(INamedTypeSymbol iface in namedType.AllInterfaces)
        {
          if(SymbolEqualityComparer.Default.Equals(iface.OriginalDefinition, enforceNumericSymbol) && iface.TypeArguments.Length == 1)
          {
            if(SymbolEqualityComparer.Default.Equals(iface.TypeArguments[0], parameter))
            {
              return true;
            }
          }
        }
      }

      return false;
    }

    private static IEnumerable<INamedTypeSymbol> GetEnforceInterfaces(INamedTypeSymbol type, INamedTypeSymbol enforceNumericSymbol)
    {
      if(SymbolEqualityComparer.Default.Equals(type.OriginalDefinition, enforceNumericSymbol))
      {
        yield return type;
      }

      foreach(INamedTypeSymbol iface in type.AllInterfaces)
      {
        if(SymbolEqualityComparer.Default.Equals(iface.OriginalDefinition, enforceNumericSymbol))
        {
          yield return iface;
        }
      }
    }

    private static bool InheritsFrom(INamedTypeSymbol type, INamedTypeSymbol baseType)
    {
      INamedTypeSymbol current = type.BaseType;
      while(current != null)
      {
        if(SymbolEqualityComparer.Default.Equals(current.OriginalDefinition, baseType))
        {
          return true;
        }
        current = current.BaseType;
      }

      return false;
    }

    private static bool IsNumericPrimitive(ITypeSymbol type)
    {
      return type.SpecialType switch
      {
        SpecialType.System_Int16 or 
        SpecialType.System_UInt16 or 
        SpecialType.System_Int32 or 
        SpecialType.System_UInt32 or 
        SpecialType.System_Int64 or 
        SpecialType.System_UInt64 or 
        SpecialType.System_Single or 
        SpecialType.System_Double or 
        SpecialType.System_Decimal => true,
        _ => false
      };
    }
  }
}
