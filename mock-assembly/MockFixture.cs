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
    public async Task Execute(ThreadGroupContext context) {
        await Task.FromResult(true);
    }

    [Action]
    public async Task FaultExecute()
    {
        await Task.Delay(1);
        throw new Exception();
    }
}

