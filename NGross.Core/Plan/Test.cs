using NGross.Core.Elements;
using NGross.Core.Models;

namespace NGross.Core.Plan;

/// <summary>
/// Denotes a User (Aka a Thread) with a set of actions to perform, a wait time to start and a count of loops
/// </summary>
public class Test : ITest
{
    public Test(IEnumerable<IThreadGroup>? threadGroups)
    {
        ThreadGroups = threadGroups;
    }

    public IEnumerable<IThreadGroup>? ThreadGroups { get; set; }

    public void RunThreadGroup(IThreadGroup threadGroup)
    {
        foreach (var i in threadGroup.Actions)
        {
            i.MethodInfo.Invoke(threadGroup.ThreadGroupInstance, Array.Empty<object?>());
        }
    }
}

public interface ITest
{
    public IEnumerable<IThreadGroup>? ThreadGroups { get; set; }

    void RunThreadGroup(IThreadGroup group);
}