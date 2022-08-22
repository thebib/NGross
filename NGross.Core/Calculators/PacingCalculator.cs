using Microsoft.Extensions.Configuration;
using NGross.Core.Elements;
using NGross.Core.Manager;

namespace NGross.Core.Calculators;

public interface IPacingCalculator
{
    TestExecutionManager.PacingStats 
        CalculatePacing(KeyValuePair<IThreadGroup, IConfigurationSection> threadGroupDict, int iteration, int thread);
}

public class PacingCalculator : IPacingCalculator
{
    public TestExecutionManager.PacingStats
        CalculatePacing(KeyValuePair<IThreadGroup, IConfigurationSection> threadGroupDict, int iteration, int thread)
    {
        var rampup = Convert.ToInt32(threadGroupDict.Value["Ramp-up"]);
        var users = Convert.ToInt32(threadGroupDict.Value["Users"]);

        if (thread > 1 && rampup > 0 && iteration==0)
        {
            var delaySegment = rampup * 1000 / users;
            var delay = (thread - 1) * delaySegment;
            
            return new TestExecutionManager.PacingStats()
            {
                After = 0,
                Before = delay
            };
        }

        return new TestExecutionManager.PacingStats()
        {
            After = 0,
            Before = 0
        };
    }
}
