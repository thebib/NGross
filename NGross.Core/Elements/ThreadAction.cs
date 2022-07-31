using System.Reflection;
using NGross.Core.Context;
using NGross.Core.Models;

namespace NGross.Core.Elements;

public class ThreadAction : IThreadAction
{
    public ThreadAction(MethodInfo methodInfo, ThreadGroupContext context)
    {
        this.Context = context;
        this.MethodInfo = methodInfo;
    }

    public ThreadGroupContext Context { get; set; }
    public MethodInfo MethodInfo { get; set; }
}