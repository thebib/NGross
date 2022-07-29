namespace NGross.Core.Elements;

public class ThreadGroup : IThreadGroup
{
    
    public Type InnerTestType { get; set; }
    public ThreadGroup(Type t)
    {
        this.InnerTestType = t;
        Actions = new List<ThreadAction>();
    }

    public IEnumerable<ThreadAction> Actions { get; set; }
}