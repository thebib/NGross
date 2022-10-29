using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using NGross.Core.Calculators;
using NGross.Core.Context;
using NGross.Core.Elements;
using NGross.Core.Logging;
using NGross.Core.Plan;

namespace NGross.Core.Manager;

public class TestExecutionManager : ITestExecutionManager
{
    private readonly IPacingCalculator _calculator;
    private Dictionary<IThreadGroup, IConfigurationSection> threadGroupDict;
    private List<Task> ThreadGroupThreads;
    private INGrossLogger logger;
    public TestExecutionManager(INGrossLogger logger, ITest test, IPacingCalculator calculator)
    {
        ThreadGroupThreads = new List<Task>();
        threadGroupDict =
            new Dictionary<IThreadGroup, IConfigurationSection>();
        logger = new NGrossLogger();
        this.logger = logger;
        this.Test = test;
        foreach (var testThreadGroup in this.Test.ThreadGroups!)
        {
            var config = testThreadGroup.ThreadGroupContext.Configuration;
            var threadConfig = config.GetSection("ThreadGroupConfig")
                .GetChildren().SingleOrDefault(c => c.Key == testThreadGroup.ThreadGroupConfiguration);
            if (threadConfig != null) threadGroupDict.Add(testThreadGroup, threadConfig);
        }
        this._calculator = calculator;
    }

    private ITest Test { get; set; }

    public async Task Execute()
    {
        foreach (var testThreadGroup in threadGroupDict)
        {
            var threadGroupCount = 1;
            var userCount = Convert.ToInt32(testThreadGroup.Value["Users"]);
            for (var i = 0; i < userCount ; i++)
            {
                var currentThread = threadGroupCount;
                var a =
                Task.Run(async () =>
                {
                    var iterations = GetThreadCount(testThreadGroup);
                    for (var currentIteration = 0; currentIteration <= iterations; currentIteration++)
                    {
                        await RunThreadGroup(testThreadGroup.Key,
                            _calculator.CalculatePacing(testThreadGroup, currentIteration, currentThread));
                    }
                });
                ThreadGroupThreads.Add(a);
                threadGroupCount++;
            }
        }
        await Task.WhenAll(ThreadGroupThreads.ToArray());
    }
    private int GetThreadCount(KeyValuePair<IThreadGroup, IConfigurationSection> testThreadGroup)
    {
        return Convert.ToInt32(testThreadGroup.Value["Loop"]);
    }

    private async Task RunThreadGroup(IThreadGroup threadGroup, PacingStats pacingController)
    {
        Thread.Sleep(pacingController.Before);
        foreach (var i in threadGroup.Actions)
        {
            var parameters = i.MethodInfo.GetParameters();

            if (parameters.Length == 0)
            {
                await ((Task) i.MethodInfo.Invoke(threadGroup.ThreadGroupInstance, Array.Empty<object>())!)!;
                continue;
            }
            
            if (parameters.Single().ParameterType == typeof(ThreadGroupContext))
            {
                await ((Task) i.MethodInfo.Invoke(threadGroup.ThreadGroupInstance, new object?[]
                {
                    threadGroup.ThreadGroupContext
                })!)!;
            }
            
            else
            {
                throw new Exception(
                    "Attempted to invoke test method with unsupported parameter, please use ThreadGroupContext!");
            }
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