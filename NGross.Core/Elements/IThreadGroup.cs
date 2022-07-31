using NGross.Core.Context;
using NGross.Core.Models;

namespace NGross.Core.Elements;

public interface IThreadGroup
{
    public IEnumerable<IThreadAction> Actions { get; set; }
    public Type InnerTestType { get; set; }
    public ThreadGroupContext ThreadGroupContext { get; set; }
}