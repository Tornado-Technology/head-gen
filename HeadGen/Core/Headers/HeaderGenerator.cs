using System.Collections.Frozen;
using System.Text;
using System.Text.RegularExpressions;
using  HeadGen.DataTypes;
using JetBrains.Annotations;

using Type = HeadGen.DataTypes.Type;

namespace HeadGen.Core.Headers;

[PublicAPI]
public abstract class HeaderGenerator : Generator
{
    protected virtual FrozenDictionary<string, string> KeywordMapping { get; set; } = new Dictionary<string, string>().ToFrozenDictionary();
    protected virtual FrozenDictionary<string, string> TypeMapping { get; set; } = new Dictionary<string, string>().ToFrozenDictionary();
    
    protected abstract string Dll { get; }

    protected Regex ArrayRegex = new(@"\[\d*\]");
    
    protected List<Variable> HandleArguments(string arguments)
    {
        if (string.IsNullOrWhiteSpace(arguments) || arguments == "void")
            return [];

        var @params = arguments.Split(',');
        var result = new List<Variable>();

        foreach (var param in @params)
        {
            var variable = new Variable();
            
            // Remove all const from types
            // Because const don't work same in C#
            var parts = param
                .Replace("const", "")
                .Trim()
                .Split(' ')
                .ToList();

            variable.Type.Name = parts[0];
            
            // Type like "unsigned int"
            if (parts.Count > 2)
            {
                variable.Type.Name += $" {parts[1]}";
                parts.RemoveAt(1);
            }
            
            // Handling param type
            variable.Name = parts.Count > 1 ? parts[1] : "param";
            
            if (ArrayRegex.IsMatch(variable.Name))
            {
                variable.Type.Array = true;
                variable.Name = ArrayRegex.Replace(variable.Name, "");
            }
            
            // Handling pointer types
            variable.Type.PointerLevel = variable.Name.Count(c => c == '*');

            if (variable.Type.PointerLevel > 0)
                variable.Name = variable.Name.Replace("*", "");

            variable.Name = HandleReservedKeywords(variable.Name);
            variable.Type.Name = HandleTypes(variable.Type.Name);
            
            result.Add(variable);
        }

        return result;
    }
    
    protected virtual Type HandleType(string source)
    {
        var type = new Type
        {
            Name = source,
            PointerLevel = source.Count(c => c == '*')
        };

        if (type.PointerLevel > 0)
            type.Name = type.Name.Replace("*", "");

        type.Array = type.Name.EndsWith("[]");
        if (type.Array)
            type.Name = type.Name[..^2];
        
        type.Name = HandleTypes(type.Name);
        
        return type;
    }

    protected virtual string HandleTypes(string type)
    {
        type = type.Replace("const", "").Trim();
        return TypeMapping.GetValueOrDefault(type, "nint");
    }
    
    protected virtual string HandleReservedKeywords(string name)
    {
        return KeywordMapping.GetValueOrDefault(name, name);
    }
}