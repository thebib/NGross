using NGross.Core.Elements;
using NGross.Core.Models;

namespace NGross.Core.Plan;

/// <summary>
/// Denotes a User (Aka a Thread) with a set of actions to perform, a wait time to start and a count of loops
/// </summary>
public class User : IUser
{
    public IEnumerable<IThreadGroup> ThreadGroups { get; set; }
    public void RunThreadGroup()
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

public interface IUser
{
    public IEnumerable<IThreadGroup> ThreadGroups { get; set; }

    void RunThreadGroup();
}