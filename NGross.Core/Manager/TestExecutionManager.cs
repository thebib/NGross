using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using NGross.Core.Calculators;
using NGross.Core.Elements;
using NGross.Core.Logging;
using NGross.Core.Plan;

namespace NGross.Core.Manager;

public class TestExecutionManager : ITestExecutionManager
{
    public IPacingCalculator Calculator;
    private Dictionary<IThreadGroup, IConfigurationSection> threadGroupDict;
    private List<Task> ThreadGroupThreads;
    public TestExecutionManager(INGrossLogger logger, ITest test, IPacingCalculator calculator)
    {
        ThreadGroupThreads = new List<Task>();
        threadGroupDict =
            new Dictionary<IThreadGroup, IConfigurationSection>();
        logger.Log("Test Manager has started");
        this.Test = test;
        foreach (var testThreadGroup in this.Test.ThreadGroups!)
        {
            var config = testThreadGroup.ThreadGroupContext.Configuration;
            var threadConfig = config.GetSection("ThreadGroupConfig")
                .GetChildren().SingleOrDefault(c => c.Key == testThreadGroup.ThreadGroupConfiguration);
            if (threadConfig != null) threadGroupDict.Add(testThreadGroup, threadConfig);
        }
        this.Calculator = calculator;
    }

    private ITest Test { get; set; }

    public async void Execute()
    {
        foreach (var testThreadGroup in threadGroupDict)
        {
            var threadGroupCount = 1;
            var userCount = Convert.ToInt32(testThreadGroup.Value["Users"]);
            for (var i = 0; i <= userCount ; i++) {
                var count = threadGroupCount;
                var a =
                Task.Run(() =>
                {
                    var iteration = GetThreadCount(testThreadGroup);
                    for (var t = 0; t <= iteration; t++)
                    {
                        RunThreadGroup(testThreadGroup.Key,
                            Calculator.CalculatePacing(testThreadGroup, t, count));
                    }
                });
                ThreadGroupThreads.Add(a);
                threadGroupCount++;
            }
        }
        Console.Write("Tasks created - Time for the magic to happen");
        Task.WaitAll(ThreadGroupThreads.ToArray());
    }
    private int GetThreadCount(KeyValuePair<IThreadGroup, IConfigurationSection> testThreadGroup)
    {
        return Convert.ToInt32(testThreadGroup.Value["Loop"]);
    }

    private void RunThreadGroup(IThreadGroup threadGroup, PacingStats pacingController)
    {
        Console.WriteLine($"Sleeping for {pacingController.Before}");
        Thread.Sleep(pacingController.Before);
        foreach (var i in threadGroup.Actions)
        {
            i.MethodInfo.Invoke(threadGroup.ThreadGroupInstance, Array.Empty<object?>());
        }
        Thread.Sleep(pacingController.After);
    }

    public struct PacingStats
    {
        public int Before;
        public int After;
    }

    public void Stop()
    {
    }
}