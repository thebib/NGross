using Microsoft.Extensions.Configuration;
using NGross.Core.Context;
using NGross.Core.Models;

namespace NGross.Core.Elements;

public class ThreadGroup : IThreadGroup
{
    public Type InnerTestType { get; set; }
    public ThreadGroupContext ThreadGroupContext { get; set; }

    public ThreadGroup(Type t)
    {
        this.InnerTestType = t;
        this.ThreadGroupContext = new ThreadGroupContext(new ConfigurationRoot(
            new List<IConfigurationProvider>()));
        Actions = new List<IThreadAction>();
    }

    public IEnumerable<IThreadAction> Actions { get; set; }
}