using System.Reflection;
using NGross.Core.Attributes.TestAttributes;
using NGross.Core.Elements;
using NGross.Core.Models;

namespace NGross.Core.Engine.Parser.ThreadGroup;

public class ThreadGroupParser : IThreadGroupParser
{
    public IEnumerable<IThreadGroup> Parse(Assembly assembly)
    {
        return assembly.GetTypes().Where(type => type.IsDefined(typeof(ThreadGroupAttribute)))
            .Select(threadGroupTypes => new Elements.ThreadGroup(threadGroupTypes));
    }
}