using System.Runtime.CompilerServices;
using NGross.Core.Elements;
using NGross.Core.Logging;
using NGross.Core.Plan;

namespace NGross.Core.Manager;

public class TestExecutionManager : ITestExecutionManager
{
    public TestExecutionManager(INGrossLogger logger, Test test)
    {
        logger.Log("Test Manager has started");
        this.Test = test;
    }

    private Test Test { get; set; }

    public async void Execute()
    {
        foreach (var testThreadGroup in this.Test.ThreadGroups)
        {
            var config = testThreadGroup.ThreadGroupContext.Configuration;
            var threadConfig = config[testThreadGroup.ThreadGroupConfiguration];
        }
    }

    public void Stop()
    {
    }
}