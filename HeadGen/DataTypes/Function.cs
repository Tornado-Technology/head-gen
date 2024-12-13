using JetBrains.Annotations;

namespace HeadGen.DataTypes;

[PublicAPI]
public struct Function
{
    public string Name;
    public Type ReturnType;
    public List<Variable> Arguments;
    
    public string ArgumentsString => $"{string.Join(", ", Arguments)}";

    public override string ToString()
    {
        return $"{ReturnType} {Name}({ArgumentsString})";
    }
}