using HeadGen.Generators.Graphics.GL;

namespace HeadGen;

public class Program
{
    public static void Main(string[] args)
    {
        var generator = new GLNativeGenerator();
        generator.Execute();
    }
}