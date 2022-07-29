namespace NGross.Core.Elements;

public interface IThreadGroup
{
    public IEnumerable<ThreadAction> Actions { get; set; }
    public Type InnerTestType { get; set; }
}