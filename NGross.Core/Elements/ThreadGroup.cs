using Microsoft.Extensions.Configuration;
using NGross.Core.Config.Reader;
using NGross.Core.Context;
using NGross.Core.Models;

namespace NGross.Core.Elements;

public class ThreadGroup : IThreadGroup
{
    public Type InnerTestType { get; set; }
    public ThreadGroupContext ThreadGroupContext { get; set; }
    public string ThreadConfiguration;

    public ThreadGroup(Type t, string config)
    {
        this.ThreadConfiguration = config;
        this.InnerTestType = t;
        if (NGrossConfigManager.Configuration != null)
            this.ThreadGroupContext = new ThreadGroupContext(NGrossConfigManager.Configuration);
        else
        {
            throw new FileNotFoundException("No configuration file provided");
        }
        Actions = new List<IThreadAction>();
    }

    public IEnumerable<IThreadAction> Actions { get; set; }
}