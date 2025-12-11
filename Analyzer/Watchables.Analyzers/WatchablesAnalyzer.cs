using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Watchables.Analyzers
{
  [DiagnosticAnalyzer(LanguageNames.CSharp)]
  public class WatchablesAnalyzer : DiagnosticAnalyzer
  {

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
    {
      get
      {
        return ImmutableArray.Create(
          Rules.NumericUsage,
          Rules.NumericInheritance,
          Rules.NumericMethod,
          Rules.MissingFlags,
          Rules.TagConstuctor
        );
      }
    }

    public override void Initialize(AnalysisContext context)
    {
      context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
      context.EnableConcurrentExecution();

      //context.RegisterSyntaxNodeAction(AnalyzeGenericTypeUsage, SyntaxKind.ObjectCreationExpression);
      //context.RegisterSyntaxNodeAction(AnalyzeGenericTypeUsage, SyntaxKind.GenericName);
      context.RegisterSymbolAction(AnalyzeType, SymbolKind.NamedType);
      context.RegisterSymbolAction(AnalyzeMethod, SymbolKind.Method);

      context.RegisterSymbolAction(AnalyzeNamedTypeSymbol, SymbolKind.NamedType);
    }

    private static void AnalyzeGenericTypeUsage(SyntaxNodeAnalysisContext context)
    {
      if(context.Node is not TypeSyntax typeSyntax) { return; }
      if(context.SemanticModel.GetSymbolInfo(typeSyntax).Symbol is not INamedTypeSymbol typeSymbol) { return; }

    }

    private static void AnalyzeNamedTypeSymbol(SymbolAnalysisContext context)
    {
      if(context.Symbol is not INamedTypeSymbol symbol || !symbol.IsGenericType) { return; }

      string baseName = symbol.OriginalDefinition.Name;
      if(baseName != "FlagsComposite" && baseName != "FlagsRequestable") { return; }

      ITypeSymbol enumType = symbol.TypeArguments[0];
      if(enumType.GetAttributes().Any(a => a.AttributeClass?.Name == "FlagsAttribute")) { return; }

      Diagnostic diagnostic = Diagnostic.Create(FlagsRule, symbol.Locations[0], baseName, enumType.Name);
      context.ReportDiagnostic(diagnostic);
    }

    private static void AnalyzeType(SymbolAnalysisContext context)
    {
      INamedTypeSymbol symbol = context.Symbol as INamedTypeSymbol;

    }

    private static void AnalyzeMethod(SymbolAnalysisContext context)
    {

    }

    private static void CheckNumericUsage(SymbolAnalysisContext context)
    {
      INamedTypeSymbol symbol = context.Symbol as INamedTypeSymbol;
      IEnumerable<INamedTypeSymbol> namedInterfaces = symbol.AllInterfaces
        .Where(i => i.Name == "IEnforceNumeric" && i.TypeArguments.Length == 1);
      foreach(INamedTypeSymbol type in namedInterfaces)
      {
        if(!IsNumeric(type.TypeArguments[0]))
        {
          Diagnostic diagnostic = Diagnostic.Create(Rules.NumericUsage, typeSyntax.GetLocation(), type.TypeArguments[0].Name);
          context.ReportDiagnostic(diagnostic);
        }
      }
    }

    private static void CheckNumericInheritance(SymbolAnalysisContext context)
    {

    }

    private static bool IsNumeric(ITypeSymbol type)
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
