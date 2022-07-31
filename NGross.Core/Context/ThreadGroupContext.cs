using BoDi;
using Microsoft.Extensions.Configuration;

namespace NGross.Core.Context;

public class ThreadGroupContext
{
    public ThreadGroupContext(IConfiguration configuration)
    {
        this.Configuration = configuration;
        this.Container = new ObjectContainer();
    }

    public IConfiguration Configuration { get; set; }

    public IObjectContainer Container;
}