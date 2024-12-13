using JetBrains.Annotations;

namespace HeadGen.DataTypes;

[PublicAPI]
public struct Variable
{
    public string Name;
    public Type Type;
    
    public override string ToString()
    {
        return $"{Type} {Name}";
    }
}