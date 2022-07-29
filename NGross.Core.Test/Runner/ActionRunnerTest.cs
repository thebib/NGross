using System;
using System.Threading.Tasks;
using NGross.Core.Runners;
using NUnit.Framework;
using Shouldly;

namespace NGross.Core.Test.Runner;

public class ActionRunnerTest
{
    [Test]
    public async Task CanRunActionTest()
    {
        var runner = new ActionRunner(SampleTask());
        var result = await runner.Run();
        result.Milliseconds.ShouldBeGreaterThan(0);
        result.InternalException.ShouldBeNull();
    }

    [Test]
    public async Task CanHandleErrorTest()
    {
        var runner = new ActionRunner(ErrorTask());
        var result = await runner.Run();
        result.Milliseconds.ShouldBeGreaterThan(0);
        result.InternalException.ShouldBeOfType<Exception>();
    }

    private static async Task SampleTask() => await Task.Delay(1);

    private static async Task ErrorTask()
    {
        await SampleTask();
        throw new Exception();
    }
}