using System.Linq;
using mock_assembly;
using NGross.Core.Elements;
using NGross.Core.Engine.Parser;
using NGross.Core.Engine.Parser.ThreadAction;
using NGross.Core.Models;
using NUnit.Framework;
using Shouldly;

namespace NGross.Core.Test.Parser;

[TestFixture]
public class ActionParserTest
{
    private ThreadGroup _threadGroup;
    private ActionParser _parser;
    
    [SetUp]
    public void Setup()
    {
        this._parser = new ActionParser();
        this._threadGroup = new ThreadGroup(typeof(MockFixture));
    }

    [Test]
    public void ParseActionSuccessfullyTest()
    {
        var action = this._parser.Parse(this._threadGroup.InnerTestType);
        action.ToList().Select(c => c.MethodInfo.Name).FirstOrDefault().ShouldBe("Execute");
    }
}