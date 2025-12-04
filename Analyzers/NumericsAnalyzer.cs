using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;

namespace Watchables.Analyzer
{
  [DiagnosticAnalyzer(LanguageNames.CSharp)]
  public class NumericsAnalyzer : DiagnosticAnalyzer
  {

    private static readonly DiagnosticDescriptor NumericRule = new DiagnosticDescriptor(
      id: "WAT001",
      title: "Invalid type argument for IEnforceNumeric<T>",
      messageFormat: $"Type '{0}' is not supported by IEnforceNumeric<T>. Only numeric unmanaged types are allowed.",
      category: "Usage",
      defaultSeverity: DiagnosticSeverity.Error, 
      isEnabledByDefault: true
    );

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(NumericRule); } }

    public override void Initialize(AnalysisContext context)
    {
      context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
      context.EnableConcurrentExecution();

      context.RegisterSyntaxNodeAction(AnalyzeGenericTypeUsage, SyntaxKind.ObjectCreationExpression);
      context.RegisterSyntaxNodeAction(AnalyzeGenericTypeUsage, SyntaxKind.GenericName);
    }

    private static void AnalyzeGenericTypeUsage(SyntaxNodeAnalysisContext context)
    {
      if(context.Node is not TypeSyntax typeSyntax) { return; }
      if(context.SemanticModel.GetSymbolInfo(typeSyntax).Symbol is not INamedTypeSymbol typeSymbol) { return; }
      IEnumerable<INamedTypeSymbol> namedInterfaces = typeSymbol.AllInterfaces
        .Where(i => i.Name == "IEnforceNumeric" && i.TypeArguments.Length == 1);
      foreach(INamedTypeSymbol type in namedInterfaces)
      {
        if (!IsNumeric(type.TypeArguments[0]))
        {
          var diagnostic = Diagnostic.Create(NumericRule, typeSyntax.GetLocation(), type.TypeArguments[0].Name);
          context.ReportDiagnostic(diagnostic);
        }
      }
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
