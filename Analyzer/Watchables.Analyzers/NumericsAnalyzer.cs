using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Generic;
using System.Collections.Immutable;


namespace Watchables.Analyzers
{
  [DiagnosticAnalyzer(LanguageNames.CSharp)]
  public class NumericsAnalyzer : DiagnosticAnalyzer
  {

    private static readonly DiagnosticDescriptor ClosedTypeRule = new(
      id: "WAT001",
      title: "Invalid type argument for IEnforceNumeric",
      messageFormat: "Type '{0}' is not supported by IEnforceNumeric. Only uint, ulong, int, long, float, double, and decimal are allowed.",
      category: "Usage",
      defaultSeverity: DiagnosticSeverity.Error,
      isEnabledByDefault: true
    );

    private static readonly DiagnosticDescriptor InheritanceRule = new(
      id: "WAT002",
      title: "IEnforceNumeric inheritance not preserved",
      messageFormat: "Open generic Type '{0}' implements IEnforceNumeric. Containing Type '{1}' must also implement IEnforceNumeric with a matching Type parameter.",
      category: "Usage",
      defaultSeverity: DiagnosticSeverity.Error,
      isEnabledByDefault: true
    );

    private static readonly DiagnosticDescriptor GenericMethodRule = new(
      id: "WAT003",
      title: "IEnforceNumeric Type in generic method",
      messageFormat: "The Type '{0}' implements IEnforceNumeric and is sharing a Type parameter with its enclosing method '{1}'. This is not allowed.",
      category: "Usage",
      defaultSeverity: DiagnosticSeverity.Error,
      isEnabledByDefault: true
    );

    private static readonly SymbolDisplayFormat MethodNameFormat = new(
      typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypes,
      genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
      memberOptions: SymbolDisplayMemberOptions.None
    );

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(ClosedTypeRule, InheritanceRule, GenericMethodRule);

    public override void Initialize(AnalysisContext context)
    {
      context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
      context.EnableConcurrentExecution();
      context.RegisterCompilationStartAction(StartAnalysis);
    }

    private static void StartAnalysis(CompilationStartAnalysisContext context)
    {
      INamedTypeSymbol interfaceSymbol = context.Compilation.GetTypeByMetadataName("Watchables.IEnforceNumeric`1");
      if(interfaceSymbol == null) { return; }
      context.RegisterSyntaxNodeAction(c => AnalyzeGenericUsage(c, interfaceSymbol), SyntaxKind.GenericName);
      context.RegisterOperationAction(c => AnalyzeCreation(c, interfaceSymbol), OperationKind.ObjectCreation);
    }

    private static void AnalyzeGenericUsage(SyntaxNodeAnalysisContext context, INamedTypeSymbol interfaceSymbol)
    {
      GenericNameSyntax genericName = context.Node as GenericNameSyntax;
      if(context.SemanticModel.GetTypeInfo(genericName).Type is not INamedTypeSymbol namedType 
        || namedType is IErrorTypeSymbol
        || !RequiresEnforcement(namedType, interfaceSymbol))
      { 
        return; 
      }

      if(CheckClosedEnforcement(namedType, interfaceSymbol, out ITypeSymbol invalidType))
      {
        context.ReportDiagnostic(Diagnostic.Create(
          ClosedTypeRule,
          context.Node.GetLocation(),
          invalidType.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat)
        ));
      }

