using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using NGross.Core.Elements;
using NGross.Core.Engine.Loader;
using NGross.Core.Engine.Parser.ThreadGroup;
using NGross.Core.Models;
using NGross.Core.Plan;
using NUnit.Framework;

namespace NGross.Core.Test.Plans;

[TestFixture]
public class UserTest
{
    [Test]
    public void TestExecuteAction()
    {
        AssemblyLoader loader = new AssemblyLoader();
        var assembly = loader.LoadFromAssembly("mock-assembly.dll");
        var threadGroupsParser = new ThreadGroupParser();
        var groups = threadGroupsParser.Parse(assembly);
        
        var user = new User
        {
            ThreadGroups = groups
        };
        
        user.ExecuteActions();
    }
}