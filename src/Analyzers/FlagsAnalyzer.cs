using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Linq;



namespace Watchables.Analyzers
{
  [DiagnosticAnalyzer(LanguageNames.CSharp)]
  public class FlagsAnalyzer : DiagnosticAnalyzer
  {

    private class FlagsTypes
    {
      public readonly INamedTypeSymbol FlagsComposite;
      public readonly INamedTypeSymbol FlagsRequestable;
      public readonly INamedTypeSymbol FlagsAttribute;

      public FlagsTypes(INamedTypeSymbol flagsComposite, INamedTypeSymbol flagsRequestable, 
        INamedTypeSymbol flagsAttribute)
      {
        FlagsComposite = flagsComposite;
        FlagsRequestable = flagsRequestable;
        FlagsAttribute = flagsAttribute;
      }
    }

    private static readonly DiagnosticDescriptor MissingFlagsRule = new(
      id: "WAT004",
      title: "Enum Type not marked with FlagsAttribute",
      messageFormat: "The Type '{0}' is intended for use with FlagsAttribute marked Enums. The supplied type '{1}' is missing the FlagsAttribute." +
      " This may result in invalid values and is not recommended.",
      category: "Usage",
      defaultSeverity: DiagnosticSeverity.Warning,
      isEnabledByDefault: true
    );

    private static readonly DiagnosticDescriptor TagConstructorRule = new(
      id: "WAT005",
      title: "Tag Type with visible constructor",
      messageFormat: "The Type '{0}' derived from Tag<TSelf> contains a creator which is not private or protected. This is not allowed.",
      category: "Usage",
      defaultSeverity: DiagnosticSeverity.Error,
      isEnabledByDefault: true
    );

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create( MissingFlagsRule, TagConstructorRule );

    public override void Initialize(AnalysisContext context)
    {
      context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
      context.EnableConcurrentExecution();
      context.RegisterCompilationStartAction(StartAnalysis);
    }

    private static void StartAnalysis(CompilationStartAnalysisContext context)
    {
      INamedTypeSymbol flagsRequestable = context.Compilation.GetTypeByMetadataName("Watchables.FlagsRequestable`1");
      INamedTypeSymbol flagsComposite = context.Compilation.GetTypeByMetadataName("Watchables.FlagsComposite`1");
      INamedTypeSymbol tag = context.Compilation.GetTypeByMetadataName("Watchables.Tag`1");
      INamedTypeSymbol flagsAttribute = context.Compilation.GetTypeByMetadataName("System.FlagsAttribute");
      if(flagsRequestable != null && flagsComposite != null && flagsAttribute != null)
      {
        FlagsTypes flagTypes = new(flagsComposite, flagsRequestable, flagsAttribute);
        context.RegisterSyntaxNodeAction(c => AnalyzeGenericUsage(c, flagTypes), SyntaxKind.GenericName);
      }
      if(tag != null)
      {
        context.RegisterSymbolAction(c => AnalyzeType(c, tag), SymbolKind.NamedType);
      }
    }

    private static void AnalyzeGenericUsage(SyntaxNodeAnalysisContext context, FlagsTypes flagsTypes)
    {
      GenericNameSyntax genericName = context.Node as GenericNameSyntax;
      if(context.SemanticModel.GetTypeInfo(genericName).Type is not INamedTypeSymbol namedType
        || namedType is IErrorTypeSymbol)
      {
        return;
      }
      if(RequiresFlagsWarning(namedType, flagsTypes, out ITypeSymbol enumType))
      {
        context.ReportDiagnostic(Diagnostic.Create(
          MissingFlagsRule,
          context.Node.GetLocation(),
          namedType.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
          enumType.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat)
        ));
      }
    }

    private static void AnalyzeType(SymbolAnalysisContext context, INamedTypeSymbol tagSymbol)
    {
      INamedTypeSymbol namedType = context.Symbol as INamedTypeSymbol;
      if(!InheritsFrom(namedType, tagSymbol, false)) { return; }
      foreach(IMethodSymbol constructor in namedType.Constructors)
      {
        if(constructor.DeclaredAccessibility is not (Accessibility.Private or Accessibility.Protected))
        {
          context.ReportDiagnostic(Diagnostic.Create(
            TagConstructorRule,
            constructor.Locations.FirstOrDefault(),
            namedType.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat)
          ));
        }
      }
    }

    private static bool RequiresFlagsWarning(INamedTypeSymbol type, FlagsTypes flagsTypes, out ITypeSymbol enumType)
    {
      if(!Compare(type.OriginalDefinition, flagsTypes.FlagsRequestable) && !Compare(type.OriginalDefinition, flagsTypes.FlagsComposite))
      {
        enumType = null;
        return false;
      }
      enumType = type.TypeArguments[0];
      if(enumType.TypeKind != TypeKind.Enum)
      {
        enumType = null;
        return false;
      }
      foreach(AttributeData attribute in enumType.GetAttributes())
      {
        INamedTypeSymbol attrSymbol = attribute.AttributeClass;
        if(Compare(attrSymbol.OriginalDefinition, flagsTypes.FlagsAttribute))
        {
          enumType = null;
          return false;
        }
      }
      return true;
    }

    private static bool InheritsFrom(INamedTypeSymbol type, INamedTypeSymbol baseType, bool acceptIsExactly)
    {
      if(Compare(type.OriginalDefinition, baseType))
      {
        return acceptIsExactly;
      }
      INamedTypeSymbol current = type.BaseType;
      while(current != null)
      {
        if(Compare(current.OriginalDefinition, baseType))
        {
          return true;
        }
        current = current.BaseType;
      }
      return false;
    }

    private static bool Compare(ISymbol x, ISymbol y)
    {
      return SymbolEqualityComparer.Default.Equals(x, y);
    }

  }
}
