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
    
    public void RunThreadGroups()
    {
        foreach (var threadAction in ThreadGroups)
        {
            RunActionsInThreadGroup(threadAction);
        }
    }

    private void RunActionsInThreadGroup(IThreadGroup threadActionValue)
    {
        foreach (var i in threadActionValue.Actions)
        {
            i.MethodInfo.Invoke(threadActionValue.InnerTestType, new object?[]{});
        }
    }
}

public interface ITest
{
    public IEnumerable<IThreadGroup>? ThreadGroups { get; set; }

    void RunThreadGroups();
}