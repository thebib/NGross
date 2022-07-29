using Microsoft.Extensions.Configuration;

namespace NGross.Core.Config.Reader;

public class NGrossConfigReader : INGrossConfigReader
{
    private readonly ConfigurationBuilder _builder = new();
    private readonly List<string> _parsedConfigs = new();

    /// <summary>
    /// Reads Configuration from the provided filepath, will attempt to read sub-configurations
    /// </summary>
    /// <param name="path">The path to the configuration file</param>
    /// <returns>A configuration object containing the parsed configurations</returns>
    public IConfigurationRoot Read(string path)
    {
        _builder.AddJsonFile(path);
        var rootConfig = _builder.Build();
        ReadSubConfigs(_builder, rootConfig);
        return _builder.Build();
    }

    private void ReadSubConfigs(IConfigurationBuilder builder, IConfiguration config)
    {
        foreach (var configsEntry in config.GetSection("Configs")
                     .GetChildren()
                     .AsEnumerable())
        {
            var configFile = configsEntry.Value!;
            
            //TODO logging here, dependent on the outcome of parsing the file
            if (!File.Exists(configFile) || _parsedConfigs.Contains(configFile)) continue;
            
            builder.AddJsonFile(configFile);
            _parsedConfigs.Add(configFile);
            Read(configsEntry.Value!);
        }
    }
}