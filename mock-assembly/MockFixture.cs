using System.Reflection.Metadata;
using BoDi;
using NGross.Core.Attributes.TestAttributes;
using NGross.Core.Context;

namespace mock_assembly;


/// <summary>
/// A Thread Group denotes a list of tasks to run on a group of threads
/// Controlled by the config passed in read from the config files
///
/// Setup allows you to 
/// </summary>
[ThreadGroup("ConfigA")]
public class MockFixture
{
    [Action]
    public async Task Execute() {
        Console.WriteLine("Hello!");
        await Task.FromResult(true);
    }

    [Action]
    public async Task FaultExecute()
    {
        Console.WriteLine("Uh oh Im going to break!");
        Console.WriteLine("Hello World!");
        await Task.Delay(1000);
        throw new Exception();
    }
}

