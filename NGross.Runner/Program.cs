// See https://aka.ms/new-console-template for more information

using System.Reflection;
using NGross.Core.Engine.Loader;
using NGross.Core.Engine.Parser.ThreadAction;
using NGross.Core.Engine.Parser.ThreadGroup;
using NGross.Core.Plan;;

if (args.Length < 1)
{
    Console.WriteLine("Please provide a Test File");
}

var loader = new AssemblyLoader();
var assembly = loader.LoadFromAssembly($"{args[0]}");

var groupParser = new ThreadGroupParser();
var groups = groupParser.Parse(assembly).ToList();

foreach (var threadGroup in groups)
{
    var actionParser = new ActionParser();
    threadGroup.Actions = actionParser.Parse(threadGroup.InnerTestType, threadGroup.ThreadGroupContext);
}

Console.Write($"Parsed Test {groups}");