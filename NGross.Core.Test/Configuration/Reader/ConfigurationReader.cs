using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

using NGross.Core.Config.Reader;
using NUnit.Framework;
using Shouldly;

namespace NGross.Core.Test.Configuration.Reader;

[TestFixture]
public class ConfigurationReader
{
    [Test]
    public void CanReadConfig()
    {
        var reader = new NGrossConfigReader();
        IConfiguration config = reader.Read("ngross_config.json");

        config["ThreadGroupConfig:ConfigA:Users"].ShouldBe("1");
        config["ThreadGroupConfig:ConfigB:Users"].ShouldBe("1");
    }

    [Test]
    public void CannotReadConfig()
    {
        var reader = new NGrossConfigReader();
        Should.Throw<FileNotFoundException>(() =>
        {
            reader.Read("junk.json");
        });
    }

    /// <summary>
    /// TODO: Implement Logging in this scenario, and test it.
    /// </summary>
    [Test]
    public void HandleMissingSubConfigs()
    {
        var reader = new NGrossConfigReader();
        reader.Read("ngross_broken_config.json");
    }
}

public class NGrossConfigReader : INGrossConfigReader
{
    public IConfigurationRoot Read(string path)
    {
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile(path);
        var rootConfig = builder.Build();
        ReadSubConfigs(builder, rootConfig);
        return builder.Build();
    }

    private static void ReadSubConfigs(IConfigurationBuilder builder, IConfiguration config)
    {
            foreach (var configsEntry in config.GetSection("Configs")
                         .GetChildren()
                         .AsEnumerable())
            {
                if (File.Exists(configsEntry.Value!))
                    builder.AddJsonFile(configsEntry.Value!);
            }
    }
}