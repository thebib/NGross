using Microsoft.Extensions.Configuration;
using NGross.Core.Config.Reader;
using NGross.Core.Context;
using NGross.Core.Models;

namespace NGross.Core.Elements;

public class ThreadGroup : IThreadGroup
{
    public Type InnerTestType { get; set; }
    public ThreadGroupContext ThreadGroupContext { get; set; }

    public ThreadGroup(Type t, string config)
    {
        this.ThreadGroupConfiguration = config;
        this.InnerTestType = t;
        this.ThreadGroupInstance = Activator.CreateInstance(t);
        if (NGrossConfigManager.Configuration != null)
            this.ThreadGroupContext = new ThreadGroupContext(NGrossConfigManager.Configuration);
        else
        {
            throw new FileNotFoundException("No configuration file provided");
        }
        Actions = new List<IThreadAction>();
    }

    public object? ThreadGroupInstance { get; set; }
    public string ThreadGroupConfiguration { get; set; }

    public IEnumerable<IThreadAction> Actions { get; set; }
}