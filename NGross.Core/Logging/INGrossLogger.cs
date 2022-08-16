namespace NGross.Core.Logging;

public interface INGrossLogger
{
    void Log(string message);
    void LogStatistic(int result, string name);
    void Dump();
}