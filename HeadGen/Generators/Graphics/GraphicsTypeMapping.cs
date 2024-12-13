using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;

namespace HeadGen.Generators.Graphics;

[SuppressMessage("ReSharper", "StringLiteralTypo")]
public class GraphicsTypeMapping
{
    public static readonly FrozenDictionary<string, string> NameMapping = new Dictionary<string, string>
    {
        // Callbacks
        { "Glproc", "GLProcess" },
        { "Vkproc", "VKProcess" },
        { "Windowpos", "WindowPosition" },
        { "Windowsize", "WindowSize" },
        { "Windowclose", "WindowClose" },
        { "Windowrefresh", "WindowRefresh" },
        { "Windowfocus", "WindowFocus" },
        { "Windowiconify", "WindowIconify" },
        { "Windowmaximize", "WindowMaximize" },
        { "Framebuffersize", "FrameBufferSize" },
        { "Windowcontentscale", "WindowContentScale" },
        { "Mousebutton", "MouseButton" },
        { "Cursorpos", "CursorPosition" },
        { "Cursorenter", "CursorEnter" },
        { "Charmods", "CharModification" },
    }.ToFrozenDictionary();
    
    public static readonly FrozenDictionary<string, string> TypeMapping = new Dictionary<string, string>
    {
        { "void", "void"},
        { "bool", "bool" },
        { "byte", "byte" },
        { "short", "short" },
        { "unsigned short", "ushort" },
        { "int", "int" },
        { "unsigned int", "uint" },
        { "long", "long" },
        { "unsigned long", "long" },
        // TODO: long long 
        
        { "float", "float" },
        { "double", "double" },
        { "long double", "decimal" },
        
        // We don't use char,
        // because C# allocates 2 bytes for its storage instead of 1 as in C++,
        // which will break us the reference transmission
        { "char", "byte" }, // { "char", "char" },
        
        // TODO: wchar_t, std::string, std::wstring
        { "size_t", "nuint" },
        
        /*
         * GL 
         */
        
        { "GLenum", "uint" },
        { "GLuint", "uint" },
        { "GLbitfield", "uint" },
        { "GLboolean", "int" },
        { "GLint", "int" },
        { "GLsizei", "int" },
        { "GLfloat", "float" },
        { "GLclampf", "float" },
        { "GLdouble", "double" },
        
        /*
         * GLFW
         */
        
        // Structs
        { "GLFWwindow", "nint" },
        { "GLFWmonitor", "nint" },
        { "GLFWvidmode", "nint" },
        { "VkResult", "int" },
        
        // Callbacks
        { "GLFWglproc", "GlfwCallbacks.GLProcess" },
        { "GLFWvkproc", "GlfwCallbacks.VKProcess" },
        { "GLFWallocatefun", "GlfwCallbacks.Allocate" },
        { "GLFWreallocatefun", "GlfwCallbacks.Reallocate" },
        { "GLFWdeallocatefun", "GlfwCallbacks.Deallocate" },
        { "GLFWerrorfun", "GlfwCallbacks.Error" },
        { "GLFWwindowposfun", "GlfwCallbacks.WindowPosition" },
        { "GLFWwindowsizefun", "GlfwCallbacks.WindowSize" },
        { "GLFWwindowclosefun", "GlfwCallbacks.WindowClose" },
        { "GLFWwindowrefreshfun", "GlfwCallbacks.WindowRefresh" },
        { "GLFWwindowfocusfun", "GlfwCallbacks.WindowFocus" },
        { "GLFWwindowiconifyfun", "GlfwCallbacks.WindowIconify" },
        { "GLFWwindowmaximizefun", "GlfwCallbacks.WindowMaximize" },
        { "GLFWframebuffersizefun", "GlfwCallbacks.FrameBufferSize" },
        { "GLFWwindowcontentscalefun", "GlfwCallbacks.WindowContentScale" },
        { "GLFWmousebuttonfun", "GlfwCallbacks.MouseButton" },
        { "GLFWcursorposfun", "GlfwCallbacks.CursorPosition" },
        { "GLFWcursorenterfun", "GlfwCallbacks.CursorEnter" },
        { "GLFWscrollfun", "GlfwCallbacks.Scroll" },
        { "GLFWkeyfun", "GlfwCallbacks.Key" },
        { "GLFWcharfun", "GlfwCallbacks.Char" },
        { "GLFWcharmodsfun", "GlfwCallbacks.CharModification" },
        { "GLFWdropfun", "GlfwCallbacks.Drop" },
        { "GLFWmonitorfun", "GlfwCallbacks.Monitor" },
        { "GLFWjoystickfun", "GlfwCallbacks.Joystick" }
    }.ToFrozenDictionary();
    
    public static readonly FrozenDictionary<string, string> KeywordMapping = new Dictionary<string, string>
    {
        { "params", "prms" },
        { "string", "str" },
        { "object", "obj" },
        { "event", "ev" },
        { "ref", "refer" }
    }.ToFrozenDictionary();
}