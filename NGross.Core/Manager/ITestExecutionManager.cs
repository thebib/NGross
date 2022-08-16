using NGross.Core.Logging;

namespace NGross.Core.Manager;

public interface ITestExecutionManager
{
    void Execute();
    void Stop();
}