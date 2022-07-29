using System.Reflection;

namespace NGross.Core.Engine.Loader;

public class AssemblyLoader : IAssemblyLoader
{
    public Assembly LoadFromAssembly(string assembly)
    {
        return Load(assembly);
    }

    protected virtual Assembly Load(string assembly) => 
        Assembly.LoadFrom(assembly);

    protected virtual string GetPath(string assembly) => 
        Path.GetFullPath(assembly);
}