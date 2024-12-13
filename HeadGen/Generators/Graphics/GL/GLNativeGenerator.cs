using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.RegularExpressions;
using HeadGen.Core;
using HeadGen.Core.Headers;
using HeadGen.DataTypes;

namespace HeadGen.Generators.Graphics.GL;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public sealed class GLNativeGenerator : HeaderGenerator
{
    private static readonly char[] NewLineSeparators = ['\n', '\r'];
    
    protected override FrozenDictionary<string, string> KeywordMapping { get; set; } = GraphicsTypeMapping.KeywordMapping;
    protected override FrozenDictionary<string, string> TypeMapping { get; set; } = GraphicsTypeMapping.TypeMapping;

    protected override string Dll => "opengl32.dll";

    protected override GeneratorOptions[] Options =>
    [
        new GeneratorOptions
        {
            Usings = [
              "System",
              "System.Runtime.InteropServices"
            ],
            Namespace = "Hypercube.Graphics.Api.Glfw",
            Name = "GLNative",
            File = "glcorearb.h",
            Modifiers = "public static unsafe partial",
            Type = "class"
        },
    ];
    
    protected override void GenerateContent(StringBuilder result, GeneratorOptions options, string fileContent)
    {
        var lines = fileContent.Split(NewLineSeparators, StringSplitOptions.RemoveEmptyEntries);
        
        foreach (var line in lines)
        {
            TryGenerateFunction(result, line);
        }
    }

    private void TryGenerateFunction(StringBuilder result, string line)
    {
        var match = Regex.Match(line, @"^(GLAPI|extern)\s+([\w\s\*]+)\s+(\w+)\s*\(([^)]*)\);");
        if (!match.Success)
            return;

        var returnType = match.Groups[2].Value;
        var functionName = match.Groups[3].Value;
        var parameters = match.Groups[4].Value;
        
        var function = new Function
        {
            Name = functionName,
            ReturnType = HandleType(returnType),
            Arguments = HandleArguments(parameters),
        };

        result.AppendLine();
        result.AppendLine($"    /// <remarks>");
        result.AppendLine($"    /// <c>");
        result.AppendLine($"    /// {line}");
        result.AppendLine($"    /// </c>");
        result.AppendLine($"    /// </remarks>");
        result.AppendLine($"    [LibraryImport(\"{Dll}\")]");
        result.AppendLine($"    public static partial {function};");
    }
}