      foreach(SyntaxNode node in context.Node.AncestorsAndSelf())
      {
        if(node == context.Node || node == null) { continue; }

        if(node is MethodDeclarationSyntax encMethodDecl
          && context.SemanticModel.GetDeclaredSymbol(encMethodDecl) is IMethodSymbol encMethod
          && CheckMethodTypeEnforced(namedType, encMethod, interfaceSymbol))
        {
          context.ReportDiagnostic(Diagnostic.Create(
            GenericMethodRule,
            context.Node.GetLocation(),
            namedType.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
            encMethod.ToDisplayString(MethodNameFormat)
          ));
          break;
        }

        if(node is TypeDeclarationSyntax encTypeDecl
          && context.SemanticModel.GetDeclaredSymbol(encTypeDecl) is INamedTypeSymbol encType
          && CheckEnforcementPropagation(namedType, encType, interfaceSymbol))
        {
          context.ReportDiagnostic(Diagnostic.Create(
            InheritanceRule,
            context.Node.GetLocation(),
            namedType.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
            encType.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat)
          ));
          break;
        }
      }
      
    }

    private static void AnalyzeCreation(OperationAnalysisContext context, INamedTypeSymbol interfaceSymbol)
    {
      if(context.Operation.Type is not INamedTypeSymbol type) { return; } 
      if(CheckClosedEnforcement(type, interfaceSymbol, out ITypeSymbol invalidType))
      {
        context.ReportDiagnostic(Diagnostic.Create(
          ClosedTypeRule,
          context.Operation.Syntax.GetLocation(),
          invalidType.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat)
        ));
      }
    }

    private static bool CheckEnforcementPropagation(INamedTypeSymbol type, INamedTypeSymbol containing, INamedTypeSymbol interfaceSymbol)
    {
      if(!containing.IsGenericType) { return false; }
      foreach(ITypeParameterSymbol parameter in containing.TypeParameters)
      {
        if(CheckEnforcementMatchingParameter(type, parameter, interfaceSymbol)
          && !CheckEnforcementMatchingParameter(containing, parameter, interfaceSymbol))
        {
          return true;
        }
      }
      return false;
    }

    private static bool CheckMethodTypeEnforced(INamedTypeSymbol type, IMethodSymbol method, INamedTypeSymbol interfaceSymbol)
    {
      if(!method.IsGenericMethod) { return false; }
      foreach(ITypeParameterSymbol parameter in method.TypeParameters)
      {
        if(CheckEnforcementMatchingParameter(type, parameter, interfaceSymbol))
        {
          return true;
        }
      }
      return false;
    }

    private static bool CheckClosedEnforcement(INamedTypeSymbol type, INamedTypeSymbol interfaceSymbol, out ITypeSymbol invalidType)
    {
      foreach(INamedTypeSymbol iface in GetEnforcementInterfaces(type, interfaceSymbol, true))
      {
        if(!IsValidNumeric(iface.TypeArguments[0]))
        {
          invalidType = iface.TypeArguments[0];
          return true;
        }
      }
      invalidType = null;
      return false;
    }

    private static IEnumerable<INamedTypeSymbol> GetEnforcementInterfaces(INamedTypeSymbol type, INamedTypeSymbol interfaceSymbol, bool isClosed)
    {
      foreach(INamedTypeSymbol iface in GetEnforcementInterfaces(type, interfaceSymbol))
      {
        bool targeted = iface.TypeArguments[0].TypeKind == TypeKind.TypeParameter;
        if(isClosed)
        {
          targeted = !targeted;
        }
        if(targeted)
        {
          yield return iface;
        }
      }
    }

    private static IEnumerable<INamedTypeSymbol> GetEnforcementInterfaces(INamedTypeSymbol type, INamedTypeSymbol interfaceSymbol)
    {
      if(SymbolEqualityComparer.Default.Equals(type.OriginalDefinition, interfaceSymbol))
      {
        yield return type;
      }
      foreach(INamedTypeSymbol iface in type.AllInterfaces)
      {
        if(SymbolEqualityComparer.Default.Equals(iface.OriginalDefinition, interfaceSymbol))
        {
          yield return iface;
        }
      }
    }

    private static bool CheckEnforcementMatchingParameter(INamedTypeSymbol type, ITypeParameterSymbol parameter, INamedTypeSymbol interfaceSymbol)
    {
      foreach(INamedTypeSymbol iface in GetEnforcementInterfaces(type, interfaceSymbol, false))
      {
        if(SymbolEqualityComparer.Default.Equals(iface.TypeArguments[0], parameter))
        {
          return true;
        }
      }
      return false;
    }

    private static bool RequiresEnforcement(INamedTypeSymbol type, INamedTypeSymbol interfaceSymbol)
    {
      if(SymbolEqualityComparer.Default.Equals(type.OriginalDefinition, interfaceSymbol))
      {
        return true;
      }
      foreach(INamedTypeSymbol iface in type.AllInterfaces)
      {
        if(SymbolEqualityComparer.Default.Equals(iface.OriginalDefinition, interfaceSymbol))
        {
          return true;
        }
      }
      return false;
    }

    private static bool IsValidNumeric(ITypeSymbol type)
    {
      return type.SpecialType switch
      {
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
