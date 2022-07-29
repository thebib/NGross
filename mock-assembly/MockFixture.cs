using NGross.Core.Attributes.TestAttributes;

namespace mock_assembly;

[ThreadGroup("ConfigA")]
public class MockFixture
{
    [Action]
    public async Task Execute()
    {
        await Task.FromResult(true);
    }

    [Action]
    public async Task FaultExecute()
    {
        await Task.Delay(1);
        throw new Exception();
    }
}