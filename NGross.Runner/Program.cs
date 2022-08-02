// See https://aka.ms/new-console-template for more information

using System.Reflection;
using NGross.Core.Builders;
using NGross.Core.Engine.Loader;
using NGross.Core.Engine.Parser.ThreadAction;
using NGross.Core.Engine.Parser.ThreadGroup;
using NGross.Core.Plan;;

if (args.Length < 1)
{
    Console.WriteLine("Please provide a Test File");
}

var builder = new TestBuilder(args[0], 
    new AssemblyLoader(), 
    new ThreadGroupParser(), 
    new ActionParser());
var test = builder.Build();

Console.Write($"Parsed Test {test}");