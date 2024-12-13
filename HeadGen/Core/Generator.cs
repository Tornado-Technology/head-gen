using System.Text;
using JetBrains.Annotations;

namespace HeadGen.Core;

[PublicAPI]
public abstract class Generator : Generator<GeneratorOptions>;

[PublicAPI]
public abstract class Generator<T> where T : GeneratorOptions
{
    protected virtual T[] Options { get; } = [];
 
    public void Execute()
    {
        foreach (var option in Options)
        {
            var source = GenerateSource(option);
            File.WriteAllText(option.SourcePath, source);
        }
    }

    /// <summary>
    /// Creates one file with the specified settings and adds it to the sources.
    /// </summary>
    protected virtual string GenerateSource(T options)
    {
        var fileContent = GetSource(options.File);
        var content = new StringBuilder();
        
        GenerateHeader(content, options, fileContent);
        GenerateUsings(content, options, fileContent);
        GenerateNamespace(content, options, fileContent);
        GenerateBody(content, options, fileContent);

        return content.ToString();
    }

    protected virtual void GenerateContent(StringBuilder result, T options, string fileContent)
    {
        
    }

    protected string GetSource(string file)
    {
        if (!File.Exists(file))
        {
            Console.WriteLine($"File {file} not exists!");
            return string.Empty;
        }
        
        return File.ReadAllText(file);
    }

    private void GenerateHeader(StringBuilder result, T options, string fileContent)
    {
        foreach (var line in options.Header)
        {
            var comment = line;
            comment = comment.Replace("${file}", options.File);
            comment = comment.Replace("${path}", options.SourcePath);
            
            result.AppendLine($"// {comment}");
        }

        result.AppendLine();
    }

    private void GenerateUsings(StringBuilder result, T options, string fileContent)
    {
        foreach (var line in options.Usings)
        {
            result.AppendLine($"using {line};");
        }

        result.AppendLine();
    }

    private void GenerateNamespace(StringBuilder result, T options, string fileContent)
    {
        result.AppendLine($"namespace {options.Namespace};");
        result.AppendLine();
    }

    private void GenerateBody(StringBuilder result, T options, string fileContent)
    {
        result.AppendLine($"{options.Modifiers} {options.Type} {options.Name}");
        result.AppendLine("{");
        
        GenerateContent(result, options, fileContent);
        
        result.AppendLine("}");
    }

    protected static string UpperSnakeCaseToPascalCase(string source)
    {
        if (string.IsNullOrWhiteSpace(source))
            return string.Empty;
        
        var parts = source.Split('_', StringSplitOptions.RemoveEmptyEntries);
        for (var i = 0; i < parts.Length; i++)
        {
            parts[i] = char.ToUpper(parts[i][0]) + parts[i][1..].ToLowerInvariant();
        }
        
        return string.Concat(parts);
    }
}